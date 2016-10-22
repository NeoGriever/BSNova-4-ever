'
' Erstellt mit SharpDevelop.
' Benutzer: Sarah
' Datum: 22.09.2016
' Zeit: 00:47
' 
' Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
'
Public Class NameGenerator
	#Region " Namensgenerierung "
		Public Shared Function CleanFN(ByVal filename As String) As String
			Dim cleanedFN As String = filename
			Dim ichars As New List(Of Char)
			ichars.AddRange(System.IO.Path.GetInvalidFileNameChars())
			ichars.AddRange(System.IO.Path.GetInvalidPathChars())
			For Each i As Char In ichars.ToArray()
				While cleanedFN.IndexOf(i) > -1
					cleanedFN = cleanedFN.Replace(i,"_")
				End While
			Next
			Return(cleanedFN)
		End Function
		Public Shared Function cc(ByVal wert As Integer) As String
			Dim rsl As String = wert.ToString()
			While rsl.Length < 2
				rsl = "0" & rsl
			End While
			Return(rsl)
		End Function
		Public Overloads Shared Function BuildName(ByVal nvc As System.Collections.Specialized.NameValueCollection,ByVal pattern_movie As String,ByVal pattern_episode As String) As String
			Dim moviePattern As String = pattern_movie
			Dim episodePattern As String = pattern_episode
			Dim usingPattern = episodePattern
			If nvc.Get("%seasonN%") = "0" Then
				usingPattern = moviePattern
			End If
			Dim builded_string As String = usingPattern
			Dim pt As String = ""
			For Each pt In nvc.Keys
				builded_string = builded_string.Replace(pt,nvc.Get(pt))
			Next
			Return(builded_string)
		End Function
		Public Overloads Shared Function BuildName(ByVal nvc As System.Collections.Specialized.NameValueCollection) As String
			Dim moviePattern As String = GlobalConfig.GlobalConfig.DBSelect("pattern.movie","%serie%\%name%.mp4",True)
			Dim episodePattern As String = GlobalConfig.GlobalConfig.DBSelect("pattern.episode","%serie%\S%seasonNN%E%episodeNN% - %name%.mp4",True)
			Dim usingPattern = episodePattern
			If nvc.Get("%seasonN%") = "0" Then
				usingPattern = moviePattern
			End If
			Dim builded_string As String = usingPattern
			Dim pt As String = ""
			For Each pt In nvc.Keys
				builded_string = builded_string.Replace(pt,nvc.Get(pt))
			Next
			Return(builded_string)
		End Function
	#End Region

End Class
