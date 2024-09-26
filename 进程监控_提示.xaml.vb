Imports iNKORE.UI.WPF.Modern.Controls
Public Class 进程监控_提示
    Dim ProcessName, ProcessPath
    Public Sub New(argProcessName As String, argProcessPath As String)
        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        ProcessName = argProcessName
        ProcessPath = argProcessPath

    End Sub


    Private Sub 进程监控_提示_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.Topmost = True
        LblProcessName.Content = "进程名:" & ProcessName
        LblProcessPath.Content = "路径:" & ProcessPath
    End Sub

    Private Async Sub Button_OK_Click(sender As Object, e As RoutedEventArgs) Handles Button_OK.Click
        If ComboBox1.Items(ComboBox1.SelectedIndex).Content = "仅结束进程" Then
            For Each p In Process.GetProcessesByName(ProcessName)
                p.Kill()
                Dim msg As New ContentDialog With {
                    .Title = "操作成功完成!",
                    .Content = "进程 " & ProcessName & " 已被结束",
                    .CloseButtonText = "确定",
                    .DefaultButton = ContentDialogButton.Close
                }
                Await msg.ShowAsync
            Next

        ElseIf ComboBox1.Items(ComboBox1.SelectedIndex).Content = "忽略" Then

        End If
        Me.Close()
    End Sub
End Class
