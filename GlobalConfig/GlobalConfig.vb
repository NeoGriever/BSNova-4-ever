Imports System.Text
Imports System.Configuration
Imports System.Data.SQLite

Public Class GlobalConfig
	Private Shared connection As SQLiteConnection = Nothing
	Public Shared Sub DBCreateDatabase()
		If connection Is Nothing Then
			connection = New SQLiteConnection()
			Dim old_filepath As String = My.Application.Info.DirectoryPath & "\user.s3db"
			Dim filepath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\user.s3db"
			If Not System.IO.Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\") Then
				System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\")
			End If
			If System.IO.File.Exists(old_filepath) And Not System.IO.File.Exists(filepath) Then
				System.IO.File.Move(old_filepath,filepath)
			End If
			connection.ConnectionString = "Data Source=" & filepath & "; Version=3;"
			connection.Open()
			Dim command As SQLiteCommand = connection.CreateCommand()
			command.CommandText = "CREATE TABLE IF NOT EXISTS userconfig (name VARCHAR PRIMARY KEY UNIQUE , value VARCHAR)"
			command.ExecuteNonQuery()
			command.Dispose()
		End If
	End Sub
	Public Shared Sub DBInsert(ByVal name As String,ByVal value As String)
		DBCreateDatabase()
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "INSERT OR IGNORE INTO userconfig (name, value) VALUES (@a,@b)"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		command.Parameters.Add(New SQLiteParameter("@b",value))
		command.ExecuteNonQuery()
		command.Dispose()
		command = connection.CreateCommand()
		command.CommandText = "UPDATE userconfig SET value = @b WHERE name = @a"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		command.Parameters.Add(New SQLiteParameter("@b",value))
		command.ExecuteNonQuery()
		command.Dispose()
	End Sub
	Public Shared Function DBSelect(ByVal name As String,Optional ByVal defaultResult As String = "",Optional ByVal InsertIfMissing As Boolean = False) As String
		DBCreateDatabase()
		Dim result As String = defaultResult
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "SELECT value FROM userconfig WHERE name = @a LIMIT 1"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		Dim rd As SQLiteDataReader = command.ExecuteReader()
		Dim InsertIt As Boolean = False
		If rd.StepCount > 0 And rd.Read() Then
			result = rd.Item("value").ToString()
			If result = "" Then
				result = defaultResult
				If InsertIfMissing Then
					InsertIt = True
				End If
			End If
		ElseIf InsertIfMissing Then
			InsertIt = True
		End If
		command.Dispose()
		If InsertIt Then
			DBInsert(name,defaultResult)
		End If
		Return(result)
	End Function
	Public Shared Sub DBDelete(ByVal name As String)
		DBCreateDatabase()
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "DELETE FROM userconfig WHERE name = @a LIMIT 1"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		Try
			command.ExecuteNonQuery()
		Catch
			DBInsert(name,"")
		End Try
		command.Dispose()
	End Sub
	Public Shared Function DBEntries() As String()
		DBCreateDatabase()
		Dim result As New System.Collections.Generic.List(Of String)
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "SELECT name FROM userconfig ORDER BY name ASC"
		Dim sqread As SQLiteDataReader = command.ExecuteReader()
		While sqread.Read()
			result.Add(sqread.Item("name").ToString())
		End While
		command.Dispose()
		Return(result.ToArray())
	End Function
	Public Shared Function DBQuerySelect(ByVal query As String,Optional ByVal getValues As Boolean = False) As String()
		DBCreateDatabase()
		Dim result As New System.Collections.Generic.List(Of String)
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "SELECT name , value FROM userconfig WHERE name LIKE @a ORDER BY name ASC"
		command.Parameters.Add(New SQLiteParameter("@a",query))
		Dim sqread As SQLiteDataReader = command.ExecuteReader()
		While sqread.Read()
			If getValues Then
				result.Add(sqread.Item("value").ToString())
			Else
				result.Add(sqread.Item("name").ToString())
			End If
		End While
		command.Dispose()
		Return(result.ToArray())
	End Function
End Class
