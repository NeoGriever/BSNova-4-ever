Imports System.Threading
Imports System.Collections.Specialized
Imports System.Text
Imports System.Xml
Imports System.Text.RegularExpressions

Public Partial Class MainForm
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	Private selfTimestamp As DateTime = DateTime.Parse("2016-10-14 02:00:00")
	#Region " Variablen "
		Private SortingList As New HybridDictionary()
		Private treeNodeList As New HybridDictionary()
		Private ShowedList As treeViewDoubleBuffered = Nothing
	#End Region
	#Region " Handles "
		Private Sub SplitContainer1SplitterMoved(sender As Object, e As SplitterEventArgs)
			
		End Sub
		Private Sub ScriptEditorÖffnenToolStripMenuItemClick(sender As Object, e As EventArgs)
			Dim se As New ScriptEditor()
			se.ShowDialog()
		End Sub
		Private Sub ListView1SelectedIndexChanged(sender As Object, e As EventArgs)
			
		End Sub
		Private Sub ListView1ItemActivate(sender As Object, e As EventArgs)
			Try
				CType(listView1.FocusedItem,DLVI).BeginDownload()
			Catch
			End Try
		End Sub
		Private Sub FilterBoxTextChanged(sender As Object, e As EventArgs)
			timer2.Stop()
			timer2.Enabled = False
			timer2.Enabled = True
			timer2.Start()
		End Sub
		Private Sub Timer2Tick(sender As Object, e As EventArgs)
			timer2.Enabled = False
			timer2.Stop()
			If filterBox.Text <> "" Then
				Dim filteredNodeList As New List(Of TreeNode)
				For Each letter_node As TreeNode In treeView1.Nodes
					Dim filteredLetterNode As New TreeNode(letter_node.Text)
					For Each series_node As TreeNode In letter_node.Nodes
						If series_node.Text.Trim().IndexOf(filterBox.Text) > -1 Then
							filteredLetterNode.Nodes.Add(CType(series_node.Clone(),TreeNode))
						End If
					Next
					If filteredLetterNode.Nodes.Count > 0 Then
						filteredLetterNode.Expand()
						filteredNodeList.Add(filteredLetterNode)
					End If
				Next
				If ShowedList Is Nothing Then
					treeView1.Visible = False
					
					ShowedList = New treeViewDoubleBuffered()
					AddHandler ShowedList.AfterSelect, AddressOf FindSelectedSeries
					ShowedList.Dock = DockStyle.Fill
					Panel1.Controls.Add(ShowedList)
				End If
				ShowedList.Nodes.Clear()
				ShowedList.Nodes.AddRange(filteredNodeList.ToArray())
			Else
				Try
					panel1.Controls.Remove(ShowedList)
				Catch
				End Try
				Try
					treeView1.Visible = True
				Catch
				End Try
				Try
					RemoveHandler ShowedList.AfterSelect, AddressOf FindSelectedSeries
				Catch
				End Try
				Try
					ShowedList.Dispose()
				Catch
				End Try
				Try
					ShowedList = Nothing
				Catch
				End Try
			End If
		End Sub
		Private Sub FindSelectedSeries(ByVal sender As Object,ByVal e As EventArgs)
			If ShowedList.SelectedNode.Level = 1 Then
				For Each letter_node As TreeNode In treeView1.Nodes
					For Each series_node As TreeNode In letter_node.Nodes
						If CType(CType(series_node.Tag,Object())(0),API.Serie) Is CType(CType(ShowedList.SelectedNode.Tag,Object())(0),API.Serie) Then
							panel1.Controls.Remove(ShowedList)
							RemoveHandler ShowedList.AfterSelect, AddressOf FindSelectedSeries
							ShowedList.Dispose()
							ShowedList = Nothing
							treeView1.Visible = True
							letter_node.Expand()
							treeView1.SelectedNode = series_node
							treeView1.SelectedNode.EnsureVisible()
							Exit For
						End If
					Next
					If ShowedList Is Nothing Then
						Exit For
					End If
				Next
			End If
		End Sub
		Private Sub HosterLinkKopierenToolStripMenuItemClick(sender As Object, e As EventArgs)
			Dim l As String = API.Watch(CType(CType(treeView1.SelectedNode.Tag,Object())(0),API.Hoster).Ids(0))
			Clipboard.SetText(l)
			MessageBox.Show("Link in Zwischenablage kopiert")
		End Sub
		Private Sub ToolStripStatusLabel1Click(sender As Object, e As EventArgs)
			GlobalDebugDiag.DebugDiag.ShowDebugView()
		End Sub
		Private Sub ToolStripButton4Click(sender As Object, e As EventArgs)
			filterBox.Text = ""
			Timer2Tick(Nothing,Nothing)
		End Sub
		Private Sub StartToolStripMenuItemClick(sender As Object, e As EventArgs)
			If listView1.SelectedItems.Count > 1 Then
				For Each i As DLVI In listView1.SelectedItems
					If Not i.Active And Not i.Done Then
						i.BeginDownload()
					End If
				Next
			ElseIf listView1.FocusedItem IsNot Nothing Then
				Dim i As DLVI = CType(listView1.FocusedItem,DLVI)
				If Not i.Active And Not i.Done Then
					i.BeginDownload()
				End If
			End If
		End Sub
		Private Sub ContextMenuStrip2Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)
			startToolStripMenuItem.Enabled = False
			stopToolStripMenuItem.Enabled = False
			entfernenToolStripMenuItem.Enabled = False
			neustartenToolStripMenuItem.Enabled = False
			
			If listView1.SelectedItems.Count > 1 Then
				Dim show_start As Boolean = False
				Dim show_stop As Boolean = False
				Dim show_remove As Boolean = False
				Dim show_restart As Boolean = False
				
				For Each i As DLVI In listView1.SelectedItems
					If i.Done Then
						show_remove = True
						show_restart = True
					End If
					If i.Active Then
						show_stop = True
					End If
					If Not i.Active And Not i.Done Then
						show_start = True
						show_remove = True
					End If
				Next
				startToolStripMenuItem.Enabled = show_start
				stopToolStripMenuItem.Enabled = show_stop
				entfernenToolStripMenuItem.Enabled = show_remove
				neustartenToolStripMenuItem.Enabled = show_restart
			ElseIf listView1.FocusedItem IsNot Nothing Then
				Dim d As DLVI = CType(listView1.FocusedItem,DLVI)
				If Not d.Active And Not d.Done Then
					startToolStripMenuItem.Enabled = True
					entfernenToolStripMenuItem.Enabled = True
				ElseIf d.Active Then
					stopToolStripMenuItem.Enabled = True
					neustartenToolStripMenuItem.Enabled = True
				ElseIf d.Done Then
					neustartenToolStripMenuItem.Enabled = True
					entfernenToolStripMenuItem.Enabled = True
				End If
			End If
		End Sub
		Private Sub StopToolStripMenuItemClick(sender As Object, e As EventArgs)
			If listView1.SelectedItems.Count > 1 Then
				For Each i As DLVI In listView1.SelectedItems
					If i.Active Then
						i.StopDownload()
					End If
				Next
			ElseIf listView1.FocusedItem IsNot Nothing Then
				Dim i As DLVI = CType(listView1.FocusedItem,DLVI)
				If i.Active Then
					i.StopDownload()
				End If
			End If
		End Sub
		Private Sub EntfernenToolStripMenuItemClick(sender As Object, e As EventArgs)
			If listView1.SelectedItems.Count > 1 Then
				For Each i As DLVI In listView1.SelectedItems
					If Not i.Active Or i.Done Then
						listView1.Items.Remove(i)
					End If
				Next
			ElseIf listView1.FocusedItem IsNot Nothing Then
				Dim i As DLVI = CType(listView1.FocusedItem,DLVI)
				If Not i.Active Or i.Done Then
					listView1.Items.Remove(i)
				End If
			End If
		End Sub
		Private Sub NeustartenToolStripMenuItemClick(sender As Object, e As EventArgs)
			If listView1.SelectedItems.Count > 1 Then
				For Each i As DLVI In listView1.SelectedItems
					If Not i.Active And i.Done Then
						i.BeginDownload()
					End If
				Next
			ElseIf listView1.FocusedItem IsNot Nothing Then
				Dim i As DLVI = CType(listView1.FocusedItem,DLVI)
				If Not i.Active And i.Done Then
					i.BeginDownload()
				End If
			End If
		End Sub
		Private Sub MainFormLoad(sender As Object, e As EventArgs)
			AddHandler GlobalDebugDiag.DebugDiag.MessageFired, AddressOf HandleLogEntries
			Dim letters() As Char = "#ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()
			For Each i As Char In letters
				SortingList.Add(i,New List(Of API.Serie))
				Dim ttn As New TreeNode(i.ToString())
				treeView1.Nodes.Add(ttn)
				treeNodeList.Add(i,ttn)
			Next
			ListView1SizeChanged(Nothing,Nothing)
			GlobalDebugDiag.DebugDiag.Log("Versions-Check ...","UPDATER")
			Dim t As New System.Threading.Thread(AddressOf CheckForUpdate)
			t.Start()
		End Sub
		Private Delegate Sub ShowDL()
		Private Sub ShowDLB()
			If menuStrip1.InvokeRequired Then
				menuStrip1.Invoke(New ShowDL(AddressOf ShowDLB))
			Else
				neueVersionHerunterladenToolStripMenuItem.Visible = True
			End If
		End Sub
		Private Sub CheckForUpdate()
			Dim dl As String = UpdateDownloadLink
			If dl <> "" Then
				ShowDLB()
			End If
			QuickStart()
		End Sub
		Private Delegate Sub BeginLoadingSeriesD()
		Private Sub QuickStart()
			Me.Invoke(New BeginLoadingSeriesD(AddressOf BeginLoadingSeries))
		End Sub
		Private Sub ListView1SizeChanged(sender As Object, e As EventArgs)
			columnHeader1.Width = listView1.Width - (SystemInformation.VerticalScrollBarWidth + columnHeader2.Width + (SystemInformation.BorderSize.Width * 2))
		End Sub
		Private Sub TreeView1AfterSelect(sender As Object, e As TreeViewEventArgs)
			timer1.Stop()
			timer1.Enabled = False
			timer1.Enabled = True
			timer1.Start()
		End Sub
		Private Sub Timer1Tick(sender As Object, e As EventArgs)
			timer1.Stop()
			timer1.Enabled = False
			Dim loadSeasonsThread As New Thread(AddressOf DoLoadSeasons)
			loadSeasonsThread.Start()
		End Sub
		Private Sub TreeView1DoubleClick(sender As Object, e As EventArgs)
			If treeView1.SelectedNode.Level = 2 Then
				DoLoadSeasons() ' Staffelepisoden laden ...
				Dim mr As TreeNode = treeView1.SelectedNode
				Dim nl As TreeNodeCollection = mr.Nodes
				For Each n As TreeNode In nl
					Dim Hosters As New List(Of API.Hoster)
					Dim epi As API.Episode = CType(CType(n.Tag,Object())(0),API.Episode)
					treeView1.SelectedNode = n
					DoLoadSeasons(True)
					treeView1.SelectedNode.Collapse()
					For Each n2 As TreeNode In n.Nodes
						Hosters.Add(CType(CType(n2.Tag,Object())(0),API.Hoster))
					Next
					GlobalDebugDiag.DebugDiag.Log("Füge Episode """ & epi.Name & """ der Staffel """ & epi.Season.Number & """ der Serie """ & epi.Season.Serie.Name & """ der Downloadliste hinzu ...","Information")
					Dim dlvi_item As New DLVI(epi,Hosters)
					dlvi_item.SubItems.Add("Wartend ...")
					listView1.Items.Add(dlvi_item)
				Next
				treeView1.SelectedNode = mr
			ElseIf treeView1.SelectedNode.Level = 3 Then
				DoLoadSeasons() ' Episoden laden ...
				Dim mr As TreeNode = treeView1.SelectedNode
				Dim nl As TreeNodeCollection = mr.Nodes
				Dim Hosters As New List(Of API.Hoster)
				For Each n2 As TreeNode In nl
					Hosters.Add(CType(CType(n2.Tag,Object())(0),API.Hoster))
				Next
				Dim epi As API.Episode = CType(CType(mr.Tag,Object())(0),API.Episode)
				GlobalDebugDiag.DebugDiag.Log("Füge Episode """ & epi.Name & """ der Staffel """ & epi.Season.Number & """ der Serie """ & epi.Season.Serie.Name & """ der Downloadliste hinzu ...","Information")
				Dim dlvi_item As New DLVI(epi,Hosters)
				dlvi_item.SubItems.Add("Wartend ...")
				listView1.Items.Add(dlvi_item)
			End If
		End Sub
		Private Sub ContextMenuStrip1Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)
			Try
				Dim can As Boolean = True
				If treeView1.SelectedNode.Level = 2 Then
					gesamteStaffelHerunterladenToolStripMenuItem.Visible = True
					can = False
				Else
					gesamteStaffelHerunterladenToolStripMenuItem.Visible = False
				End If
				If treeView1.SelectedNode.Level = 3 Then
					episodeHerunterladenToolStripMenuItem.Visible = True
					can = False
				Else
					episodeHerunterladenToolStripMenuItem.Visible = False
				End If
				If treeView1.SelectedNode.Level = 4 Then
					hosterLinkKopierenToolStripMenuItem.Visible = True
					can = False
				Else
					hosterLinkKopierenToolStripMenuItem.Visible = False
				End If
				e.Cancel = can
			Catch
				gesamteStaffelHerunterladenToolStripMenuItem.Visible = False
				episodeHerunterladenToolStripMenuItem.Visible = False
				hosterLinkKopierenToolStripMenuItem.Visible = False
				e.Cancel = True
			End Try
		End Sub
	#End Region
	#Region " Internal Functions "
		Private Overloads Function HosterSupported(ByVal hosterName As String) As Boolean
			Return(HosterScripts.HosterSupported(hosterName))
		End Function
		Private Overloads Function HosterSupported(ByVal domain As Uri) As Boolean
			Return(HosterScripts.HosterSupported(domain))
		End Function
	#End Region
	#Region " Delegates "
		Private Delegate Sub HandleLogEntriesD(ByVal msg As String,ByVal type As String,ByVal subcat As String)
		Private Sub HandleLogEntries(ByVal msg As String,ByVal type As String,ByVal subcat As String)
			If subcat = "MAIN" Then
				If statusStrip1.InvokeRequired Then
					statusStrip1.Invoke(New HandleLogEntriesD(AddressOf HandleLogEntries),New Object() {msg,type,subcat})
				Else
					toolStripStatusLabel1.Text = DateTime.Now.ToString() & " > " & msg
				End If
			End If
		End Sub
		
		Private Delegate Sub FinishedLoadingSeriesD()
		Private Sub FinishedLoadingSeries()
			Dim sortingKeys(SortingList.Keys.Count) As Char
			SortingList.Keys.CopyTo(sortingKeys,0)
			Dim suggestList As New List(Of String)
			For Each j As Char In sortingKeys
				Try
					Dim sl As List(Of API.Serie) = CType(SortingList.Item(j),List(Of API.Serie))
					For Each k As API.Serie In sl
						CType(treeNodeList.Item(j),TreeNode).Nodes.Add(k.Name).Tag = New Object() {k}
						suggestList.Add(k.Name)
					Next
				Catch
				End Try
			Next
			Dim acsc As New AutoCompleteStringCollection()
			acsc.AddRange(suggestList.ToArray())
			filterBox.AutoCompleteCustomSource = acsc
			filterBox.AutoCompleteSource = AutoCompleteSource.CustomSource
			filterBox.AutoCompleteMode = AutoCompleteMode.Suggest
		End Sub
		
		Private Delegate Function GetSelectedEpisodeD() As API.Episode
		Private Function GetSelectedEpisode() As API.Episode
			If treeView1.InvokeRequired Then
				Return(CType(treeView1.Invoke(New GetSelectedEpisodeD(AddressOf GetSelectedEpisode)),API.Episode))
			Else
				Return(CType(CType(treeView1.SelectedNode.Tag,Object())(0),API.Episode))
			End If
		End Function
		
		Private Delegate Function GetSelectedSerieD() As API.Serie
		Private Function GetSelectedSerie() As API.Serie
			If treeView1.InvokeRequired Then
				Return(CType(treeView1.Invoke(New GetSelectedSerieD(AddressOf GetSelectedSerie)),API.Serie))
			Else
				Return(CType(CType(treeView1.SelectedNode.Tag,Object())(0),API.Serie))
			End If
		End Function
		
		Private Delegate Function GetSelectedSeasonD() As API.Season
		Private Function GetSelectedSeason() As API.Season
			If treeView1.InvokeRequired Then
				Return(CType(treeView1.Invoke(New GetSelectedSeasonD(AddressOf GetSelectedSeason)),API.Season))
			Else
				Return(CType(CType(treeView1.SelectedNode.Tag,Object())(0),API.Season))
			End If
		End Function
		
		Private Delegate Function GetSelectedNodeD() As TreeNode
		Private Function GetSelectedNode() As TreeNode
			If treeView1.InvokeRequired Then
				Return(CType(treeView1.Invoke(New GetSelectedNodeD(AddressOf GetSelectedNode)),TreeNode))
			Else
				Return(treeView1.SelectedNode)
			End If
		End Function
		
		Private Delegate Sub AddToTreeNodeD(ByRef tn As TreeNode,ByVal season As API.Season)
		Private Sub AddToTreeNode(ByRef tn As TreeNode,ByVal season As API.Season)
			If treeView1.InvokeRequired Then
				treeView1.Invoke(New AddToTreeNodeD(AddressOf AddToTreeNode),New Object() {tn,season})
			Else
				Dim bezeichnung As String = "Staffel " & season.Number
				If season.Number = 0 Then
					bezeichnung = "Filme"
				End If
				tn.Nodes.Add(bezeichnung).Tag = New Object() {season}
			End If
		End Sub
		
		Private Delegate Sub AddToTreeNodeEpisodeD(ByRef tn As TreeNode,ByVal episode As API.Episode)
		Private Sub AddToTreeNodeEpisode(ByRef tn As TreeNode,ByVal episode As API.Episode)
			If treeView1.InvokeRequired Then
				treeView1.Invoke(New AddToTreeNodeEpisodeD(AddressOf AddToTreeNodeEpisode),New Object() {tn,episode})
			Else
				Dim nvc As New NameValueCollection()
				nvc.Add("%serie%",episode.Season.Serie.Name)
				nvc.Add("%seasonNN%",NameGenerator.cc(episode.Season.Number))
				nvc.Add("%seasonN%",episode.Season.Number.ToString())
				nvc.Add("%episodeNN%",NameGenerator.cc(episode.Episode))
				nvc.Add("%episodeN%",episode.Episode.ToString())
				nvc.Add("%name%",episode.Name)
				nvc.Add("%nameE%",NameGenerator.CleanFN(episode.NameEnglish))
				nvc.Add("%nameG%",NameGenerator.CleanFN(episode.NameGerman))
				
				tn.Nodes.Add(NameGenerator.BuildName(nvc)).Tag = New Object() {episode}
			End If
		End Sub
		
		Private Delegate Sub AddToTreeNodeHosterD(ByRef tn As TreeNode,ByVal hoster As API.Hoster)
		Private Sub AddToTreeNodeHoster(ByRef tn As TreeNode,ByVal hoster As API.Hoster)
			If treeView1.InvokeRequired Then
				treeView1.Invoke(New AddToTreeNodeHosterD(AddressOf AddToTreeNodeHoster),New Object() {tn,hoster})
			Else
				Dim n As TreeNode = tn.Nodes.Add(hoster.Name)
				n.Tag = New Object() {hoster}
				If HosterSupported(hoster.Name) Then
					n.ForeColor = Color.DarkGreen
				Else
					n.ForeColor = Color.DarkRed
				End If
			End If
		End Sub
		
		Private Delegate Sub SetTNColorD(ByVal tn As TreeNode,ByVal col As Color)
		Private Sub SetTNColor(ByVal tn As TreeNode,ByVal col As Color)
			If treeView1.InvokeRequired Then
				treeView1.Invoke(New SetTNColorD(AddressOf SetTNColor),New Object() {tn,col})
			Else
				tn.ForeColor = col
			End If
		End Sub
		
		Private Delegate Sub DoExpandD(ByRef tn As TreeNode)
		Private Sub DoExpand(ByRef tn As TreeNode)
			If treeView1.InvokeRequired Then
				treeView1.Invoke(New DoExpandD(AddressOf DoExpand),New Object() {tn})
			Else
				tn.Expand()
			End If
		End Sub
	#End Region
	#Region " Asynchrone Ladefunktionen "
		Private Sub BeginLoadingSeries()
			Dim seriesLoadingThread As New Thread(AddressOf DoLoadingSeries)
			seriesLoadingThread.Start()
		End Sub
		Private Sub DoLoadingSeries()
			Dim sortingKeys(SortingList.Keys.Count) As Char
			SortingList.Keys.CopyTo(sortingKeys,0)
			Dim seriesList As New List(Of API.Serie)
			seriesList.AddRange(API.Series.GetSeries(True))
			Dim target_element As Char = CChar("#")
			For Each i As API.Serie In seriesList
				Dim n As String = i.Name.Trim()
				Dim l As Char = CChar(n.Substring(0,1).ToUpper())
				If Array.IndexOf(sortingKeys,l) > -1 Then
					target_element = l
				End If
				CType(SortingList.Item(target_element),List(Of API.Serie)).Add(i)
			Next
			Me.Invoke(New FinishedLoadingSeriesD(AddressOf FinishedLoadingSeries))
		End Sub
		Private Sub DoLoadSeasons(Optional ByVal SkipExpand As Boolean = False)
			Dim SelectedNode As TreeNode = GetSelectedNode()
			If SelectedNode.Level = 1 And SelectedNode.Nodes.Count = 0 And SelectedNode.ForeColor <> Color.Blue Then
				SetTNColor(SelectedNode,Color.Blue)
				Dim SelectedSerie As API.Serie = GetSelectedSerie()
				GlobalDebugDiag.DebugDiag.Log("Staffeln der Serie """ & SelectedSerie.Name & """ werden geladen ...","Information")
				Dim SeasonList As New List(Of API.Season)
				SeasonList.AddRange(SelectedSerie.GetSeasons())
				For Each Season As API.Season In SeasonList
					AddToTreeNode(SelectedNode,Season)
				Next
				GlobalDebugDiag.DebugDiag.Log("Staffeln der Serie """ & SelectedSerie.Name & """ geladen","Information")
				If Not SkipExpand Then
					DoExpand(SelectedNode)
				End If
				SetTNColor(SelectedNode,treeView1.ForeColor)
			ElseIf SelectedNode.Level = 2 And SelectedNode.Nodes.Count = 0 And SelectedNode.ForeColor <> Color.Blue  Then
				SetTNColor(SelectedNode,Color.Blue)
				Dim SelectedSeason As API.Season = GetSelectedSeason()
				GlobalDebugDiag.DebugDiag.Log("Episoden der Staffel """ & SelectedSeason.Number & """ der Serie """ & SelectedSeason.Serie.Name & """ werden geladen ...","Information")
				Dim EpisodeList As New List(Of API.Episode)
				EpisodeList.AddRange(SelectedSeason.GetEpisodes())
				For Each Episode As API.Episode In EpisodeList
					AddToTreeNodeEpisode(SelectedNode,Episode)
				Next
				GlobalDebugDiag.DebugDiag.Log("Episoden der Staffel """ & SelectedSeason.Number & """ der Serie """ & SelectedSeason.Serie.Name & """ geladen","Information")
				If Not SkipExpand Then
					DoExpand(SelectedNode)
				End If
				SetTNColor(SelectedNode,treeView1.ForeColor)
			ElseIf SelectedNode.Level = 3 And SelectedNode.Nodes.Count = 0 And SelectedNode.ForeColor <> Color.Blue  Then
				SetTNColor(SelectedNode,Color.Blue)
				Dim SelectedEpisode As API.Episode = GetSelectedEpisode()
				GlobalDebugDiag.DebugDiag.Log("Hoster der Episode """ & SelectedEpisode.Name & """ der Staffel """ & SelectedEpisode.Season.Number & """ der Serie """ & SelectedEpisode.Season.Serie.Name & """ wird geladen ...","Information")
				Dim HosterList As New List(Of API.Hoster)
				HosterList.AddRange(SelectedEpisode.GetHoster())
				For Each Hoster As API.Hoster In HosterList
					AddToTreeNodeHoster(SelectedNode,Hoster)
				Next
				GlobalDebugDiag.DebugDiag.Log("Hoster der Episode """ & SelectedEpisode.Name & """ der Staffel """ & SelectedEpisode.Season.Number & """ der Serie """ & SelectedEpisode.Season.Serie.Name & """ geladen","Information")
				If Not SkipExpand Then
					DoExpand(SelectedNode)
				End If
				SetTNColor(SelectedNode,treeView1.ForeColor)
			ElseIf SelectedNode.Level = 4 And SelectedNode.Nodes.Count = 0 And SelectedNode.ForeColor <> Color.Blue  Then
			End If
		End Sub
	#End Region
	
	Sub EinstellungenToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim cw As New Config()
		cw.Show()
	End Sub
	Private dl_cache As String = ""
	Private ReadOnly Property UpdateDownloadLink As String
		Get
			Try
				If dl_cache = "" Then
					Dim apiLink As String = "https://www.mediafire.com/api/1.4/folder/get_content.php?r=sbpt&content_type=files&filter=all&order_by=created&order_direction=asc&chunk=1&version=1.5&folder_key=4dbfihe76a6k4&response_format=xml"
					Dim x As New XmlDocument()
					Dim wc As New System.Net.WebClient()
					x.LoadXml(wc.DownloadString(apiLink).Trim())
					Dim n As XmlNode = Nothing
					Dim r As XmlNodeList = x.ChildNodes(1).ChildNodes(2).ChildNodes(3).ChildNodes
					Dim current_newest As XmlNode = Nothing
					For Each n In r
						Dim zeit As DateTime = DateTime.Parse(n.ChildNodes(6).InnerText)
						If current_newest Is Nothing Then
							current_newest = n
						Else
							Dim letzte As DateTime = DateTime.Parse(current_newest.ChildNodes(6).InnerText)
							If DateTime.Compare(letzte,zeit) < 0 Then
								current_newest = n
							End If
						End If
					Next
					Dim server_date As DateTime = DateTime.Parse(current_newest.ChildNodes(6).InnerText)
					server_date = server_date.AddHours(6)
					If System.TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now) Then
						server_date = server_date.AddHours(1)
					End If
					GlobalDebugDiag.DebugDiag.Log("Letztes Setup wurde hochgeladen: " & server_date.ToString(),"UPDATER")
					If DateTime.Compare(server_date,selfTimestamp) > -1 Then
						dl_cache = current_newest.ChildNodes(17).ChildNodes(0).InnerText
					End If
				End If
				Return(dl_cache)
			Catch
				Return("")
			End Try
		End Get
	End Property
	
	Sub NeueVersionHerunterladenToolStripMenuItemClick(sender As Object, e As EventArgs)
		System.Diagnostics.Process.Start(Me.UpdateDownloadLink)
	End Sub
End Class
