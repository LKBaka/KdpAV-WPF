Imports KdpAV_WPF.Win32API, System.Threading
Class TaskMgr_性能_CPU
    Dim WithEvents t As New Windows.Forms.Timer
    Dim cpuCounter As New PerformanceCounter("Processor", "% Processor Time", "_Total")
    Dim ramCounter As PerformanceCounter
    Private Sub TaskMgr_性能_CPU_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        t.Interval = 1000
        t.Start()
    End Sub


    'Public Shared Function FileTimeToInt64(ft As FILETIME) As ULong
    '    Return (CUInt(ft.dwHighDateTime) << 32) Or ft.dwLowDateTime
    'End Function

    'Public Shared Function GetCpuUsage() As CPU_USAGE
    '    Dim cpuUsage As New CPU_USAGE()
    '    Dim idleTime As FILETIME, kernelTime As FILETIME, userTime As FILETIME

    '    If GetSystemTimes(idleTime, kernelTime, userTime) Then
    '        cpuUsage.idleTime = FileTimeToInt64(idleTime)
    '        cpuUsage.kernelTime = FileTimeToInt64(kernelTime)
    '        cpuUsage.userTime = FileTimeToInt64(userTime)
    '    End If

    '    Return cpuUsage
    'End Function

    'Public Shared Function CalculateCpuUsage(previousSample As CPU_USAGE, currentSample As CPU_USAGE, numberOfCores As Integer) As Single
    '    Try
    '        Dim idleDifference As ULong = currentSample.idleTime - previousSample.idleTime
    '        Dim kernelDifference As ULong = currentSample.kernelTime - previousSample.kernelTime
    '        Dim userDifference As ULong = currentSample.userTime - previousSample.userTime

    '        Dim systemTime As ULong = kernelDifference + userDifference
    '        Dim total As ULong = systemTime + idleDifference

    '        Dim percentage As Single = CSng((100.0 * (systemTime - idleDifference) / total) / numberOfCores)

    '        Return percentage
    '    Catch ex As Exception

    '    End Try
    'End Function

    Private Sub t_Tick(sender As Object, e As EventArgs) Handles t.Tick
        ' Dim cpu = CInt(cpuCounter.NextValue()) & "%"

    End Sub
End Class
