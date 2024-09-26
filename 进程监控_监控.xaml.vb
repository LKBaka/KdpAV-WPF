Imports System.Windows.Forms
Imports KdpAV_WPF.Win32API, System.Windows.Interop
Public Class 进程监控_监控
    Dim WithEvents timer1 As New Forms.Timer
    Dim lastProcess As New List(Of Integer)
    Dim s As New Scan(MainWindow.VirusFuncsData, MainWindow.VirusScore)
    Dim WithEvents 托盘 As New System.Windows.Forms.NotifyIcon
    Dim ScanEngine = New List(Of String)

    Private Sub 进程监控_监控_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        timer1.Interval = 15
        timer1.Start()

        Hide()

        托盘.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath)
        托盘.Text = "KdpAV-WPF"
        托盘.Visible = True
        If MainWindow.config("ANK引擎") = "True" Then ScanEngine.Add("ANK")
        If MainWindow.config("导入表引擎") = "True" Then ScanEngine.Add("PEData")
        If MainWindow.config("猎剑云引擎") = "True" Then ScanEngine.Add("PiTui")

    End Sub

    Private Async Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        Await Task.Run(
            Async Function()

                ScanEngine = New List(Of String)
                If MainWindow.config("ANK引擎") = "True" Then ScanEngine.Add("ANK")
                If MainWindow.config("导入表引擎") = "True" Then ScanEngine.Add("PEData")
                If MainWindow.config("猎剑云引擎") = "True" Then ScanEngine.Add("PiTui")

                For Each proc In Process.GetProcesses
                    Try
                        If lastProcess.Contains(proc.Id) Then Continue For '如果被扫描过就跳过
                        lastProcess.Add(proc.Id)

                        If proc.MainModule.FileName.Contains(Environment.GetFolderPath(Environment.SpecialFolder.Windows)) Then Continue For

                        '如果是Windows文件夹下的文件就跳过


                        Dim result = False
                        Dim results As New List(Of Boolean)

                        If ScanEngine.Contains("PEData") Then
                            result = Await s.api_PEDataScan(proc.MainModule.FileName)
                            results.Add(result)
                        End If

                        If ScanEngine.Contains("ANK") Then
                            result = Await s.api_ANKScan(proc.MainModule.FileName)
                            results.Add(result)
                        End If

                        If ScanEngine.Contains("PiTui") Then
                            result = Await s.api_PiTuiScan(proc.MainModule.FileName)
                            results.Add(result)
                        End If

                        result = results.Contains(True)

                        If Not result Then Continue For '没有威胁就跳过




                    Catch err As System.NullReferenceException
                        Debug.Print(err.Message)
                    Catch ex As Exception
                        Debug.Print(ex.Message)
                    End Try
                Next
            End Function)
    End Sub

    Private Sub 托盘_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles 托盘.Click
        Dim m1 As New MenuItem("× 退出")

        Dim contextMenu As New Forms.ContextMenu
        contextMenu.MenuItems.Add(m1)

        AddHandler m1.Click, AddressOf ExitApplication_Click

        托盘.ContextMenu = contextMenu
        'Dim 托盘存在 = False
        'For Each f As Window In Application.Current.Windows
        '    If f.Name = "NotfifyIcon" Then 托盘存在 = True
        'Next

        'If Not 托盘存在 Then
        '    Dim frm As New 托盘菜单

        '    Dim p As New POINT
        '    If GetCursorPos(p) Then
        '        Dispatcher.Invoke(
        '            Sub()
        '                frm.Show()
        '                frm.Topmost = True
        '                frm.Left = p.X - frm.Width / 2
        '                frm.Top = p.Y - frm.Height - 25
        '            End Sub)
        '    End If

        'End If
    End Sub

    Public Sub ExitApplication_Click()
        Application.Current.Shutdown()
    End Sub
End Class
