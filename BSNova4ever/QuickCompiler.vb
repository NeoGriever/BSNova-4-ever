Imports System.IO
Imports System.Xml
Imports Microsoft.VisualBasic
Imports Microsoft.CSharp
Imports System.CodeDom.Compiler
Imports System.Reflection

Public Class QuickCompiler
	Public Shared Event ThrownError(ByVal cerr As System.CodeDom.Compiler.CompilerError)
	Public Shared Overloads Function Compile(ByVal src As String) As Object
		Return(Compile(src,"HosterParser",0))
	End Function
	Public Shared Overloads Function Compile(ByVal src As String,ByVal codeType As Integer) As Object
		Return(Compile(src,"HosterParser",codeType))
	End Function
	Public Shared Overloads Function Compile(ByVal src As String,ByVal className As String) As Object
		Return(Compile(src,className,0))
	End Function
	Public Shared Overloads Function Compile(ByVal src As String,ByVal className As String,ByVal codeType As Integer) As Object
		Dim vbel As New VBCodeProvider()
		Dim csel As New CSharpCodeProvider()
		Dim oCParams As New CompilerParameters()
		oCParams.IncludeDebugInformation = True
		oCParams.ReferencedAssemblies.Add("mscorlib.dll")
		oCParams.ReferencedAssemblies.Add("System.dll")
		oCParams.ReferencedAssemblies.Add("System.Windows.Forms.dll")
		oCParams.ReferencedAssemblies.Add("System.Linq.dll")
		oCParams.ReferencedAssemblies.Add("GlobalDebugDiag.exe")
		Dim oCResults As CompilerResults = Nothing
		Dim oAssy As Assembly = Nothing
		oCParams.GenerateInMemory = True
		Dim cr As System.CodeDom.Compiler.CompilerResults = Nothing
		If codeType = 0 Then
			cr = vbel.CompileAssemblyFromSource(oCParams,src)
		ElseIf codeType = 1 Then
			cr = csel.CompileAssemblyFromSource(oCParams,src)
		End If
		If cr.Errors.Count > 0 Then
			Dim err As System.CodeDom.Compiler.CompilerError = Nothing
			Dim out As String = ""
			For Each err In cr.Errors
				out = out & err.ErrorText & System.Environment.NewLine
				RaiseEvent ThrownError(err)
			Next
			Return(Nothing)
		Else
			Return(cr.CompiledAssembly.CreateInstance(className))
		End If
	End Function
End Class