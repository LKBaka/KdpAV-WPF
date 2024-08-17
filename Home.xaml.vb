Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window
Imports System.Windows.Controls
Class Home
    Private Sub Home_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        _Title.Content = $"您好! {Environment.UserName}"
        SubTitle.Content = $"您需要进行什么操作? {Environment.UserName}"

    End Sub

    Private Sub Go_ScanPage_Click(sender As Object, e As RoutedEventArgs) Handles Go_ScanPage.Click
        MainWindow.Page = "Scan"
    End Sub

End Class
