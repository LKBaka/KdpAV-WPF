Imports System.Windows.Forms
Imports System.Threading
Imports System.ComponentModel

Class ScanPage
    Public ScanEngine As New List(Of String) From {"PEData"}
    Public VirusMD5Data = App.VirusMD5Data
    Public VirusPEFuncsData = App.VirusFuncsData
    Public VirusScore = App.VirusScore
    Dim ScanViewModel As New ScanViewModel(Me)
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        DataContext = ScanViewModel
    End Sub

    Public Async Function Scan(ByVal scanPath As String) As Tasks.Task
        Dim clsScan As Scan = Nothing
        Try
            For Each f As String In IO.Directory.GetFiles(scanPath)
                Dim result = False
                Await Task.Run(Async Function()
                                   Dispatcher.Invoke(Sub() ScanViewModel.ScanningFile = "正在扫描:" & f)

                                   If ScanEngine.Contains("MD5") Then
                                       clsScan = New Scan(VirusMD5Data)
                                       result = clsScan.api_MD5Scan(clsScan.getMD5(f))
                                       Debug.Print(f & " " & result)

                                       If result Then
                                           Dispatcher.Invoke(Sub() ListBox_Virus.Items.Add(f))
                                           Return
                                       End If
                                   End If

                                   If ScanEngine.Contains("PEData") Then
                                       clsScan = New Scan(VirusPEFuncsData, VirusScore)
                                       result = Await clsScan.api_PEDataScan(f)
                                       Debug.Print(f & " " & result)

                                       If result Then
                                           Dispatcher.Invoke(Sub() ListBox_Virus.Items.Add(f))
                                           Return
                                       End If
                                   End If

                                   If ScanEngine.Contains("ANK") Then
                                       clsScan = New Scan()
                                       result = Await clsScan.api_ANKScan(f)
                                       Debug.Print(f & " " & result)

                                       If result Then
                                           Dispatcher.Invoke(Sub() ListBox_Virus.Items.Add(f))
                                           Return
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
            IO.File.Delete(ListBox_Virus.Items(fileIndex))
            ListBox_Virus.Items.RemoveAt(fileIndex)
        Next
    End Sub

    Private Sub ScanPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ScanEngine = New List(Of String)
        If App.config("ANK引擎") = "True" Then ScanEngine.Add("ANK")
        If App.config("导入表引擎") = "True" Then ScanEngine.Add("PEData")
        If App.config("猎剑云引擎") = "True" Then ScanEngine.Add("PiTui")
    End Sub

End Class
