Imports iNKORE.UI.WPF.Modern.Controls
Imports System.Reflection

Class TaskMgr
    Dim WithEvents timer1 As New Forms.Timer With {
        .Interval = 200
    }
    Private Async Sub KillProcess_Click(sender As Object, e As RoutedEventArgs)
        Dim selValue = ListBox_Process.Items(ListBox_Process.SelectedIndex)
        Debug.Print(selValue)
        For Each p In Process.GetProcessesByName(selValue)
            p.Kill()
        Next

        Dim msg As New ContentDialog With {
                    .Title = "操作成功完成!",
                    .Content = "进程 " & selValue & " 已被结束",
                    .CloseButtonText = "确定",
                    .DefaultButton = ContentDialogButton.Close
            }
        Await msg.ShowAsync

    End Sub


    Private Sub TaskMgr_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        timer1.Start()
    End Sub
    Private Sub RefreshProcessList()
        Dim tmp As New ArrayList
        For Each p In Process.GetProcesses
            tmp.Add(p.ProcessName)
        Next

        ListBox_Process.ItemsSource = tmp
    End Sub
    Private Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        RefreshProcessList()

        'ramCounter = New PerformanceCounter("Memory", "Available MBytes")
        'Console.WriteLine("电脑可使用内存：" + ramCounter.NextValue() + "MB")






    End Sub

    Private Sub Refresh_Click(sender As Object, e As RoutedEventArgs)
        RefreshProcessList()
    End Sub

    Private Sub OpenFilePath_Click(sender As Object, e As RoutedEventArgs)
        For Each p In Process.GetProcessesByName(ListBox_Process.Items(ListBox_Process.SelectedIndex))
            Dim procInfo As New ProcessStartInfo With {
                .FileName = "explorer.exe",
                .Arguments = " " & "/select," & p.MainModule.FileName
            }
            Dim proc As New Process With {
                .StartInfo = procInfo
            }
            proc.Start()

            Exit For
        Next
    End Sub

    Private Sub Open_Click(sender As Object, e As RoutedEventArgs)
        Dim ofd As New Windows.Forms.OpenFileDialog With {
            .Title = "选择欲打开的文件"
        }


        If ofd.ShowDialog = Forms.DialogResult.OK Then
            Process.Start(ofd.FileName)
        End If
    End Sub

    Private Sub Close_Form_Click(sender As Object, e As RoutedEventArgs)
        Close()
    End Sub

    'Private Sub Button_CPU_Click(sender As Object, e As RoutedEventArgs) Handles Button_CPU.Click
    '    contentframe.Navigate(New Uri("TaskMgr_性能_CPU.xaml", UriKind.Relative))
    'End Sub

    'Private Sub tabc_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tabc.SelectionChanged
    '    If tabc.SelectedIndex <> 1 Then
    '        If contentframe Is Nothing Then Exit Sub
    '        contentframe.Navigate(New Uri("BlankPage.xaml", UriKind.Relative))
    '    End If
    'End Sub
End Class
