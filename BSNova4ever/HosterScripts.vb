Imports System.Xml
Imports System.IO

Public Class HosterScripts
	Public Shared Overloads Function GetHosterScriptType(ByVal hosterName As String) As Integer
		GroundInitialize()
		Dim reslt As Integer = 0
		Try
			For Each i As XmlNode In rootNode.ChildNodes
				Dim scriptName As String = i.Attributes.GetNamedItem("name").Value.ToLower().Trim()
				If scriptName = hosterName.ToLower().Trim() Then
					reslt = CInt(i.Attributes.GetNamedItem("type").Value.Trim())
					Exit For
				End If
			Next
		Catch
		End Try
		Return(reslt)
	End Function
	Public Shared Overloads Function GetHosterScriptType(ByVal url As Uri) As Integer
		GroundInitialize()
		Dim reslt As Integer = 0
		Try
			For Each i As XmlNode In rootNode.ChildNodes
				Dim supportedDomains As New List(Of String)
				For Each j As XmlNode In i.ChildNodes(0).ChildNodes
					supportedDomains.Add(j.Attributes.GetNamedItem("domain").Value.ToLower().Trim())
				Next
				If supportedDomains.IndexOf(url.Host.ToLower().Trim()) > -1 Then
					reslt = CInt(i.Attributes.GetNamedItem("type").Value.Trim())
					Exit For
				End If
			Next
		Catch
		End Try
		Return(reslt)
	End Function
	Public Shared Overloads Function GetHosterScript(ByVal hosterName As String) As String
		GroundInitialize()
		Dim reslt As String = ""
		For Each i As XmlNode In rootNode.ChildNodes
			Dim scriptName As String = i.Attributes.GetNamedItem("name").Value.ToLower().Trim()
			If scriptName = hosterName.ToLower().Trim() Then
				reslt = CType(i.ChildNodes(1).ChildNodes(0),XmlCDataSection).Value
				Exit For
			End If
		Next
		Return(reslt)
	End Function
	Public Shared Overloads Function GetHosterScript(ByVal url As Uri) As String
		GroundInitialize()
		Dim reslt As String = ""
		For Each i As XmlNode In rootNode.ChildNodes
			Dim supportedDomains As New List(Of String)
			For Each j As XmlNode In i.ChildNodes(0).ChildNodes
				supportedDomains.Add(j.Attributes.GetNamedItem("domain").Value.ToLower().Trim())
			Next
			If supportedDomains.IndexOf(url.Host.ToLower().Trim()) > -1 Then
				reslt = CType(i.ChildNodes(1).ChildNodes(0),XmlCDataSection).Value
				Exit For
			End If
		Next
		Return(reslt)
	End Function
	Public Shared Overloads Function HosterSupported(ByVal hosterName As String) As Boolean
		GroundInitialize()
		Dim reslt As Boolean = False
		For Each i As XmlNode In rootNode.ChildNodes
			Dim scriptName As String = i.Attributes.GetNamedItem("name").Value.ToLower().Trim()
			If scriptName = hosterName.ToLower().Trim() Then
				reslt = True
				Exit For
			End If
		Next
		Return(reslt)
	End Function
	Public Shared Overloads Function HosterSupported(ByVal url As Uri) As Boolean
		GroundInitialize()
		Dim reslt As Boolean = False
		For Each i As XmlNode In rootNode.ChildNodes
			Dim supportedDomains As New List(Of String)
			For Each j As XmlNode In i.ChildNodes(0).ChildNodes
				supportedDomains.Add(j.Attributes.GetNamedItem("domain").Value.ToLower().Trim())
			Next
			If supportedDomains.IndexOf(url.Host.ToLower().Trim()) > -1 Then
				reslt = True
				Exit For
			End If
		Next
		Return(reslt)
	End Function
	Public Shared Function GetHosterDomains(ByVal hosterName As String) As String()
		Dim rsl As New List(Of String)
		For Each i As XmlNode In rootNode.ChildNodes
			Dim scriptName As String = i.Attributes.GetNamedItem("name").Value.ToLower().Trim()
			If scriptName = hosterName.ToLower().Trim() Then
				For Each j As XmlNode In i.ChildNodes(0).ChildNodes
					rsl.Add(j.Attributes.GetNamedItem("domain").Value.ToLower().Trim())
				Next
				Exit For
			End If
		Next
		Return(rsl.ToArray())
	End Function
	Public Shared Sub RemoveHosterScript(ByVal hosterName As String)
		GroundInitialize()
		Dim rsl As New List(Of String)
		For Each i As XmlNode In rootNode.ChildNodes
			Dim scriptName As String = i.Attributes.GetNamedItem("name").Value.ToLower().Trim()
			If scriptName = hosterName.ToLower().Trim() Then
				rootNode.RemoveChild(i)
				Exit For
			End If
		Next
		xmlFile.Save(pt)
		RebuildPriorityList(hosterName)
	End Sub
	Public Shared Sub SaveHosterScript(ByVal hosterName As String,ByVal supportedDomains As List(Of Uri),ByVal script As String,ByVal scriptType As Integer)
		GroundInitialize()
		Dim editingNode As XmlNode = Nothing
		If HosterSupported(hosterName) Then
			For Each i As XmlNode In rootNode.ChildNodes
				Dim scriptName As String = i.Attributes.GetNamedItem("name").Value.ToLower().Trim()
				If scriptName = hosterName.ToLower().Trim() Then
					editingNode = i
					Exit For
				End If
			Next
		End If
		If editingNode Is Nothing Then
			Dim node As XmlElement = xmlFile.CreateElement("hoster")
			Dim supportedDomainsNode As XmlElement = xmlFile.CreateElement("supporteddomains")
			For Each i As Uri In supportedDomains
				Dim supportedDomain As XmlElement = xmlFile.CreateElement("host")
				Dim attr2 As XmlAttribute = xmlFile.CreateAttribute("domain")
				attr2.Value = i.Host.ToLower().Trim()
				supportedDomain.Attributes.Append(attr2)
				supportedDomainsNode.AppendChild(supportedDomain)
			Next
			node.AppendChild(supportedDomainsNode)
			Dim attr As XmlAttribute = xmlFile.CreateAttribute("name")
			attr.Value = hosterName
			node.Attributes.Append(attr)
			attr = xmlFile.CreateAttribute("type")
			attr.Value = scriptType.ToString()
			node.Attributes.Append(attr)
			Dim scriptPlace As XmlElement = xmlFile.CreateElement("script")
			Dim scriptCData As XmlCDataSection = xmlFile.CreateCDataSection(script)
			scriptPlace.AppendChild(scriptCData)
			node.AppendChild(scriptPlace)
			rootNode.AppendChild(node)
		Else
			editingNode.ChildNodes(0).RemoveAll
			For Each i As Uri In supportedDomains
				Dim supportedDomain As XmlElement = xmlFile.CreateElement("host")
				Dim attr2 As XmlAttribute = xmlFile.CreateAttribute("domain")
				attr2.Value = i.Host.ToLower().Trim()
				supportedDomain.Attributes.Append(attr2)
				editingNode.ChildNodes(0).AppendChild(supportedDomain)
			Next
			editingNode.Attributes.RemoveAll()
			Dim attr As XmlAttribute = xmlFile.CreateAttribute("name")
			attr.Value = hosterName
			editingNode.Attributes.Append(attr)
			attr = xmlFile.CreateAttribute("type")
			attr.Value = scriptType.ToString()
			editingNode.Attributes.Append(attr)
			CType(editingNode.ChildNodes(1).ChildNodes(0),XmlCDataSection).Value = script
		End If
		xmlFile.Save(pt)
		RebuildPriorityList()
	End Sub
	Private Shared Sub RebuildPriorityList(Optional ByVal RemoveFromList As String = "")
		Dim existingList As New List(Of String)
		Try
			existingList.AddRange(rootNode.Attributes.GetNamedItem("priorityList").Value.Trim().Split(CChar(",")))
		Catch
		End Try
		For Each i As XmlNode In rootNode.ChildNodes
			Dim name As String = i.Attributes.GetNamedItem("name").Value.Trim()
			If existingList.IndexOf(name) < 0 Then
				existingList.Add(name)
			End If
		Next
		If RemoveFromList <> "" Then
			existingList.Remove(RemoveFromList)
		End If
		SetSortingList(existingList)
	End Sub
	Private Shared pt As String = ""
	Private Shared xmlFile As XmlDocument = Nothing
	Private Shared rootNode As XmlNode = Nothing
	Private Shared Sub GroundInitialize()
		pt = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) & "\BSNova\hoster.xml"
		If xmlFile Is Nothing Then
			xmlFile = New XmlDocument()
			If Not File.Exists(pt) Then
				Dim xdec As XmlDeclaration = xmlFile.CreateXmlDeclaration("1.0","UTF-8","no")
				xmlFile.AppendChild(xdec)
				rootNode = xmlFile.CreateElement("hosterscripts")
				xmlFile.AppendChild(rootNode)
			Else
				xmlFile.Load(pt)
				rootNode = xmlFile.ChildNodes(1)
			End If
		ElseIf rootNode Is Nothing Then
			rootNode = xmlFile.ChildNodes(1)
		End If
	End Sub
	Private Shared Function FindNamedNode(ByVal name As String) As XmlNode
		Dim result As XmlNode = Nothing
		For Each literator As XmlNode In rootNode.ChildNodes
			If literator.Name = name Then
				result = literator
				Exit For
			End If
		Next
		Return(result)
	End Function
	Public Shared Function GetPriorityList() As List(Of String)
		GroundInitialize()
		Dim result As New List(Of String)
		Try
			result.AddRange(rootNode.Attributes.GetNamedItem("priorityList").Value.Trim().Split(CChar(",")))
		Catch
		End Try
		Return(result)
	End Function
	Public Shared Sub SetPriorityList(ByVal newList As String)
		GroundInitialize()
		rootNode.Attributes.RemoveNamedItem("priorityList")
		Dim xatt As XmlAttribute = xmlFile.CreateAttribute("priorityList")
		xatt.Value = newList
		rootNode.Attributes.Append(xatt)
	End Sub
	Private Shared Sub SetSortingList(ByVal newSortingList As List(Of String))
		Dim att As XmlAttribute = xmlFile.CreateAttribute("priorityList")
		att.Value = String.Join(",",newSortingList.ToArray())
		rootNode.Attributes.RemoveNamedItem("priorityList")
		rootNode.Attributes.Append(att)
		xmlFile.Save(pt)
	End Sub
End Class
