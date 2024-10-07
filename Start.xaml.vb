Public Class App
    Public Shared VirusMD5Data As New ArrayList 'MD5
    Public Shared VirusFuncsData As New Dictionary(Of String, Integer) '导入表分数
    Public Shared VirusScore = 125 ' 置信度
    Public Shared funcs As New List(Of String) From
    {"扫描", "任务管理器", "工具箱", "设置", "IU Song"} '功能列表

    Public Shared config As Dictionary(Of String, String) '配置

    Private Sub Start_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim mainForm As New MainWindow
        'mainForm.Show()

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

        Me.Visibility = Visibility.Hidden

    End Sub


    'For Each proc In Process.GetProcessesByName("taskmgr")
    '    Try
    '        RemoteHooking.Inject(
    '                proc.Id,
    '                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "KdpAV.Hooker.dll"),
    '                GetType(KdpAV.Hooker.TerminateProcessHook).Assembly.Location)
    '    Catch ex As Exception
    '        'MsgBox($"Failed to inject into process {proc.ProcessName}: {ex.Message}")
    '        Debug.Print(ex.Message)
    '    End Try
    'Next


End Class
