Imports System.Windows.Forms
Imports System.Runtime.InteropServices, KdpAV_WPF.Win32API
Imports KdpAV.Hooker
Imports System.Windows.Interop

Class MainWindow
    Public WithEvents timer1 As New Timer '时钟
    Public Shared VirusMD5Data As New ArrayList 'MD5
    Public Shared VirusFuncsData As New Dictionary(Of String, Integer) '导入表分数
    Public Shared VirusScore = 125 ' 置信度
    Public Shared funcs As New List(Of String) From
    {"扫描", "任务管理器", "工具箱"} '功能列表

    Public Shared page = "HomePage" '选中页
    Public Shared config As Dictionary(Of String, String) '配置
    Private Sub SideMenu_Page_SelectionChanged(sender As iNKORE.UI.WPF.Modern.Controls.NavigationView, args As iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs) Handles SideMenu_Page.SelectionChanged

        Dim selectedItem = args.SelectedItem
        page = selectedItem.Tag
    End Sub
    Public Sub 导航页面(page As String)
        contentFrame.Navigate(New Uri(page & ".xaml", UriKind.Relative))
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        timer1.Interval = 9
        timer1.Start()

        Dim cfg As New config(CommonVars.AppPath & "\config")
        config = cfg.ReadAllConfigs()

        Debug.Print(config("进程监控") = "True")
        Debug.Print(config("文件监控") = "True")
        Debug.Print(config("U盘助手") = "True")

        If config("进程监控") = "True" Then
            Dim f As New 进程监控_监控()
            f.Show()
        End If

        If config("文件监控") = "True" Then
            Dim f As New 文件监控_监控()
            f.Show()
        End If

        If config("U盘助手") = "True" Then
            Dim f As New U盘助手_主()
            f.Show()
        End If

        cfg = New config(CommonVars.AppPath & "\MD5\") ' 读MD5
        VirusMD5Data = cfg.ReadAllMD5Data

        cfg = New config(CommonVars.AppPath & "\PEImportedFuncs") '读导入表函数分数表
        VirusFuncsData = cfg.ReadAllPEImortedFuncsData

        SideMenu_Page.IsPaneOpen = False '收缩侧边栏

    End Sub

    Private Async Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        Await Task.Run(
            Sub()
                Dispatcher.Invoke(
                Sub()
                    Dim Open = SideMenu_Page.IsPaneOpen
                    contentFrame.Width = If(Open, 481, 697)

                    导航页面(page)
                End Sub)
            End Sub)

    End Sub

End Class
