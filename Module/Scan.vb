Imports System.Security.Cryptography
Imports PeNet
Imports System.Text.Json

Public Class HezhongResult
    ' {"code": 200, "md5": "60823f6bf0020325a1db2d5ed25dba84", "score": 40, "tag": "Unknown"}
    Public Property code As Integer
    Public Property md5 As String
    Public Property score As Integer
    Public Property tag As String
End Class
Public Class Scan
    Dim MD5Data As New ArrayList
    Dim PEImportedFuncsData As New Dictionary(Of String, Integer)
    Dim VirusScore = 0
    Dim whiteList As New List(Of String) ' °×Ãûµ¥
    Public Sub New(ByVal argMD5Data)
        MD5Data = argMD5Data
    End Sub
    Public Sub New(argPEImportedFuncsData, argVirusScore)
        PEImportedFuncsData = argPEImportedFuncsData
        VirusScore = argVirusScore
    End Sub
    Public Sub New()

    End Sub
    Public Function getMD5(ByVal strSource As String) As String
        Dim result As String = ""

        Try
            'strSource = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\path.txt"
            Dim fstream As New IO.FileStream(strSource, IO.FileMode.Open, IO.FileAccess.Read)
            Dim dataToHash(fstream.Length - 1) As Byte
            fstream.Read(dataToHash, 0, fstream.Length)
            fstream.Close()
            Dim hashvalue As Byte() = CType(CryptoConfig.CreateFromName("MD5"), Security.Cryptography.HashAlgorithm).ComputeHash(dataToHash)
            Dim i As Integer
            For i = 0 To hashvalue.Length - 1
                result += Microsoft.VisualBasic.Right("00" + Hex(hashvalue(i)).ToLower, 2)
            Next
            Return result
        Catch ex As Exception
            'MsgBox(ex.Message)
            Return result
        End Try

    End Function

    Public Function api_MD5Scan(ByVal fileMD5 As String) As Boolean
        If MD5Data Is Nothing Then Throw New Exception("²¡¶¾¿âÎª¿Õ")
        Return Me.MD5Data.Contains(fileMD5)
    End Function
    Public Async Function api_ANKScan(ByVal filePath As String) As Task(Of Boolean)
        Return Await Task.Run(Function()
                                  Try
                                      If whiteList.Contains(getMD5(filePath)) Then Return False

                                      Dim procInfo As New ProcessStartInfo With {
                                          .FileName = """" & CommonVars.AppPath & "\ANK_CORE\ANK_CORE.exe" & """",
                                          .Arguments = " " & """" & filePath & """" & " " & "FUWRMQe336sSx5m5-mHOP7ncDsUbCL3l",
                                          .CreateNoWindow = True,
                                          .RedirectStandardOutput = True,
                                          .UseShellExecute = False
                                      }
                                      Dim proc As New Process With {
                                          .StartInfo = procInfo
                                      }
                                      proc.Start()

                                      Using output As IO.StreamReader = proc.StandardOutput
                                          If CDbl(output.ReadToEnd.Replace(vbCrLf, "")) >= 0.5 Then Return True
                                      End Using

                                      proc.WaitForExit()
                                  Catch ex As Exception
                                      Debug.Print("ANK " & ex.Message)
                                  End Try
                                  Return False
                              End Function)
    End Function

    Public Async Function api_PiTuiScan(ByVal filePath As String) As Task(Of Boolean)
        Return Await Task.Run(
            Function()
                Try
                    If whiteList.Contains(getMD5(filePath)) Then Return False

                    'Virusmark.exe A abc123 "C:\Users\Example\Documents\file_to_scan.txt"
                    Dim procInfo As New ProcessStartInfo With {
                                          .FileName = """" & CommonVars.AppPath & "\PiTui_Core\PiTuiCloud.exe" & """",
                                          .Arguments = " " & "A " & "a323a4a2-d096-4dd5-b69d-6bc5b6b9ae64" & " " & """" & filePath & """",
                                          .CreateNoWindow = True,
                                          .RedirectStandardOutput = True,
                                          .UseShellExecute = False
                                      }
                    Dim proc As New Process With {
                                          .StartInfo = procInfo
                                      }
                    proc.Start()

                    Using output As IO.StreamReader = proc.StandardOutput
                        Dim jsonText = output.ReadToEnd
                        Dim r As HezhongResult = JsonSerializer.Deserialize(Of HezhongResult)(jsonText)
                        If r.code = 200 Then
                            Return r.score >= 50
                        End If
                    End Using

                    proc.WaitForExit()
                Catch ex As Exception
                    Debug.Print("PiTui " & ex.Message)
                End Try
                Return False
            End Function)
    End Function

    Public Async Function api_PEDataScan(filePath As String) As Task(Of Boolean)
        Return Await Task.Run(
            Function()
                Try
                    Dim pefile As New PeNet.PeFile(filePath)
                    Dim score = 0
                    Debug.Print($"{filePath} {pefile.ImportedFunctions Is Nothing}")
                    ' If pefile.ImportedFunctions Is Nothing Then Return False
                    For i As Integer = 0 To pefile.ImportedFunctions.Count - 1
                        Try
                            If pefile.ImportedFunctions(i).Name Is Nothing Then Continue For
                            Dim funcName = pefile.ImportedFunctions(i).Name.Replace(vbCrLf, "")

                            score += If(PEImportedFuncsData.ContainsKey(funcName), PEImportedFuncsData(funcName), 0)

                        Catch ex As Exception
                            Debug.Print(ex.Message)
                        End Try
                    Next
                    Return score >= VirusScore
                Catch ex As Exception
                    Debug.Print(ex.Message)
                End Try
                Return False
            End Function)
    End Function

    Public Function noAsync_api_PEDataScan(filePath As String) As Boolean
        Try
            Dim pefile As New PeNet.PeFile(filePath)
            Dim score = 0
            For i As Integer = 0 To pefile.ImportedFunctions.Count - 1
                'Try
                Debug.Print($"{filePath} {pefile.ImportedFunctions Is Nothing}")

                If pefile.ImportedFunctions(i).Name Is Nothing Then Continue For
                Dim funcName = pefile.ImportedFunctions(i).Name.Replace(vbCrLf, "")

                score += If(PEImportedFuncsData.ContainsKey(funcName), PEImportedFuncsData(funcName), 0)

                'Catch ex As Exception
                'End Try
            Next
            Return score >= VirusScore
        Catch ex As Exception
        End Try
        Return False

    End Function

End Class

