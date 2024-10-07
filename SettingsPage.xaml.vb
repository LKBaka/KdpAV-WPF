Imports System.ComponentModel

Class SettingsPage
    Dim SetthingsViewModel As New SetthingsViewModel(Me)

    Public Sub w()
        Dim cfg As New config(CommonVars.AppPath & "\config")
        cfg.WriteAllConfigs(App.config)
    End Sub
    Private Sub CheckBoxANKEngine_Checked(sender As Object, e As RoutedEventArgs) Handles CheckBoxANKEngine.Checked
        App.config("ANK引擎") = "True"
        w()
    End Sub
    Private Sub CheckBoxANKEngine_Unchecked(sender As Object, e As RoutedEventArgs) Handles CheckBoxANKEngine.Unchecked
        App.config("ANK引擎") = "False"
        w()
    End Sub

    Private Sub CheckBoxPEDataEngine_Checked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPEDataEngine.Checked
        App.config("导入表引擎") = "True"
        w()
    End Sub
    Private Sub CheckBoxPEDataEngine_Unchecked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPEDataEngine.Unchecked
        App.config("导入表引擎") = "False"
        w()
    End Sub




    Private Sub CheckBoxPiTuiEngine_Checked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPiTuiEngine.Checked
        App.config("猎剑云引擎") = "True"
        w()
    End Sub

    Private Sub CheckBoxPiTuiEngine_Unchecked(sender As Object, e As RoutedEventArgs) Handles CheckBoxPiTuiEngine.Unchecked
        App.config("猎剑云引擎") = "False"
        w()

    End Sub
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        DataContext = SetthingsViewModel
    End Sub
End Class


