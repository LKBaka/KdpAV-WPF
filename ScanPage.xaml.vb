Imports System.Windows.Forms
Imports System.Threading

Class ScanPage
    Public ScanEngine As New List(Of String) From {"PEData"}
    Public VirusMD5Data = MainWindow.VirusMD5Data
    Public VirusPEFuncsData = MainWindow.VirusFuncsData
    Public VirusScore = MainWindow.VirusScore
    Private Sub Button_Broswer_Click(sender As Object, e As RoutedEventArgs) Handles Button_Broswer.Click
        Dim fbl As New FolderBrowserDialog With {
            .Description = "选择欲扫描的文件夹"
        }

        If fbl.ShowDialog = DialogResult.OK Then
            TextBox_Path.Text = fbl.SelectedPath
        End If
    End Sub

    Private Async Sub Button_StartScan_Click(sender As Object, e As RoutedEventArgs) Handles Button_StartScan.Click
        sender.IsEnabled = False
        Button_RemoveVirus.IsEnabled = False
        Dim sw As New Stopwatch
        ListBox_Virus.Items.Clear()

        sw.Start()
        Await Scan(TextBox_Path.Text)
        sw.Stop()

        Button_RemoveVirus.IsEnabled = True
        sender.IsEnabled = True
        lblScanningFile.Content =
        "扫描完成，共发现" & ListBox_Virus.Items.Count & "个威胁。" &
        "耗时" & sw.Elapsed.Hours & "小时" & sw.Elapsed.Minutes & "分" & sw.Elapsed.Seconds & "秒"
    End Sub
    Public Async Function Scan(ByVal scanPath As String) As Tasks.Task
        Dim clsScan As Scan = Nothing
        Try
            For Each f As String In IO.Directory.GetFiles(scanPath)
                Dim result = False
                Await task.Run(Async Function()
                                   Dispatcher.Invoke(Sub() lblScanningFile.Content = "正在扫描:" & f)

                                   If ScanEngine.Contains("MD5") Then
                                       clsScan = New Scan(VirusMD5Data)
                                       result = clsScan.api_MD5Scan(clsScan.getMD5(f))
                                       Debug.Print(f & " " & result)

                                       If result Then
                                           Dispatcher.Invoke(Sub() ListBox_Virus.Items.Add(f))
                                           Exit Function
                                       End If
                                   End If

                                   If ScanEngine.Contains("PEData") Then
                                       clsScan = New Scan(VirusPEFuncsData, VirusScore)
                                       result = Await clsScan.api_PEDataScan(f)
                                       Debug.Print(f & " " & result)

                                       If result Then
                                           Dispatcher.Invoke(Sub() ListBox_Virus.Items.Add(f))
                                           Exit Function
                                       End If
                                   End If

                                   If ScanEngine.Contains("ANK") Then
                                       clsScan = New Scan()
                                       result = Await clsScan.api_ANKScan(f)
                                       Debug.Print(f & " " & result)

                                       If result Then
                                           Dispatcher.Invoke(Sub() ListBox_Virus.Items.Add(f))
                                           Exit Function
                                       End If
                                   End If

                                   If ScanEngine.Contains("PiTui") Then
                                       clsScan = New Scan()
                                       result = Await clsScan.api_PiTuiScan(f)
                                       Debug.Print(f & " " & result)

                                       If result Then
                                           Dispatcher.Invoke(Sub() ListBox_Virus.Items.Add(f))
                                           Exit Function
                                       End If
                                   End If
                               End Function)
            Next

            For Each d As String In IO.Directory.GetDirectories(scanPath)
                Await Scan(d)
            Next
        Catch
        End Try
    End Function
      
    Private Sub Button_RemoveVirus_Click(sender As Object, e As RoutedEventArgs) Handles Button_RemoveVirus.Click
        For fileIndex = ListBox_Virus.Items.Count - 1 To 0 Step -1
            'Debug.Print(ListBox_Virus.Items(fileIndex))
            IO.File.Delete(ListBox_Virus.Items(fileIndex))
            ListBox_Virus.Items.RemoveAt(fileIndex)
        Next
    End Sub
     
    Private Sub ScanPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ScanEngine = New List(Of String)
        If MainWindow.config("ANK引擎") = "True" Then ScanEngine.Add("ANK")
        If MainWindow.config("导入表引擎") = "True" Then ScanEngine.Add("PEData")
        If MainWindow.config("猎剑云引擎") = "True" Then ScanEngine.Add("PiTui")
    End Sub
End Class
