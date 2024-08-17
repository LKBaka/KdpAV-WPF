Imports System.IO
Imports WPFTest.KdpConst
Public Interface IYara
    Function Scan(filePath As String, rulePath As String) As List(Of Object)
End Interface

Public Class Yara : Implements IYara
    Private rulePath_
    Public Function getRulePath()
        Return rulePath_
    End Function
    Public Function Scan(filePath As String, rulePath As String) As List(Of Object) Implements IYara.Scan
        If Not File.Exists(filePath) Then Return {False, ""}.ToList
        rulePath_ = rulePath
        Dim startInfo As New ProcessStartInfo With {
            .FileName = $"""{AppPath}\yara64.exe""",
            .Arguments = $" ""{rulePath}"" ""{filePath}""",
            .UseShellExecute = False,
            .RedirectStandardOutput = True,
            .CreateNoWindow = True
        }

        Dim proc As New Process With {
            .StartInfo = startInfo
        }
        proc.Start()

        Using sr As StreamReader = proc.StandardOutput
            Dim output = sr.ReadToEnd()
            Debug.Print(output)
            If output = "" Then Return {False, ""}.ToList
            Dim results As List(Of Object) = {}.ToList

            Try
                results = {True, output.Split(" ")(0)}.ToList
                Return results
            Catch ex As Exception
            End Try
        End Using


        proc.WaitForExit()
        Return {False, ""}.ToList
    End Function
End Class
