Imports System.Text
Imports System.Configuration
Imports System.Data.SQLite

Public Class GlobalCache
	Private Shared connection As SQLiteConnection = Nothing
	Public Shared Sub DBCreateDatabase()
		If connection Is Nothing Then
			connection = New SQLiteConnection()
			Dim old_filepath As String = My.Application.Info.DirectoryPath & "\cache.s3db"
			Dim filepath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\cache.s3db"
			If Not System.IO.Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\") Then
				System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\")
			End If
			If System.IO.File.Exists(old_filepath) Then
				System.IO.File.Move(old_filepath,filepath)
			End If
			connection.ConnectionString = "Data Source=" & filepath & "; Version=3;"
			connection.Open()
			Dim command As SQLiteCommand = connection.CreateCommand()
			command.CommandText = "CREATE TABLE IF NOT EXISTS cache (name VARCHAR PRIMARY KEY UNIQUE , data TEXT , created BIGINT )"
			command.ExecuteNonQuery()
			command.Dispose()
		End If
	End Sub
	Public Shared Function HasCache(ByVal name As String,Optional ByVal lifetime As Integer = 86400) As Boolean
		Dim result As Boolean = False
		DBCreateDatabase()
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "SELECT created FROM cache WHERE name = @a"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		Dim sr As SQLiteDataReader = command.ExecuteReader()
		If sr.StepCount > 0 Then
			sr.Read()
			Dim created_timestamp As Long = CLng(sr.Item("created"))
			Dim current_timestamp As Long = TimeToUnix(DateTime.Now)
			If current_timestamp - created_timestamp < lifetime Then
				result = True
			End If
		End If
		command.Dispose()
		Return(result)
	End Function
	Public Shared Function GetCache(ByVal name As String) As String
		Dim result As String = ""
		DBCreateDatabase()
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "SELECT data FROM cache WHERE name = @a"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		Dim sr As SQLiteDataReader = command.ExecuteReader()
		If sr.StepCount > 0 Then
			sr.Read()
			result = sr.Item("data").ToString()
		End If
		command.Dispose()
		Return(result)
	End Function
	Public Shared Sub SetCache(ByVal name As String,ByVal data As String)
		DBCreateDatabase()
		Dim stamp As Long = TimeToUnix(DateTime.Now)
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "INSERT OR IGNORE INTO cache (name, data, created) VALUES (@a,@b,@c)"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		command.Parameters.Add(New SQLiteParameter("@b",data))
		command.Parameters.Add(New SQLiteParameter("@c",stamp))
		command.ExecuteNonQuery()
		command.Dispose()
		command = connection.CreateCommand()
		command.CommandText = "UPDATE cache SET data = @b , created = @c WHERE name = @a"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		command.Parameters.Add(New SQLiteParameter("@b",data))
		command.Parameters.Add(New SQLiteParameter("@c",stamp))
		command.ExecuteNonQuery()
		command.Dispose()
	End Sub
	Public Shared Sub RemoveCache(ByVal name As String)
		DBCreateDatabase()
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "DELETE FROM cache WHERE name = @a"
		command.Parameters.Add(New SQLiteParameter("@a",name))
		command.ExecuteNonQuery()
		command.Dispose()
	End Sub
	Public Shared Sub ClearCache()
		DBCreateDatabase()
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "TRUNCATE TABLE cache"
		command.ExecuteNonQuery()
		command.Dispose()
	End Sub
	Private Shared Function TimeToUnix(ByVal dteDate As DateTime) As Long
	    If dteDate.IsDaylightSavingTime = True Then
	        dteDate = DateAdd(DateInterval.Hour, -1, dteDate)
	    End If
	    TimeToUnix = DateDiff(DateInterval.Second, #1/1/1970#, dteDate)
	End Function
End Class