Imports System.Text
Imports System.Security.Cryptography

Public Class md5
	Public Shared Function MD5Hash(ByVal s As String) As String
        Dim encoding As Encoding = Encoding.GetEncoding("iso-8859-1")
        Dim input As Byte() = encoding.GetBytes(s)
        Dim hash As Byte() = (New MD5CryptoServiceProvider()).ComputeHash(input)
        Dim output As String = ""
        Dim i As Byte = Nothing
        For Each i In hash
        	Dim b As String = Hex(i)
        	If b.Length < 2 Then
        		b = "0" & b
        	End If
        	output &= b
        Next
        Return(output)
	End Function
End Class
