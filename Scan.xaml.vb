Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.Win32
Class ScanPage
    Dim clsScan = Nothing
    Public ScanEngine As New List(Of String) From {"MD5", "PEImportedFuncs"}
    Private Sub Title_TouchDown(sender As Object, e As EventArgs) Handles Title.MouseDown
        MainWindow.Page = "Home"
    End Sub

    Private Async Sub StartScan_Click(sender As Object, e As RoutedEventArgs) Handles StartScan.Click
        Dim fbl As New FolderBrowserDialog
        If fbl.ShowDialog = DialogResult.OK Then
            VirusBox.Items.Clear()

            ScanDirPath.Text = fbl.SelectedPath
            Await __Scan(ScanDirPath.Text)
            ScaningFilePath.Content = $"查杀已完成,共发现{VirusBox.Items.Count}个病毒"
        End If
    End Sub

    Private Async Function __Scan(dirPath) As Task
        For Each filePath In Directory.GetFiles(dirPath)
            Debug.Print(filePath)
            Dispatcher.Invoke(Sub() ScaningFilePath.Content = $"正在扫描:{filePath}")

            clsScan = New Scan(MainWindow.VirusMD5)
            If ScanEngine.Contains("MD5") Then
                Dim result = Await clsScan.apiMD5Scan(clsScan.getMD5(filePath))
                If result Then
                    Dispatcher.Invoke(Sub()
                                          Dim virusItem As New ListBoxItem() With {
                                                .Content = filePath
                                          }
                                          VirusBox.Items.Add(virusItem)
                                      End Sub)
                End If
            End If

            clsScan = New Scan(MainWindow.VirusImportedFuncs)
            If ScanEngine.Contains("PEImportedFuncs") Then
                Dim result = Await clsScan.apiPeScan(filePath)
                If result Then
                    Dispatcher.Invoke(Sub()
                                          Dim virusItem As New ListBoxItem() With {
                                                .Content = filePath
                                          }
                                          VirusBox.Items.Add(virusItem)
                                      End Sub)
                End If
            End If

            If ScanEngine.Contains("HezhongCloud") Then
                Dim result = Await clsScan.apiHezhongCloudScan(filePath)
                If result Then
                    Dispatcher.Invoke(Sub()
                                          Dim virusItem As New ListBoxItem() With {
                                                .Content = filePath
                                          }
                                          VirusBox.Items.Add(virusItem)
                                      End Sub)
                End If
                Thread.Sleep(200)

            End If
        Next

        For Each subDirPath In Directory.GetDirectories(dirPath)
            Await __Scan(subDirPath)
        Next
    End Function

    Private Sub ClearVirus_Click(sender As Object, e As RoutedEventArgs) Handles ClearVirus.Click
        Try
            For virusIndex = VirusBox.Items.Count - 1 To 0 Step -1
                IO.File.Delete(VirusBox.Items(virusIndex).Content)
                VirusBox.Items.RemoveAt(virusIndex)
            Next
        Catch ex As Exception
            MessageBox.Show($"发生了错误!请将此窗口截图给开发人员{vbCrLf}{ex.Message}", "KdpAV-WPF")
        End Try
    End Sub
End Class
