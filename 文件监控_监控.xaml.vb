Imports System.IO

Public Class 文件监控_监控
    Public WithEvents watcherDesktop As IO.FileSystemWatcher
    Public WithEvents watcherDownload As IO.FileSystemWatcher

    Private Sub 文件监控_监控_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Visibility = Windows.Visibility.Hidden
        ' 初始化FileSystemWatcher
        watcherDesktop = New FileSystemWatcher With {
            .Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) ' 设置要监控的路径
        }
        watcherDownload = New FileSystemWatcher With {
            .Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads") ' 设置要监控的路径
        }
        ' 添加事件处理程序
        AddHandler watcherDesktop.Created, AddressOf OnFileChanged
        AddHandler watcherDesktop.Changed, AddressOf OnFileChanged
        ' 添加事件处理程序
        AddHandler watcherDownload.Created, AddressOf OnFileChanged
        AddHandler watcherDownload.Changed, AddressOf OnFileChanged


        ' 启用监控
        watcherDesktop.EnableRaisingEvents = True
        watcherDownload.EnableRaisingEvents = True
    End Sub


   
    ' 文件创建或修改时触发的事件处理程序
    Private Sub OnFileChanged(sender As Object, e As FileSystemEventArgs)
        Dim s As New Scan(App.VirusFuncsData, 125)
        Dim result = s.noAsync_api_PEDataScan(e.FullPath)
        Debug.Print(result & " " & e.FullPath)
        If result Then
            Dispatcher.Invoke(
                Sub()
                    Dim frm As New 文件监控_提示(e.FullPath)
                    frm.Show()
                End Sub)
        End If
    End Sub
End Class
