Imports System.Net
Imports System.Security.Cryptography
Imports System.Text
Imports System.Convert
Imports System.IO

Public Class WebClientSpecial
	Inherits System.Net.WebClient
	Public Cookies As CookieContainer = Nothing
	Public Event ContainsCookies()
	Public StartAt As Integer = 0
	Public ReadOnly Property DefaultTimeout As Integer
		Get
			Dim result As String = GlobalConfig.GlobalConfig.GetValue("api.timeout")
			If result = "" Then
				result = "15000"
				GlobalConfig.GlobalConfig.SetValue("api.timeout",result)
			End If
			Return(CInt(result))
		End Get
	End Property
	Public OneShotTimeout As Integer = 0
	Private Function hexon(ByVal bytes() As Byte) As String
		Dim result As String = ""
		Dim i As Byte = Nothing
		For Each i In bytes
			Dim hexChar As String = CInt(i).ToString("X").ToLower()
			If hexChar.Length < 2 Then
				hexChar = "0" & hexChar
			End If
			result &= hexChar
		Next
		Return(result)
	End Function
	Private Function AccessKey(ByVal url As String) As String
		Dim result As String = ""
		Dim publicKey As String = treeViewDoubleBuffered.TagSize
		Dim privateKey() As Byte = treeViewDoubleBuffered.TagOffset
		Dim time As Integer = CInt((DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds())
		url = url.Replace("http://bs.to/api/","")
		Dim h256 As New HMACSHA256(privateKey)
		Dim hmac As String = hexon(h256.ComputeHash(Encoding.Default.GetBytes(time & "/" & url)))
		Dim json_data As String = "{""public_key"":""" & publicKey & """,""timestamp"":" & time & ",""hmac"":""" & hmac & """}"
		result = System.Convert.ToBase64String(Encoding.Default.GetBytes(json_data))
		Return(result)
	End Function
	Protected Overrides Function GetWebRequest(ByVal url As Uri) As WebRequest
		Dim r As System.Net.HttpWebRequest = CType(MyBase.GetWebRequest(url),HttpWebRequest)
		r.CookieContainer = Cookies
		Dim timeout As Integer = DefaultTimeout
		If OneShotTimeout > 0 Then
			timeout = OneShotTimeout
			OneShotTimeout = 0
		End If
		r.Timeout = timeout
		Dim u As String = AccessKey(url.ToString())
		r.Headers.Add("BS-Token",u)
		r.UserAgent = "BSNova"
		r.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
		r.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
		r.UseDefaultCredentials = True
		If StartAt > 0 Then
			r.AddRange(StartAt)
		End If
		Return(r) 
	End Function
	Protected Overrides Function GetWebResponse(ByVal wr As WebRequest) As WebResponse
		Dim r As System.Net.HttpWebResponse = CType(MyBase.GetWebResponse(wr),HttpWebResponse)
		If Cookies Is Nothing Then
			Cookies = New CookieContainer()
		End If
		Cookies.Add(r.Cookies)
		If Cookies.Count > 0 Then
			RaiseEvent ContainsCookies
		End If
		Return(r)
	End Function
	Public Function DownloadStringCached(ByVal s As String,Optional ByVal skipCache As Boolean = False) As String
		Dim s2 As String = s
		If s2.IndexOf("?") > -1 Then
			s2 = s2.Substring(0,s2.IndexOf("?"))
		End If
		Dim cacheFile As String = md5.MD5Hash(s2).ToLower() & ".cache"
		
		Dim cachePath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\cache\"
		Dim cachePathInfo As New System.IO.DirectoryInfo(cachePath)
		Try
			cachePathInfo.Create()
		Catch
		End Try
		
		If Not skipCache And GlobalConfig.GlobalConfig.GetValue("api.caching") = "1" AND System.IO.File.Exists(cachePath & cacheFile) Then
			Dim fs As New FileStream(cachePath & cacheFile,FileMode.Open)
			Dim br As New BinaryReader(fs)
			Dim stamp As DateTime = DateTime.FromFileTime(br.ReadInt64())
			Dim age As Long = CLng((DateTime.Now - stamp).TotalSeconds())
			Dim maxAge As String = GlobalConfig.GlobalConfig.GetValue("api.cache.maxage")
			If maxAge = "" Then
				maxAge = "30"
				GlobalConfig.GlobalConfig.SetValue("api.cache.maxage",maxAge)
			End If
			If age > 86400 * CInt(maxAge) Then
				Return(DownloadStringCached(s,True))
				br.Close()
				br.Dispose()
				fs.Close()
				fs.Dispose()
			Else
				Dim cacheData As String = br.ReadString()
				br.Close()
				br.Dispose()
				fs.Close()
				fs.Dispose()
				Return(cacheData)
			End If
		Else
			Try
				API.ValidateThread()
				Dim src As String = MyBase.DownloadString(s)
				If src.IndexOf("""error"":""unauthorized""") < 0 Then
					If GlobalConfig.GlobalConfig.GetValue("api.caching") = "1" Then
						Dim fs As New FileStream(cachePath & cacheFile,FileMode.Create)
						Dim bw As New BinaryWriter(fs)
						
						bw.Write(CLng(DateTime.Now.ToFileTime()))
						bw.Write(src)
						
						bw.Close()
						bw.Dispose()
						fs.Close()
						fs.Dispose()
					End If
					Return(src)
				Else
					Return("")
				End If
			Catch
				Return(DownloadStringCached(s))
			End Try
		End If
	End Function
End Class
