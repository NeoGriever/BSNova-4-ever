'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 10.10.2016
' Zeit: 11:47
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Public Partial Class Config
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		Dim epi As New API.Episode("Testepisode","Testepisode (englisch)",0,False,0,"Testserie",2,6)
		epi.Season = New API.Season(2,0,"Testserie")
		epi.Season.Serie = New API.Serie("Testerie",0)
		listView1.Items.Add("").Tag = New Object() {epi}
		epi = New API.Episode("Testfilm","Testmovie",0,False,1,"Testserie",0,0)
		epi.Season = New API.Season(0,1,"Testserie")
		epi.Season.Serie = New API.Serie("Testserie",1)
		listView1.Items.Add("").Tag = New Object() {epi}
		epi = New API.Episode("Noch eine Episode","Another Episode",0,False,3,"Blah",5,18)
		epi.Season = New API.Season(5,3,"Blah")
		epi.Season.Serie = New API.Serie("Blah",3)
		listView1.Items.Add("").Tag = New Object() {epi}
	End Sub
	
	Sub Button4Click(sender As Object, e As EventArgs)
		GlobalConfig.GlobalConfig.SetValue("downloader.path",textBox1.Text)
		GlobalConfig.GlobalConfig.SetValue("pattern.movie",textBox2.Text)
		GlobalConfig.GlobalConfig.SetValue("pattern.episode",textBox3.Text)
		Dim tlist As New List(Of String)
		For Each i As ListViewItem In moveItemListView1.Items
			tlist.Add(i.Text)
		Next
		HosterScripts.SetPriorityList(String.Join(",",tlist))
		MessageBox.Show("Die Einstellungen wurden gespeichert.","Einstellungen gespeichert",MessageBoxButtons.OK,MessageBoxIcon.Information)
		Me.Close()
	End Sub
	
	Sub ConfigLoad(sender As Object, e As EventArgs)
		Dim TargetPath As String = GlobalConfig.GlobalConfig.GetValue("downloader.path").Trim()
		If TargetPath = "" Then
			TargetPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonVideos)
		End If
		If TargetPath.Substring(TargetPath.Length - 1,1) <> "\" Then
			TargetPath &= "\"
		End If
		textBox1.Text = TargetPath
		Dim patt_movie As String = GlobalConfig.GlobalConfig.GetValue("pattern.movie")
		Dim patt_episo As String = GlobalConfig.GlobalConfig.GetValue("pattern.episode")
		If patt_movie = "" Then
			patt_movie = "%serie%\%name%.mp4"
		End If
		If patt_episo = "" Then
			patt_episo = "%serie%\S%seasonNN%E%episodeNN% - %name%.mp4"
		End If
		textBox2.Text = patt_movie
		textBox3.Text = patt_episo
		For Each i As String In HosterScripts.GetPriorityList()
			moveItemListView1.Items.Add(i)
		Next
	End Sub
	
	Sub TextBox2TextChanged(sender As Object, e As EventArgs)
		For Each i In listView1.Items
			Dim episode As API.Episode = CType(CType(i.Tag,Object())(0),API.Episode)
			Dim nvc As New System.Collections.Specialized.NameValueCollection()
			nvc.Add("%serie%",episode.Season.Serie.Name)
			nvc.Add("%seasonNN%",NameGenerator.cc(episode.Season.Number))
			nvc.Add("%seasonN%",episode.Season.Number.ToString())
			nvc.Add("%episodeNN%",NameGenerator.cc(episode.Episode))
			nvc.Add("%episodeN%",episode.Episode.ToString())
			nvc.Add("%name%",episode.Name)
			nvc.Add("%nameE%",NameGenerator.CleanFN(episode.NameEnglish))
			nvc.Add("%nameG%",NameGenerator.CleanFN(episode.NameGerman))
			i.Text = NameGenerator.BuildName(nvc,textBox2.Text,textBox3.Text)
		Next
	End Sub
	
	Sub Button2Click(sender As Object, e As EventArgs)
		Dim HV As New HelpView()
		HV.ShowDialog()
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		Dim fbd As New FolderBrowserDialog()
		fbd.SelectedPath = textBox1.Text
		If fbd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			Dim TargetPath As String = fbd.SelectedPath
			If TargetPath.Substring(TargetPath.Length - 1,1) <> "\" Then
				TargetPath &= "\"
			End If
			textBox1.Text = TargetPath
		End If
	End Sub
End Class
