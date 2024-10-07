Imports System.ComponentModel

Public Class ScanModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _IsScanning As Boolean

    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub


    Private _SelcetedPath = "C:\"
    Private _ScanningFile = "扫描完成，共发现0个病毒"
    Public Property SelectedPath As String
        Get
            Return _SelcetedPath
        End Get
        Set(value As String)
            _SelcetedPath = value
            OnPropertyChanged("SelectedPath")
        End Set
    End Property
    Public Property ScanningFile As String
        Get
            Return _ScanningFile
        End Get
        Set(value As String)
            _ScanningFile = value
            OnPropertyChanged("ScanningFile")
        End Set
    End Property

    Public Property IsScanning As Boolean
        Get
            Return _IsScanning
        End Get
        Set(value As Boolean)
            _IsScanning = value
            OnPropertyChanged("IsScanning")
        End Set
    End Property

    Private _ANK引擎 = False
    Private _导入表引擎 = False
    Private _猎剑云引擎 = False
    Public Property ANK引擎 As Boolean
        Get
            Return _ANK引擎
        End Get
        Set(value As Boolean)
            _ANK引擎 = value
            OnPropertyChanged("ANK引擎")
        End Set
    End Property
    Public Property 导入表引擎 As Boolean
        Get
            Return _导入表引擎
        End Get
        Set(value As Boolean)
            _导入表引擎 = value
            OnPropertyChanged("导入表引擎")
        End Set
    End Property

    Public Property 猎剑云引擎 As Boolean
        Get
            Return _猎剑云引擎
        End Get
        Set(value As Boolean)
            _猎剑云引擎 = value
            OnPropertyChanged("猎剑云引擎")
        End Set
    End Property
End Class