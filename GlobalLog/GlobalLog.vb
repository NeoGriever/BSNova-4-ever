Imports System.Text
Imports System.Configuration
Imports System.Data.SQLite

Public Class GlobalLog
	Private Shared connection As SQLiteConnection = Nothing
	Public Shared Sub DBCreateDatabase()
		If connection Is Nothing Then
			connection = New SQLiteConnection()
			Dim old_filepath As String = My.Application.Info.DirectoryPath & "\log.s3db"
			Dim filepath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\log.s3db"
			If Not System.IO.Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\") Then
				System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\")
			End If
			If System.IO.File.Exists(old_filepath) Then
				System.IO.File.Move(old_filepath,filepath)
			End If
			connection.ConnectionString = "Data Source=" & filepath & "; Version=3;"
			connection.Open()
			Dim command As SQLiteCommand = connection.CreateCommand()
			command.CommandText = "CREATE TABLE IF NOT EXISTS log (timestamp BIGINT PRIMARY KEY UNIQUE, data TEXT, type VARCHAR)"
			command.ExecuteNonQuery()
			command.Dispose()
		End If
		Dim clear_command As SQLiteCommand = connection.CreateCommand()
		clear_command.CommandText = "DELETE FROM log WHERE timestamp < @a"
		clear_command.Parameters.Add(New SQLiteParameter("@a",TimeToUnix(DateTime.Now.AddDays(-7))))
		clear_command.ExecuteNonQuery()
		clear_command.Dispose()
	End Sub
	Public Shared Sub Log(ByVal str As String)
		DBCreateDatabase()
		Dim command As SQLiteCommand = connection.CreateCommand()
		command.CommandText = "INSERT INTO log (timestamp, data, type) VALUES (@a,@b,@c)"
		command.Parameters.Add(New SQLiteParameter("@a",TimeToUnix(DateTime.Now)))
		command.Parameters.Add(New SQLiteParameter("@b",str))
		command.Parameters.Add(New SQLiteParameter("@c","System"))
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
