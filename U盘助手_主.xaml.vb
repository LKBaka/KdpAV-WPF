Imports System.IO

Public Class U盘助手_主
    Dim lastDrives As New List(Of String)
    Dim WithEvents t As New Windows.Forms.Timer
    Private Sub U盘助手_主_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Hide() ' 隐藏
        t.Interval = 2500 '间隔2.5秒
        t.Start()
    End Sub

    Private Sub t_Tick(sender As Object, e As EventArgs) Handles t.Tick
        For Each drive In DriveInfo.GetDrives()
            Try
                If lastDrives.Contains(drive.Name) Then Continue For
                lastDrives.Add(drive.Name)
                If drive.DriveType <> DriveType.Removable Then Continue For

                Debug.Print(drive.Name)
                Dim frm As New U盘助手_弹窗(drive.Name, drive.VolumeLabel, drive.TotalFreeSpace)
                frm.Show()

                '移动弹窗到右上角
                frm.Left = SystemParameters.PrimaryScreenWidth - frm.Width - 15
                frm.Top = 0 + 15
            Catch ex As Exception
            End Try
        Next

        '检测U盘是否拔出
        Dim Drives = DriveInfo.GetDrives()
        Dim DrivesNames = New ArrayList
        For Each d In Drives
            DrivesNames.Add(d.Name)
        Next

        For Each drive In DrivesNames
            For lastDriveIdx = lastDrives.Count - 1 To 0 Step -1
                Dim lastDrive = lastDrives(lastDriveIdx)
                If DrivesNames.Contains(lastDrive) Then Continue For Else lastDrives.Remove(lastDrive)
                '若当前磁盘名列表包含此磁盘名就跳过 否则删除在访问过的磁盘名列表 的磁盘名

                '检查是否有对应磁盘的弹窗
                For Each f In Application.Current.Windows
                    If f.Name <> "UDiskHelperWindow" Then Continue For
                    Debug.Print(f.Name & " " & drive)

                    If Not DrivesNames.Contains(f.DiskName) Then f.Close() Else  '如果不包含此磁盘关闭此窗口
                Next
            Next
        Next

    End Sub
End Class
