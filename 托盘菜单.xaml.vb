Public Class 托盘菜单

    Private Sub Button_Exit_Click(sender As Object, e As RoutedEventArgs) Handles Button_Exit.Click
        Application.Current.Shutdown()
    End Sub

    Private Sub 托盘菜单_Deactivated(sender As Object, e As EventArgs) Handles Me.Deactivated
        Try
            Close()
            '若不被激活就销毁
        Catch ex As Exception

        End Try
    End Sub

    Private Sub 托盘菜单_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Topmost = True
    End Sub
End Class
