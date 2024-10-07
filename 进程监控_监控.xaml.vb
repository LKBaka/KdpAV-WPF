Imports System.ComponentModel
Imports System.Windows.Forms
Imports KdpAV_WPF.Win32API, System.Windows.Interop
Public Class 进程监控_监控
    Dim WithEvents timer1 As New Forms.Timer
    Dim lastProcess As New List(Of Integer)
    Dim s As New Scan(App.VirusFuncsData, App.VirusScore)
    Dim WithEvents 托盘 As New System.Windows.Forms.NotifyIcon
    Dim ScanEngine = New List(Of String)

    Private Sub 进程监控_监控_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        timer1.Interval = 15
        timer1.Start()

        Hide()

        托盘.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath)
        托盘.Text = "KdpAV-WPF"
        托盘.Visible = True
        If App.config("ANK引擎") = "True" Then ScanEngine.Add("ANK")
        If App.config("导入表引擎") = "True" Then ScanEngine.Add("PEData")
        If App.config("猎剑云引擎") = "True" Then ScanEngine.Add("PiTui")

    End Sub

    Private Async Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        Await Task.Run(
            Async Function()

                ScanEngine = New List(Of String)
                If App.config("ANK引擎") = "True" Then ScanEngine.Add("ANK")
                If App.config("导入表引擎") = "True" Then ScanEngine.Add("PEData")
                If App.config("猎剑云引擎") = "True" Then ScanEngine.Add("PiTui")

                For Each proc In Process.GetProcesses
                    Try
                        If lastProcess.Contains(proc.Id) Then Continue For '如果被扫描过就跳过
                        lastProcess.Add(proc.Id)
                        Try

                            If proc.HasExited Then Continue For '若进程退出则跳过

                            '检查文件名是否为空
                            If (Not proc.HasExited) And (Not String.IsNullOrEmpty(proc.MainModule.FileName)) Then

                                Dim f = proc.MainModule.FileName
                                ' 检查是否为系统进程
                                If proc.SessionId = 0 Or proc.StartInfo.UserName = "NT AUTHORITY\SYSTEM" Then Continue For


                                Dim result = False
                                Dim results As New List(Of Boolean)

                                If ScanEngine.Contains("PEData") Then
                                    result = Await s.api_PEDataScan(f)
                                    results.Add(result)
                                End If

                                If ScanEngine.Contains("ANK") Then
                                    result = Await s.api_ANKScan(f)
                                    results.Add(result)
                                End If

                                If ScanEngine.Contains("PiTui") Then
                                    result = Await s.api_PiTuiScan(f)
                                    results.Add(result)
                                End If

                                result = results.Contains(True)

                                If Not result Then Continue For '没有威胁就跳过

                                Dispatcher.Invoke(
                                    Sub()
                                        Dim tip As New 进程监控_提示(proc.ProcessName, f)
                                        tip.Show()
                                    End Sub)

                            End If
                        Catch ex As Exception
                            Debug.Print(ex.Message)
                        Catch err As System.NullReferenceException
                            Debug.Print(err.Message)
                        Catch err As Win32Exception
                            Debug.Print(err.Message)
                        Catch err As InvalidOperationException
                            Debug.Print(err.Message)
                        End Try
                    Catch ex As Exception
                        Debug.Print(ex.Message)
                    End Try
                Next
            End Function)
    End Sub

    Private Sub 托盘_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles 托盘.Click
        Dim m1 As New MenuItem("退出")
        Dim m2 As New MenuItem("主界面")

        Dim contextMenu As New Forms.ContextMenu
        contextMenu.MenuItems.Add(m2)
        contextMenu.MenuItems.Add(m1)

        AddHandler m1.Click, AddressOf ExitApplication_Click
        AddHandler m2.Click, AddressOf StartMainWindow_Click

        托盘.ContextMenu = contextMenu
    End Sub

    Public Sub ExitApplication_Click()
        Application.Current.Shutdown()
    End Sub
    Public Sub StartMainWindow_Click()
        Dim m As New MainWindow
        m.Show()
    End Sub
End Class
