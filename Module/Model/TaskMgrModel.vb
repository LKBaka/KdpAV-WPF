Imports System.ComponentModel

Public Class TaskMgrModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub


    Private _ProcItems As New List(Of String)
    Private _SelectedItem As String
    Private _SelectedIndex As Integer
    Private _SearchProcess As String = ""
    Public Property ProcItems As List(Of String)
        Get
            Return _ProcItems
        End Get
        Set(value As List(Of String))
            _ProcItems = value
            OnPropertyChanged("ProcItems")
        End Set
    End Property

    Public Property SelectedItem As String
        Get
            Return _SelectedItem
        End Get
        Set(value As String)
            _SelectedItem = value
            OnPropertyChanged("SelectedItem")
        End Set
    End Property

    Public Property SearchProcess As String
        Get
            Return _SearchProcess
        End Get
        Set(value As String)
            _SearchProcess = value
            OnPropertyChanged("SearchProcess")
        End Set
    End Property

    Public Property SelectedIndex As Integer
        Get
            Return _SelectedIndex
        End Get
        Set(value As Integer)
            _SelectedIndex = value
            OnPropertyChanged("SelectedIndex")
        End Set
    End Property

End Class