Imports iNKORE.UI.WPF.Modern.Controls
Public Class 文件监控_提示
    Dim FilePath
    Public Sub New(argFilePath As String)
        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        FilePath = argFilePath

    End Sub


    Private Sub 进程监控_提示_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        LblFilePath.Content = "文件路径:" & FilePath

        Me.Topmost = False
    End Sub

    Private Async Sub Button_OK_Click(sender As Object, e As RoutedEventArgs) Handles Button_OK.Click
        If ComboBox1.Items(ComboBox1.SelectedIndex).Content = "删除文件" Then
            IO.File.Delete(FilePath)
            Dim msg As New ContentDialog With {
                .Title = "操作成功完成!",
                .Content = "威胁 " & FilePath & " 已被删除",
                .CloseButtonText = "确定",
                .DefaultButton = ContentDialogButton.Close
            }
            Await msg.ShowAsync

        ElseIf ComboBox1.Items(ComboBox1.SelectedIndex).Content = "忽略" Then

        End If
        Me.Close()
    End Sub
End Class
