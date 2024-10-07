Imports System.ComponentModel
Imports System.ServiceModel
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Threading

Public Class ScanViewModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private _ScanModel As New ScanModel
    Private Page As ScanPage

    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New(argPage As Object)
        Page = argPage
    End Sub

    Public Property ScanModel As ScanModel
        Get
            Return _ScanModel
        End Get
        Set(value As ScanModel)
            _ScanModel = value
            OnPropertyChanged("ScanModel")
        End Set
    End Property
#Region "相关信息"

    Public Property SelectedPath As String
        Get
            Return ScanModel.SelectedPath
        End Get
        Set(value As String)
            ScanModel.SelectedPath = value
            OnPropertyChanged("SelectedPath")
        End Set
    End Property

    Public Property ScanningFile As String
        Get
            Return ScanModel.ScanningFile
        End Get
        Set(value As String)
            ScanModel.ScanningFile = value
            OnPropertyChanged("ScanningFile")
        End Set
    End Property
    Public Property IsScanning As Boolean
        Get
            Return ScanModel.IsScanning
        End Get
        Set(value As Boolean)
            ScanModel.IsScanning = value
            OnPropertyChanged("IsScanning")
        End Set
    End Property
#End Region
#Region "引擎"
    Public Property ANK引擎 As Boolean
        Get
            Return ScanModel.ANK引擎
        End Get
        Set(value As Boolean)
            ScanModel.ANK引擎 = value
            OnPropertyChanged("ANK引擎")
        End Set
    End Property
    Public Property 导入表引擎 As Boolean
        Get
            Return ScanModel.导入表引擎
        End Get
        Set(value As Boolean)
            ScanModel.导入表引擎 = value
            OnPropertyChanged("导入表引擎")
        End Set
    End Property

    Public Property 猎剑云引擎 As Boolean
        Get
            Return ScanModel.猎剑云引擎
        End Get
        Set(value As Boolean)
            ScanModel.猎剑云引擎 = value
            OnPropertyChanged("猎剑云引擎")
        End Set
    End Property


#End Region

    '选择界面
    Public Sub SelectPath()
        Dim fbl As New FolderBrowserDialog With {
            .Description = "选择欲扫描的文件夹"
        }

        If fbl.ShowDialog = DialogResult.OK Then
            SelectedPath = fbl.SelectedPath
        End If
    End Sub

    '开始扫描
    Public Async Sub StartScan()
        IsScanning = True
        Page.PBar.Value = 0
        Page.PBar.IsIndeterminate = True

        Dim sw As New Stopwatch
        Page.ListBox_Virus.Items.Clear()

        sw.Start()
        Await Page.Scan(SelectedPath)
        sw.Stop()
        Page.PBar.IsIndeterminate = False
        Page.PBar.Value = 100


        IsScanning = False

        ScanningFile =
        "扫描完成，共发现" & Page.ListBox_Virus.Items.Count & "个威胁。" &
        "耗时" & sw.Elapsed.Hours & "小时" & sw.Elapsed.Minutes & "分" & sw.Elapsed.Seconds & "秒"
    End Sub




#Region "Action委托"
    Public ReadOnly Property SelectPathAction As ICommand
        Get
            Return New RelayCommand(AddressOf SelectPath,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property

    Public ReadOnly Property StartScanAction As ICommand
        Get
            Return New RelayCommand(AddressOf StartScan,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property
#End Region
End Class
Public Class InverseColorConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        If TypeOf value Is SolidColorBrush Then
            Dim originalColorBrush As SolidColorBrush = DirectCast(value, SolidColorBrush)
            Dim invertedColor As Color = Color.FromArgb(CByte(255 - originalColorBrush.Color.A),
                                                        CByte(255 - originalColorBrush.Color.R),
                                                        CByte(255 - originalColorBrush.Color.G),
                                                        CByte(255 - originalColorBrush.Color.B))
            Return New SolidColorBrush(invertedColor)
        End If
        Return value
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class