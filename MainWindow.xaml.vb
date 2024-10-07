Imports System.Windows.Forms
Imports System.Runtime.InteropServices, KdpAV_WPF.Win32API
Imports System.Windows.Interop
Imports EasyHook
Imports PeNet.Header.Resource
Imports System.IO
Imports iNKORE.UI.WPF.Modern.Controls

Class MainWindow
    Dim WithEvents timer1 As New Timer
    Public Shared page = "HomePage"
    '选中页
    Private Sub SideMenu_Page_SelectionChanged(sender As iNKORE.UI.WPF.Modern.Controls.NavigationView, args As iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs) Handles SideMenu_Page.SelectionChanged

        Dim selectedItem = args.SelectedItem
        page = selectedItem.Tag
    End Sub
    Public Sub 导航页面(page As String)
        contentFrame.Navigate(New Uri(page & ".xaml", UriKind.Relative))
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Dispatcher.Invoke(Sub() Me.Visibility = Visibility.Visible)
        DataContext = Me

        timer1.Interval = 9
        timer1.Start()
    End Sub

    Private Async Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        Await Task.Run(
            Sub()
                Dispatcher.Invoke(
                Sub()
                    contentFrame.Width = If(SideMenu_Page.IsPaneOpen, 481, 697)

                    导航页面(page)
                End Sub)
            End Sub)

    End Sub
End Class
