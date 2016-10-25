Imports System.Net
Imports System.Collections.Specialized
Imports Microsoft.Win32
Imports System.IO
Imports System.Drawing
Imports Newtonsoft.Json
Imports System.Xml

Public Class API
	Public Shared LockTime As Double = 0.7
	Public Shared APILINK As String = "http://bs.to/api/"
	Private Shared AlreadyWriting As Boolean = False
	Public Shared Property LastCall As Long
		Get
			Dim rsl As String = GlobalConfig.GlobalConfig.DBSelect("api.lastcall","0",False)
			If rsl = "" Then
				Return(0)
			Else
				Return(CLng(rsl))
			End If
		End Get
		Set
			GlobalConfig.GlobalConfig.DBInsert("api.lastcall",Value.ToString())
		End Set
	End Property
	Public Shared Sub ValidateThread()
		Dim usespeedlimit As String = GlobalConfig.GlobalConfig.DBSelect("api.speedlimit","0",False)
		If usespeedlimit = "" Then
			usespeedlimit = "0"
		End If
		If usespeedlimit = "1" Then
			Dim lastCall_v As Double = API.LastCall / 10000000
			Dim jetzt As Double = DateTime.Now.ToFileTime() / 10000000
			Dim diff As Double = jetzt - lastCall_v
			If diff < API.LockTime Then
				System.Threading.Thread.Sleep(CInt(Math.Ceiling((API.LockTime - diff) * 1000)))
			End If
			API.LastCall = DateTime.Now.ToFileTime()
		End If
	End Sub
	Public Shared Function GetSerieName(ByVal id As Integer) As String
		Dim sr() As API.Serie = API.Series.GetSeries()
		Dim i As API.Serie = Nothing
		Dim result As String = ""
		For Each i In sr
			If i.ID = id Then
				result = i.Name
				Exit For
			End If
		Next
		Return(result)
	End Function
	Friend Shared Sub ToLog(ByVal s As String)
		GlobalDebugDiag.DebugDiag.Log(s,"Information")
	End Sub
	Public Shared ReadOnly Property DefaultCover As Image
		Get
			Dim res As New System.Resources.ResourceManager("Ressources", System.Reflection.Assembly.GetExecutingAssembly())
			Return(CType(res.GetObject("CoverDefault"),Image))
		End Get
	End Property
	Public Class Series
		Private Shared Function cc(ByVal n As Integer) As String
			Dim outp As String = n.ToString()
			While outp.Length < 5
				outp = "0" & outp
			End While
			Return(outp)
		End Function
		Public Shared Function GetNewest(Optional ByVal Count As Integer = 15,Optional ByVal SkipCache As Boolean = False) As API.Serie()
			Dim sli() As API.Serie = GetSeries(SkipCache)
			
			Dim hlvi As New System.Windows.Forms.ListView()
			hlvi.Sorting = System.Windows.Forms.SortOrder.Descending
			
			Dim i As API.Serie = Nothing
			Dim a As New List(Of Integer)
			For Each i In sli
				a.Add(i.ID)
			Next
			Dim arr() As Integer = a.ToArray()
			Array.Sort(arr)
			
			Dim outp As New List(Of API.Serie)
			Dim n As Integer = 0
			For n = arr.Length - 1 To arr.Length - Count - 1 Step - 1
				outp.Add(New API.Serie("",arr(n)))
			Next
			
			hlvi.Dispose()
			
			Return(outp.ToArray())
		End Function
		Public Shared Function GetSeries(Optional ByVal SkipCache As Boolean = False) As API.Serie()
			API.ToLog("Loading whole Series list ...")
			Dim l As New List(Of API.Serie)
			Dim wc As New WebClientSpecial()
			wc.UseDefaultCredentials = true
			wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
			Dim rsl As String = ""
			Try
				rsl = wc.DownloadStringCached(APILINK & "series/" & API.GetUHash(),SkipCache)
				wc.Dispose()
			Catch ex As Exception
				Throw ex
			End Try
			If rsl = "" Then
				Throw New Exception("API nicht erreichbar. Cache nicht vorhanden.")
			End If
			Try
				Dim xdoc As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode("{""bsapi"":" & rsl & "}","bsapi")
				Dim entries As System.Xml.XmlNodeList = xdoc.ChildNodes(0).ChildNodes
				Dim entry As System.Xml.XmlNode = Nothing
				For Each entry In entries
					Dim entry_name As String = entry.ChildNodes(0).InnerText.Trim()
					Dim entry_id As Integer = Integer.Parse(entry.ChildNodes(1).InnerText.Trim())
					l.Add(New API.Serie(entry_name,entry_id))
				Next
				API.ToLog("Loaded " & l.Count & " Series")
				Return(l.ToArray())
			Catch ex As Exception
				API.ToLog("Error!")
				API.ToLog(ex.ToString())
				API.ToLog("Retry")
				Return(GetSeries())
			End Try
		End Function
	End Class
	Public Class Serie
		Private _this_name As String = ""
		Private _this_id As Integer = 0
		Private _desc As String = ""
		Private _img As Image = Nothing
		Private _genres() As String = {}
		Public ReadOnly Property Image As Image
			Get
				Dim res As New System.Resources.ResourceManager("Ressources", System.Reflection.Assembly.GetExecutingAssembly())
				Dim _bigimg As Image = CType(res.GetObject("CoverDefault"),Image)
				Dim _overlay As Image = CType(res.GetObject("CoverOverlay"),Image)
				If _img Is Nothing Then
					Dim rect1 As New Rectangle(0, 0, 180, 34)
					Dim rect2 As New Rectangle(1, 1, 180, 34)
					
					Dim cachePath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\cache\"
					Dim cachePathInfo As New System.IO.DirectoryInfo(cachePath)
					Try
						cachePathInfo.Create()
					Catch
					End Try
					Dim cacheFile As String = cachePath & md5.MD5Hash(_this_id.ToString()) & ".cache.jpg"
					Try
						If File.Exists(cacheFile) Then
							_img = Image.FromFile(cacheFile)
						Else
							Dim hwc As HttpWebRequest = CType(HttpWebRequest.Create("http://s.bs.to/img/cover/" & _this_id & ".jpg"),HttpWebRequest)
							hwc.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
							Dim hwr As HttpWebResponse = CType(hwc.GetResponse(),HttpWebResponse)
							Dim tmi As Image = Image.FromStream(hwr.GetResponseStream())
							_img = New Bitmap(180,270)
							Dim g As Graphics = Graphics.FromImage(_img)
							g.DrawImageUnscaledAndClipped(tmi,New Rectangle(0,34,180,236))
							g.DrawImageUnscaledAndClipped(_overlay,New Rectangle(0,0,180,270))
							
							Dim fs As Single = 12
							
							Dim font1 As New Font("Arial", fs, FontStyle.Bold, GraphicsUnit.Point)
							Dim stringFormat As New StringFormat()
							stringFormat.Alignment = StringAlignment.Center
							stringFormat.LineAlignment = StringAlignment.Center
							
							While g.MeasureString(_this_name,font1).Width > 180
								fs = CSng(fs * 0.8)
								font1 = New Font("Arial", fs, FontStyle.Bold, GraphicsUnit.Point)
							End While
							
							g.DrawString(_this_name, font1, Brushes.Black, rect2, stringFormat)
							g.DrawString(_this_name, font1, Brushes.White, rect1, stringFormat)
							
							g.Flush()
							g.Dispose()
							hwr.Close()
							
							_img.Save(cacheFile,System.Drawing.Imaging.ImageFormat.Jpeg)
						End If
					Catch ex As Exception
						_img = _bigimg
						Dim g As Graphics = Graphics.FromImage(_img)
						
						Dim fs As Single = 12
						
						Dim font1 As New Font("Arial", fs, FontStyle.Bold, GraphicsUnit.Point)
						Dim stringFormat As New StringFormat()
						stringFormat.Alignment = StringAlignment.Center
						stringFormat.LineAlignment = StringAlignment.Center
						
						While g.MeasureString(_this_name,font1).Width > 180
							fs = CSng(fs * 0.8)
							font1 = New Font("Arial", fs, FontStyle.Bold, GraphicsUnit.Point)
						End While

						g.DrawString(_this_name, font1, Brushes.Black, rect2, stringFormat)
						g.DrawString(_this_name, font1, Brushes.White, rect1, stringFormat)
						
						g.Flush()
						g.Dispose()
						
						_img.Save(cacheFile,System.Drawing.Imaging.ImageFormat.Jpeg)
					End Try
				End If
				Return(_img)
			End Get
		End Property
		Public ReadOnly Property Name As String
			Get
				Return(_this_name)
			End Get
		End Property
		Public ReadOnly Property ID As Integer
			Get
				Return(_this_id)
			End Get
		End Property
		Public ReadOnly Property Description As String
			Get
				API.ToLog("Return Description")
				Return(_desc)
			End Get
		End Property
		Public ReadOnly Property Genres As String()
			Get
				Return(_genres)
			End Get
		End Property
		Public Sub New(ByVal addname As String,ByVal addid As Integer)
			_this_name = addname
			_this_id = addid
			If _this_name = "" And _this_id > 0 Then
				_this_name = API.GetSerieName(_this_id)
			End If
		End Sub
		Public Function GetSeasons(Optional ByVal IgnoreCache As Boolean = False) As API.Season()
			API.ToLog("Loading Seasons of " & Me.Name)
			Dim l As New List(Of API.Season)
			Dim wc As New WebClientSpecial()
			wc.UseDefaultCredentials = true
			wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
			Dim rsl As String = wc.DownloadStringCached(APILINK & "series/" & _this_id & "/1/" & API.GetUHash(),IgnoreCache)
			wc.Dispose()
			Dim xdoc As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode("{""bsapi"":" & rsl & "}","bsapi")
			Dim AnzahlFilme As Integer = Integer.Parse(xdoc.ChildNodes(0).ChildNodes(0).ChildNodes(0).ChildNodes(6).InnerXml)
			Dim AnzahlStaffeln As Integer = Integer.Parse(xdoc.ChildNodes(0).ChildNodes(0).ChildNodes(0).ChildNodes(7).InnerXml)
			Dim Beschreibung As String = xdoc.ChildNodes(0).ChildNodes(0).ChildNodes(0).ChildNodes(3).InnerXml
			_desc = Beschreibung
			Dim cn As XmlNode = Nothing
			Dim gg As XmlNode = xdoc.ChildNodes(0).ChildNodes(0).ChildNodes(0).ChildNodes(8)
			Dim offs As Integer = 0
			Dim rgen As Integer = -1
			For offs = 0 To gg.ChildNodes.Count - 1
				If gg.ChildNodes(offs).Name = "genre" Then
					rgen = offs
					Exit For
				End If
			Next
			offs = 0
			Dim rgen2 As Integer = -1
			For offs = 0 To gg.ChildNodes.Count - 1
				If gg.ChildNodes(offs).Name = "main_genre" Then
					rgen2 = offs
					Exit For
				End If
			Next
			Dim genresTemp As New List(Of String)
			If rgen > -1 Then
				For Each cn In gg.ChildNodes(rgen).ChildNodes
					Dim data As String = cn.InnerText
					genresTemp.Add(data)
				Next
			End If
			If rgen2 > -1 Then
				genresTemp.Add(gg.ChildNodes(rgen2).InnerText)
			End If
			_genres = genresTemp.ToArray()
			API.ToLog("Movies " & AnzahlFilme)
			If AnzahlFilme > 0 Then
				Dim s As New API.Season(0,_this_id,_this_name)
				s.Serie = Me
				l.Add(s)
			End If
			Dim n As Integer = 0
			API.ToLog("Seasons " & AnzahlStaffeln)
			For n = 1 To AnzahlStaffeln
				Dim s As New API.Season(n,_this_id,_this_name)
				s.Serie = Me
				l.Add(s)
			Next
			Return(l.ToArray())
		End Function
	End Class
	Public Class Season
		Private _this_number As Integer = -2
		Private _this_series_id As Integer = 0
		Private _this_series_name As String = ""
		Public Serie As API.Serie = Nothing
		Public ReadOnly Property Number As Integer
			Get
				Return(_this_number)
			End Get
		End Property
		Public Sub New(ByVal addnumber As Integer,ByVal SeriesID As Integer,ByVal SeriesName As String)
			_this_number = addnumber
			_this_series_id = SeriesID
			_this_series_name = SeriesName
		End Sub
		Public Function GetEpisodes(Optional ByVal IgnoreCache As Boolean = False) As API.Episode()
			API.ToLog("Load Episodes of Season " & Me.Number & " of Serie " & Me.Serie.Name)
			Dim l As New List(Of API.Episode)
			Dim wc As New WebClientSpecial()
			wc.UseDefaultCredentials = true
			wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
			
			Dim rsl As String = wc.DownloadStringCached(APILINK & "series/" & _this_series_id & "/" & _this_number  & "/" & API.GetUHash(),IgnoreCache)
			wc.Dispose()
			Dim xdoc As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode("{""bsapi"":" & rsl & "}","bsapi")
			
			Dim entries As System.Xml.XmlNodeList = xdoc.ChildNodes(0).ChildNodes(0).ChildNodes
			Dim i As System.Xml.XmlNode = Nothing
			Dim number As Integer = 0
			Dim i2 As Integer = 2
			API.ToLog("Found " & (entries.Count - 2) & " Episodes")
			For i2 = 2 To entries.Count - 1
				i = entries(i2)
				number += 1
				Dim gerName As String = i.ChildNodes(0).InnerText.Trim()
				Dim engName As String = i.ChildNodes(1).InnerText.Trim()
				Dim epiID As Integer = Integer.Parse(i.ChildNodes(2).InnerText.Trim())
				Dim watched As Boolean = (i.ChildNodes(3).InnerText.Trim() = "1")
				Dim ep As New API.Episode(gerName,engName,epiID,watched,_this_series_id,_this_series_name,_this_number,number)
				ep.Season = Me
				API.ToLog("Added Episode " & ep.Name)
				l.Add(ep)
			Next
			Return(l.ToArray())
		End Function
	End Class
	Public Class Episode
		Private _this_ger_name As String = ""
		Private _this_eng_name As String = ""
		Private _this_id As Integer = -1
		Private _this_watched As Boolean = False
		Private _this_serie As Integer = -1
		Private _this_serie_name As String = ""
		Private _this_season As Integer = -1
		Private _this_index As Integer
		Public Season As API.Season = Nothing
		Public ReadOnly Property Episode As Integer
			Get
				Return(_this_index)
			End Get
		End Property
		Public ReadOnly Property Name As String
			Get
				If _this_ger_name <> "" Then
					Return(Me.NameGerman)
				Else
					Return(Me.NameEnglish)
				End If
			End Get
		End Property
		Public ReadOnly Property NameEnglish As String
			Get
				Return(_this_eng_name)
			End Get
		End Property
		Public ReadOnly Property NameGerman As String
			Get
				Return(_this_ger_name)
			End Get
		End Property
		Public ReadOnly Property ID As Integer
			Get
				Return(_this_id)
			End Get
		End Property
		Public ReadOnly Property Watched As Boolean
			Get
				Return(_this_watched)
			End Get
		End Property
		Public Sub New(ByVal addname_ger As String,ByVal addname_eng As String,ByVal addid As Integer,ByVal addwatched As Boolean,ByVal _serie As Integer,ByVal _serie_name As String,ByVal _season As Integer,ByVal epi As Integer)
			_this_ger_name = addname_ger
			_this_eng_name = addname_eng
			_this_id = addid
			_this_watched = addwatched
			_this_serie = _serie
			_this_serie_name = _serie_name
			_this_season = _season
			_this_index = epi
		End Sub
		Public Function GetHoster() As API.Hoster()
			API.ToLog("Load Hoster of " & Me.Name & " of Episode " & Me.Season.Serie.Name)
			Dim l As List(Of API.Hoster) = Nothing
			Dim wc As New WebClientSpecial()
			wc.UseDefaultCredentials = true
			wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
			
			Dim rsl As String = wc.DownloadStringCached(APILINK & "series/" & _this_serie & "/" & _this_season & "/" & _this_id & API.GetUHash(),True)
			Dim xdoc As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode("{""bsapi"":" & rsl & "}","bsapi")

			Dim hl As New List(Of API.Hoster)
			Dim i As System.Xml.XmlNode = Nothing
			Dim i2 As Integer = 0
			
			Dim collection As Xml.XmlNodeList = xdoc.ChildNodes(0).ChildNodes(0).ChildNodes
			
			For i2 = 2 To collection.Count - 1
				i = collection(i2)
				Dim name As String = i.ChildNodes(0).InnerText.Trim()
				Dim part As Integer = Integer.Parse(i.ChildNodes(1).InnerText.Trim())
				Dim id As Integer = Integer.Parse(i.ChildNodes(2).InnerText.Trim())
				API.ToLog("Hoster found: " & name & " (" & id & ")")
				hl.Add(New API.Hoster(name,id,Me,_this_serie_name,_this_season))
			Next
			l = hl
			Return(l.ToArray())
		End Function
		Private Function GetExplicitHoster(ByVal hl As List(Of API.Hoster),ByVal name As String) As API.Hoster
			Dim i As API.Hoster = Nothing
			Dim h As API.Hoster = Nothing
			For Each i In hl.ToArray()
				If i.Name = name Then
					h = i
					Exit For
				End If
			Next
			Return(h)
		End Function
	End Class
	Public Class Hoster
		Private _name As String = ""
		Private _ids As New List(Of Integer)
		Private _epiname As API.Episode = Nothing
		Private _this_serie_name As String = ""
		Private _this_season As Integer = -1
		Public ReadOnly Property Episode As API.Episode
			Get
				Return(_epiname)
			End Get
		End Property
		Public ReadOnly Property Name As String
			Get
				Return(_name)
			End Get
		End Property
		Public ReadOnly Property Serienname As String
			Get
				Return(_this_serie_name)
			End Get
		End Property
		Public ReadOnly Property Staffel As Integer
			Get
				Return(_this_season)
			End Get
		End Property
		Public ReadOnly Property Ids As Integer()
			Get
				Return(_ids.ToArray())
			End Get
		End Property
		Public ReadOnly Property Parts As Integer
			Get
				Return(_ids.Count)
			End Get
		End Property
		Public Sub New(ByVal addname As String,ByVal addid As Integer,ByVal addepisode As API.Episode,ByVal _series_name As String,ByVal _season As Integer)
			_epiname = addepisode
			_name = addname
			_this_serie_name = _series_name
			_this_season = _season
			_ids.Add(addid)
		End Sub
		Public Sub AddHoster(ByVal id As Integer)
			API.ToLog("Added Hoster-ID " & id & " to Hoster " & _name & " of Episode " & _epiname.Name & " of Serie " & _epiname.Season.Serie.Name)
			_ids.Add(id)
		End Sub
	End Class
	Public Shared Function Watch(ByVal id As Integer) As String
		API.ToLog("Watch single episode (Changes status of ""watched"")")
		Try
			Dim l As List(Of API.Hoster) = Nothing
			Dim wc As New WebClientSpecial()
			wc.UseDefaultCredentials = true
			wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
			
			Dim rsl As String = wc.DownloadStringCached(APILINK & "watch/" & id & API.GetUHash())
			API.ToLog("Get Hoster-Link")
			Dim xdoc As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode("{""bsapi"":" & rsl & "}","bsapi")
			Dim url As String = xdoc.ChildNodes(0).ChildNodes(0).ChildNodes(4).InnerText
			If url = "javascript:alert('Nicht möglich');" Then
				url = xdoc.ChildNodes(0).ChildNodes(0).ChildNodes(1).InnerText
				API.ToLog("Full-URL no direct link. Using alternate detection ...")
			End If
			API.ToLog("Return " & url)
			Return(url)
		Catch ex As Exception
			API.ToLog("Error!")
			API.ToLog(ex.ToString())
			Return("")
		End Try
	End Function
	Public Shared Function Login(ByVal username As String,ByVal password As String) As Boolean
		API.ToLog("Perform login")
		Dim wc As New WebClientSpecial()
		wc.OneShotTimeout = 3 * 60 * 1000 ' 3 Minuten
		wc.UseDefaultCredentials = true
		wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
		Dim nv As New System.Collections.Specialized.NameValueCollection()
		nv.Add("login[user]",username)
		nv.Add("login[pass]",password)
		API.ToLog("Username: " & username)
		API.ToLog("Password-Length: " & password.Length)
		API.ValidateThread()
		Dim rsl As String = System.Text.Encoding.UTF8.GetString(wc.UploadValues(APILINK & "login/",nv))
		Dim x As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode("{""bsapi"":" & rsl & "}","bsapi")
		If x.ChildNodes(0).ChildNodes(0).ChildNodes(0).Name = "error" Then
			API.ToLog("Login failed")
			API.ToLog("Reason: " & x.ChildNodes(0).ChildNodes(0).ChildNodes(0).InnerText)
			Return(false)
		Else
			API.ToLog("Login successful")
			API.ToLog(rsl)
			UserHash = x.ChildNodes(0).ChildNodes(0).ChildNodes(1).InnerText
			UserName = x.ChildNodes(0).ChildNodes(0).ChildNodes(0).InnerText
			Return(true)
		End If
	End Function
	Public Shared Sub Logout()
		API.ToLog("Logout")
		Dim wc As New WebClientSpecial()
		wc.UseDefaultCredentials = true
		wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
		wc.DownloadStringCached(APILINK & "logout/")
		UserHash = ""
		UserName = ""
		API.ToLog("No Results required. Logout done.")
	End Sub
	Public Shared Property UserHash As String
		Get
			Return(GlobalConfig.GlobalConfig.DBSelect("userHash","",False))
		End Get
		Set
			GlobalConfig.GlobalConfig.DBInsert("userHash",Value)
		End Set
	End Property
	Public Shared Property UserName As String
		Get
			Return(GlobalConfig.GlobalConfig.DBSelect("userName","",False))
		End Get
		Set
			GlobalConfig.GlobalConfig.DBInsert("userName",Value)
		End Set
	End Property
	Private Shared Function GetUHash() As String
		If UserHash = "" Then
			Return("")
		Else
			Return("?s=" & UserHash)
		End If
	End Function
	Public Shared ReadOnly Property Favorites As API.Serie()
		Get
			API.ToLog("Loading Favorites")
			Dim d As New List(Of API.Serie)
			Try
				Dim wc As New WebClientSpecial()
				wc.UseDefaultCredentials = true
				wc.Proxy = System.Net.WebRequest.GetSystemWebProxy()
				API.ToLog("Validating User-Login")
				If GetUHash() <> "" Then
					API.ToLog("User-Login exist")
					Dim rsl As String = wc.DownloadString(APILINK & "user/series/" & GetUHash())
					
					Dim xdoc As System.Xml.XmlDocument = JsonConvert.DeserializeXmlNode("{""bsapi"":" & rsl & "}","bsapi")
					Dim j As Xml.XmlNode = Nothing
					Dim xoll As Xml.XmlNodeList = xdoc.ChildNodes(0).ChildNodes
					If xoll(0).Name <> "error" Then
						
						Dim nn As Integer = 0
						API.ToLog("Detected Favorites: " & xoll.Count)
						For nn = 0 To xoll.Count - 1
							j = xoll(nn)
							Dim id As Long = CLng(j.ChildNodes(0).InnerText.Trim())
							Dim name As String = j.ChildNodes(1).InnerText.Trim()
							API.ToLog("Added " & name & " to Favorites List")
							d.Add(New API.Serie(name,CInt(id)))
						Next
					Else
						API.ToLog("Call failed. User-Hash reset (logget out!)")
						UserHash = ""
					End If
				Else
					API.ToLog("No User-Hash exists")
				End If
			Catch ex As Exception
				API.ToLog("Error!")
				API.ToLog(ex.ToString())
			End Try
			Return(d.ToArray())
		End Get
	End Property
End Class