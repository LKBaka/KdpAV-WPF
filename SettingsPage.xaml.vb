Class SettingsPage
    Public Sub w()
        Dim cfg As New config(CommonVars.AppPath & "\config")
        cfg.WriteAllConfigs(MainWindow.config)
    End Sub
    Private Sub CheckBoxANKEngine_Checked(sender As Object, e As RoutedEventArgs) Handles CheckBoxANKEngine.Checked
        MainWindow.config("ANK引擎") = "True"
        w()
    End Sub
    Private Sub CheckBoxANKEngine_Unchecked(sender As Object, e As RoutedEventArgs) Handles CheckBoxANKEngine.Unchecked
        MainWindow.config("ANK引擎") = "False"
        w()
    End Sub

    Private Sub CheckBoxPEDataEngine_Checked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPEDataEngine.Checked
        MainWindow.config("导入表引擎") = "True"
        w()
    End Sub
    Private Sub CheckBoxPEDataEngine_Unchecked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPEDataEngine.Unchecked
        MainWindow.config("导入表引擎") = "False"
        w()
    End Sub




    Private Sub CheckBoxPiTuiEngine_Checked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPiTuiEngine.Checked
        MainWindow.config("猎剑云引擎") = "True"
        w()
    End Sub

    Private Sub CheckBoxPiTuiEngine_Unchecked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPiTuiEngine.Unchecked
        MainWindow.config("猎剑云引擎") = "False"
        w()

    End Sub
    Private Sub SettingsPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim cfg As New config(CommonVars.AppPath & "\config")
        Dim c = cfg.ReadAllConfigs()

        CheckBoxANKEngine.IsChecked = CBool(c("ANK引擎") = "True")
        CheckBoxPEDataEngine.IsChecked = CBool(c("导入表引擎") = "True")
        CheckBoxPiTuiEngine.IsChecked = CBool(c("猎剑云引擎") = "True")
         
    End Sub
End Class


