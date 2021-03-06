﻿Imports System.IO
Imports System.Net
Imports System.Collections.Specialized

Public Partial Class DLVI
	Inherits System.Windows.Forms.ListViewItem
	Private Episode As API.Episode = Nothing
	Private Hosters As List(Of API.Hoster)
	Private _Active As Boolean = False
	Private _finished As Boolean = False
	Private _error As Boolean = False
	Private TargetPath As String = ""
	Private GeneratedName As String = ""
	Private wc As New WebClient()
	Private _vis As Boolean = True
	
	Public ReadOnly Property Fehler As Boolean
		Get
			Return(_error)
		End Get
	End Property
	Public ReadOnly Property Active As Boolean
		Get
			Return(_Active)
		End Get
	End Property
	Public ReadOnly Property Done As Boolean
		Get
			Return(_finished)
		End Get
	End Property
	Public Property Visible As Boolean
		Get
			Return(_vis)
		End Get
		Set
			_vis = value
		End Set
	End Property
	Public Sub BeginDownload()
		If Not _Active Then
			_Active = True
			If Me.SubItems.Count <= 1 Then
				Me.SubItems.Add("")
			End If
			Me.SubItems(1).Text = "Starte ..."
			Dim work_thread As New System.Threading.Thread(AddressOf WorkOnHoster)
			work_thread.Start()
		End If
	End Sub
	
	Private Delegate Sub SetStateD(ByVal s As String)
	Private Sub SetState(ByVal s As String)
		If Me.ListView.InvokeRequired Then
			Me.ListView.Invoke(New SetStateD(AddressOf SetState),New Object() {s})
		Else
			Me.SubItems(1).Text = s
			cur_val = s
		End If
	End Sub
	
	Public Event Finished(ByRef obj As DLVI,ByVal e As Exception)
	
	Private Sub WorkOnHoster()
		Dim shl As List(Of HosterState) = GetSortedHosterList(Hosters)
		
		Dim Successed As Boolean = False
		Dim NoHoster As Boolean = False
		While Not Successed And Not NoHoster
			Dim currentHoster As HosterState = GetNextWorkingHoster(shl)
			If currentHoster IsNot Nothing Then
				SetState(currentHoster.Hoster.Name)
				If HosterScripts.HosterSupported(currentHoster.Hoster.Name) Then
					SetState(currentHoster.Hoster.Name & " wird unterstützt")
					Try
						Dim dl As String = currentHoster.Link(0)
						Dim hs As String = HosterScripts.GetHosterScript(currentHoster.Hoster.Name)
						Dim ht As Integer = HosterScripts.GetHosterScriptType(currentHoster.Hoster.Name)
						Dim hosterParsingClass As Object = QuickCompiler.Compile(hs,ht)
						Dim directLink As String = CStr(hosterParsingClass.ParseHoster(dl))
						If directLink = "" Then
							SetState(currentHoster.Hoster.Name & " parsen fehlgeschlagen")
							currentHoster.Success = 2
							_Active = False
						Else
							SetState(currentHoster.Hoster.Name & " parsen erfolgreich")
							SetState("Download läuft ...")
							Dim fi As New System.IO.FileInfo(TargetPath)
							If Not fi.Directory.Exists Then
								Dim par As DirectoryInfo = fi.Directory.Parent
								If Not par.Exists Then
									Dim tpar As DirectoryInfo = par.Parent
									If Not tpar.Exists Then
										Dim vpar As DirectoryInfo = tpar.Parent
										If Not vpar.Exists Then
											vpar.Create()
										End If
										tpar.Create()
									End If
									par.Create()
								End If
								fi.Directory.Create()
							End If
							wc.DownloadFileAsync(New Uri(directLink),TargetPath)
							Successed = True
						End If
					Catch ex As Exception
						GlobalDebugDiag.DebugDiag.Log(ex.ToString(),"INFO")
						SetState("Hoster-Fehler")
					End Try
				Else
					SetState(currentHoster.Hoster.Name & " wird nicht unterstützt")
					currentHoster.Success = 2
				End If
			Else
				SetState("Hoster-Problem! (Keine Hoster!)")
				NoHoster = True
				_Active = False
				_error = True
				_finished = False
				RaiseEvent Finished(Me,New Exception("Kein Hoster verfügbar!"))
			End If
		End While
	End Sub
	Public Sub LaunchFile()
		System.Diagnostics.Process.Start(TargetPath)
	End Sub
	Public Sub DLPDone(ByVal sender As Object,ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
		If e.Error IsNot Nothing Then
			If e.Error.Message.IndexOf("The request was canceled") > -1 Then
				SetState("Download abgebrochen")
				_Active = False
				_finished = False
				_error = False
				cur = 0
				max = 0
			Else
				GlobalDebugDiag.DebugDiag.Log(e.Error.ToString(),"INFO")
				SetState("Download fehlgeschlagen")
				_Active = False
				_finished = False
				_error = True
				cur = 0
				max = 0
			End If
		Else
			SetState("Download abgeschlossen")
			_Active = False
			_finished = True
			_error = False
		End If
		RaiseEvent Finished(Me,e.Error)
		_finished = True
	End Sub
	Private cur_val As String = ""
	Public cur As Long = 0
	Public max As Long = 0
	Private _last_received As Long = 0
	Public Event AddReceivedBytes(ByVal b As Long)
	Public Sub DLPChanged(ByVal sender As Object,ByVal e As System.Net.DownloadProgressChangedEventArgs)
		cur = e.BytesReceived
		max = e.TotalBytesToReceive
		RaiseEvent AddReceivedBytes(e.BytesReceived - _last_received)
		_last_received = e.BytesReceived
		If cur_val <> "Download läuft ... (" & e.ProgressPercentage & "%)" Then
			SetState("Download läuft ... (" & e.ProgressPercentage & "%)")
		End If
	End Sub
	Private Function ClrN(ByVal s As String) As String
		Dim n As New List(Of Char)
		n.AddRange(Path.GetInvalidFileNameChars())
		n.AddRange(Path.GetInvalidPathChars())
		While s.IndexOfAny(n.ToArray()) > -1
			For Each t As Char In n.ToArray()
				s = s.Replace(t,"_")
			Next
		End While
		Return(s)
	End Function
	Public Sub New(ByVal epi As API.Episode,ByVal hos As List(Of API.Hoster))
		Episode = epi
		Hosters = hos
		TargetPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonVideos)
		If TargetPath.Substring(TargetPath.Length - 1,1) <> "\" Then
			TargetPath &= "\"
		End If
		TargetPath = GlobalConfig.GlobalConfig.DBSelect("downloader.path",TargetPath,True).Trim()
		If TargetPath.Substring(TargetPath.Length - 1,1) <> "\" Then
			TargetPath &= "\"
		End If
		Dim nvc As New NameValueCollection()
		nvc.Add("%serie%",NameGenerator.CleanFN(Episode.Season.Serie.Name))
		nvc.Add("%seasonNN%",NameGenerator.cc(Episode.Season.Number))
		nvc.Add("%seasonN%",Episode.Season.Number.ToString())
		nvc.Add("%episodeNN%",NameGenerator.cc(Episode.Episode))
		nvc.Add("%episodeN%",Episode.Episode.ToString())
		nvc.Add("%name%",NameGenerator.CleanFN(Episode.Name))
		nvc.Add("%nameE%",NameGenerator.CleanFN(Episode.NameEnglish))
		nvc.Add("%nameG%",NameGenerator.CleanFN(Episode.NameGerman))
		GeneratedName = NameGenerator.BuildName(nvc)
		TargetPath &= GeneratedName
		Me.Text = GeneratedName
		AddHandler wc.DownloadProgressChanged, AddressOf DLPChanged
		AddHandler wc.DownloadFileCompleted, AddressOf DLPDone
	End Sub
	Private Function GetNextWorkingHoster(ByVal hosterlist As List(Of HosterState)) As HosterState
		Dim result As HosterState = Nothing
		For Each i As HosterState In hosterlist
			If i.Success <> 2 Then
				result = i
				Exit For
			End If
		Next
		Return(result)
	End Function
	Private Function GetSortedHosterList(ByVal hosterList As List(Of API.Hoster)) As List(Of HosterState)
		Dim PriorityList() As String = HosterScripts.GetPriorityList().ToArray()
		Dim SortedList As New List(Of HosterState)
		For Each priorityEntry As String In PriorityList
			For Each hosterEntry As API.Hoster In hosterList
				If hosterEntry.Name.ToLower() = priorityEntry.ToLower() Then
					SortedList.Add(New HosterState(hosterEntry))
				End If
			Next
		Next
		Return(SortedList)
	End Function
	Public Sub StopDownload()
		Try
			wc.CancelAsync()
		Catch
		End Try
	End Sub
End Class
Public Class HosterState
	Public Hoster As API.Hoster = Nothing
	Public Success As Integer = 0
	Public HosterLink() As String = Nothing
	Public ReadOnly Property Link As String()
		Get
			Try
				If HosterLink Is Nothing And Success <> 2 Then
					Dim hl As New List(Of String)
					For Each id As Integer In Hoster.Ids
						hl.Add(API.Watch(id))
					Next
					HosterLink = hl.ToArray()
				End If
			Catch
				Success = 2
			End Try
			Return(HosterLink)
		End Get
	End Property
	Public Sub New(ByVal h As API.Hoster)
		Hoster = h
	End Sub
End Class