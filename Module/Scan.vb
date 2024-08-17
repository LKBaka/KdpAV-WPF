Imports System.IO
Imports System.Net
Imports System.Net.WebRequestMethods
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json
Imports WPFTest.WIN32API
Imports WPFTest.KdpConst
Imports PeNet
Class HezhongCloudResult
    Public code = Nothing
    Public md5 = Nothing
    Public score = Nothing
    Public tag = Nothing
End Class
Public Class Scan
    Public peVirusImportedFuncs
    Public VirusMD5 As New List(Of String)
    Public Sub New(argMD5s As List(Of String))
        VirusMD5 = argMD5s
    End Sub
    Public Sub New(argVirusImportedFuncs As Dictionary(Of String, Integer))
        peVirusImportedFuncs = argVirusImportedFuncs
    End Sub
    Public Function getMD5(filePath As String) As String
        Try
            Dim EmptyMd5 = MD5.Create
            Using fr = IO.File.OpenRead(filePath)
                ' 计算哈希值  
                Dim hashBytes As Byte() = EmptyMd5.ComputeHash(fr)

                ' 将字节数组转换为十六进制字符串  
                Dim sb As New StringBuilder()
                For Each b As Byte In hashBytes
                    sb.Append(b.ToString("x2"))
                Next

                ' 返回哈希值的字符串表示  
                Return sb.ToString()
            End Using
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Async Function apiPeScan(filePath As String) As Task(Of Boolean)
        Return Await Task.Run(Async Function()
                                  Try
                                      Dim NSudoDevilModeModuleHandle As IntPtr = LoadLibrary(NSudoPath) ' LoadLibrary导入DLL

                                      Dim peFileResults = New PeFile(filePath)
                                      Dim score = 0
                                      For importedFuncIdx = 0 To peFileResults.ImportedFunctions.Count - 1
                                          Dim funcName = peFileResults.ImportedFunctions(importedFuncIdx).Name
                                          score += If(MainWindow.VirusImportedFuncs.ContainsKey(funcName), MainWindow.VirusImportedFuncs(funcName), 0)
                                      Next


                                      FreeLibrary(NSudoDevilModeModuleHandle)
                                      If score > VirusScore Then Return True

                                      Return False
                                  Catch err As Exception
                                      Return False
                                  End Try
                              End Function)
    End Function
    Public Async Function apiHezhongCloudScan(filePath As String) As Task(Of Boolean)
        Return Await Task.Run(Async Function()
                                  Dim url As String = "http://c.hezhongkj.top/scan"
                                  Dim token As String = "a323a4a2-d096-4dd5-b69d-6bc5b6b9ae64"
                                  Dim fileMD5 = getMD5(filePath)


                                  Dim data As New With {
                                    .token = token,
                                    .md5 = fileMD5
                                  }

                                  Dim JsonData = JsonConvert.SerializeObject(data)

                                  Dim byteArray As Byte() = Encoding.UTF8.GetBytes(JsonData)

                                  ' 设置请求
                                  Dim request As WebRequest = WebRequest.Create(url)
                                  request.Method = "POST"
                                  request.ContentType = "application/json"
                                  request.ContentLength = byteArray.Length

                                  ' 写入请求数据
                                  Using stream As Stream = request.GetRequestStream()
                                      stream.Write(byteArray, 0, byteArray.Length)
                                  End Using

                                  ' 发送请求并获取响应
                                  Try
                                      Using response As WebResponse = request.GetResponse()
                                          ' 读取响应
                                          Using reader As New StreamReader(response.GetResponseStream())
                                              Dim responseFromServer As String = reader.ReadToEnd()

                                              Dim responseObject As HezhongCloudResult = JsonConvert.DeserializeObject(Of HezhongCloudResult)(responseFromServer)
                                              Debug.Print(responseObject.score)
                                              If CInt(responseObject.score) > 50 Then Return True
                                          End Using
                                      End Using

                                  Catch ex As Exception
                                  End Try

                                  Return False
                              End Function)
    End Function
    Public Async Function apiMD5Scan(fileMD5 As String) As Task(Of Boolean)
        Return Await Task.Run(Function()
                                  If VirusMD5.Contains(fileMD5) Then
                                      Return True
                                  End If

                                  Return False
                              End Function)
    End Function
End Class

