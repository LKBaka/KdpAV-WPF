Imports System.Windows.Media.Animation
Imports iNKORE.UI.WPF.Modern.Controls
Imports Microsoft.Win32

Class ToolBox
    Private Async Sub FixTaskmgr_Click(sender As Object, e As RoutedEventArgs) Handles FixTaskmgr.Click
        Using key As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System")
            If key IsNot Nothing Then key.SetValue("DisableTaskmgr", 0)
        End Using

        Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System")
            If key IsNot Nothing AndAlso key.GetValue("DisableTaskmgr") = 0 Then
                Dim Msg As New ContentDialog With {
                   .Title = "KdpAV-WPF",
                   .Content = $"修改成功",
                   .CloseButtonText = $"确定",
                   .DefaultButton = ContentDialogButton.Close
                }
                Await Msg.ShowAsync()
            Else
                Dim Msg As New ContentDialog With {
                   .Title = "KdpAV-WPF",
                   .Content = $"修改失败!",
                   .CloseButtonText = $"确定",
                   .DefaultButton = ContentDialogButton.Close
                }
                Await Msg.ShowAsync()
            End If
        End Using
    End Sub

    Private Sub Title_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Title.MouseDown
        MainWindow.Page = "Home"
    End Sub

    Private Async Sub FixRegistryTools_Click(sender As Object, e As RoutedEventArgs) Handles FixRegistryTools.Click
        Using key As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System")
            If key IsNot Nothing Then key.SetValue("DisableRegistryTools", 0)
        End Using

        Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System")
            If key IsNot Nothing AndAlso key.GetValue("DisableRegistryTools") = 0 Then
                Dim Msg As New ContentDialog With {
                   .Title = "KdpAV-WPF",
                   .Content = $"修改成功",
                   .CloseButtonText = $"确定",
                   .DefaultButton = ContentDialogButton.Close
                }
                Await Msg.ShowAsync()
            Else
                Dim Msg As New ContentDialog With {
                   .Title = "KdpAV-WPF",
                   .Content = $"修改失败!",
                   .CloseButtonText = $"确定",
                   .DefaultButton = ContentDialogButton.Close
                }
                Await Msg.ShowAsync()
            End If
        End Using
    End Sub

    Private Async Sub FixCMD_Click(sender As Object, e As RoutedEventArgs) Handles FixCMD.Click
        Using key As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Policies\Microsoft\Windows\System")
            If key IsNot Nothing Then key.SetValue("DisableCMD", 0)
        End Using

        Using key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Policies\Microsoft\Windows\System")
            If key IsNot Nothing AndAlso key.GetValue("DisableCMD") = 0 Then
                Dim Msg As New ContentDialog With {
                   .Title = "KdpAV-WPF",
                   .Content = $"修改成功",
                   .CloseButtonText = $"确定",
                   .DefaultButton = ContentDialogButton.Close
                }
                Await Msg.ShowAsync()
            Else
                Dim Msg As New ContentDialog With {
                   .Title = "KdpAV-WPF",
                   .Content = $"修改失败!",
                   .CloseButtonText = $"确定",
                   .DefaultButton = ContentDialogButton.Close
                }
                Await Msg.ShowAsync()
            End If
        End Using
    End Sub

    Private Sub KdpTaskMgrWindow_Click(sender As Object, e As RoutedEventArgs) Handles KdpTaskMgrWindow.Click
        Dim tsk As New KdpTaskmgr
        tsk.Show()
    End Sub
End Class
