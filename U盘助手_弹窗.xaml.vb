Imports System.ComponentModel

Public Class U盘助手_弹窗

    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public DiskName As String = "" '盘符
    Public VolumeLabel = "" ' 磁盘名
    Public FreeSize As ULong = 0

    Private _TitleContent = ""
    Private _SubTitleContent = ""

    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub
    Public Property TitleContent As String
        Get
            Return _TitleContent
        End Get
        Set(value As String)
            _TitleContent = value
            OnPropertyChanged("TitleContent")
        End Set
    End Property
    Public Property SubTitleContent As String
        Get
            Return _SubTitleContent
        End Get
        Set(value As String)
            _SubTitleContent = value
            OnPropertyChanged("SubTitleContent")
        End Set
    End Property


    Public Sub New(argDiskName As String, argVolumeLabel As String, argTotalFreeSize As ULong)
        InitializeComponent()

        DiskName = argDiskName
        VolumeLabel = argVolumeLabel
        FreeSize = argTotalFreeSize
        DataContext = Me
    End Sub

    Private Sub U盘助手_弹窗_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TitleContent = VolumeLabel & " [" & DiskName.Replace("\", "") & "]"
        SubTitleContent = "剩余空间:" & BytesToString(FreeSize)
    End Sub

    ' 将字节转换为GB的字符串表示
    Function BytesToString(ByVal byteCount As Long) As String
        Dim sizes() As String = {"Bytes", "KB", "MB", "GB", "TB"}
        If byteCount = 0 Then
            Return "0 Bytes"
        End If
        Dim bytes As Double = Math.Abs(byteCount)
        Dim place As Integer = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)))
        Dim num As Double = Math.Round(bytes / Math.Pow(1024, place), 1)
        Return Math.Sign(byteCount) * num & " " & sizes(place)
    End Function

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Process.Start("explorer", " " & DiskName) '通过explorer的启动参数跳转至磁盘
    End Sub

End Class
