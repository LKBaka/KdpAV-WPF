Imports System.ComponentModel
Imports System.ServiceModel
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Threading

Public Class SetthingsViewModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private _ScanModel As New ScanModel
    Private Page As SettingsPage

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

    Private cfg As New config(CommonVars.AppPath & "\config")
    Private _c = cfg.ReadAllConfigs()
    Public Property c As Dictionary(Of String, String)
        Get
            Return _c
        End Get
        Set(value As Dictionary(Of String, String))
            _c = value
            OnPropertyChanged("c")
        End Set
    End Property

    Public Sub InitSettingsPage()
        ANK引擎 = CBool(c("ANK引擎") = "True")
        导入表引擎 = CBool(c("导入表引擎") = "True")
        猎剑云引擎 = CBool(c("猎剑云引擎") = "True")
    End Sub

    Public Sub 打开猎剑云官网()
        Process.Start("https://c.hezhongkj.top/")
    End Sub
#End Region
#Region "Action委托"
    Public ReadOnly Property InitSettingsPageAction As ICommand
        Get
            Return New RelayCommand(AddressOf InitSettingsPage,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property
    Public ReadOnly Property 打开猎剑云官网Action As ICommand
        Get
            Return New RelayCommand(AddressOf 打开猎剑云官网,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property
#End Region
End Class
