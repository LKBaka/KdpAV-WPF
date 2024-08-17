Imports System.IO
Imports WPFTest.KdpConst
Imports System.Windows.Threading
Imports WPFTest.config
Class MainWindow
    Private WithEvents Timer1 As New DispatcherTimer
    Public Shared Page
    Public Shared VirusMD5 As New List(Of String)
    Public Shared VirusImportedFuncs As Dictionary(Of String, Integer)
    Private Async Sub Init()
        Await Task.Run(Sub()
                           VirusImportedFuncs = getVirusImportedFuncs()
                           Debug.Print(VirusImportedFuncs.Keys.Count)

                           Dim tmpMD5 As New List(Of String)
                           For Each filePath In Directory.GetFiles($"{AppPath}\MD5\")
                               Using sr As New StreamReader(filePath)
                                   Dim subTmpMD5 = sr.ReadToEnd.Split(vbCrLf).ToList
                                   For Each m In subTmpMD5
                                       tmpMD5.Add(m)
                                   Next
                               End Using
                           Next

                           VirusMD5 = tmpMD5

                       End Sub)
    End Sub
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Timer1.Interval = TimeSpan.FromSeconds(0)
        Timer1.Start()

        Page = "Home"

        Init()
    End Sub
    Private Sub NavigateToPage(pageName As String)

        mainFrame.Navigate(New Uri($"{pageName}.xaml", UriKind.Relative))
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        NavigateToPage(Page)
    End Sub
End Class
