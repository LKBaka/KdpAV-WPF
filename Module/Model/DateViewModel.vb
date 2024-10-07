Imports System.ComponentModel
Imports KdpAV_WPF.DateModel

Public Class DateViewModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private _DateModel As New DateModel

    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub
    Public Property DateModel As DateModel
        Get
            If _DateModel Is Nothing Then
                _DateModel = New DateModel
            End If
            Return _DateModel
        End Get
        Set(value As DateModel)
            _DateModel = value
            OnPropertyChanged("DateModel")
        End Set
    End Property

    Public Property DateHourMinuteSecondContent As String
        Get
            Return DateModel.DateHourMinuteSecondContent
        End Get
        Set(value As String)
            DateModel.DateHourMinuteSecondContent = value
            OnPropertyChanged("DateHourMinuteSecondContent")
        End Set
    End Property
    Public Property DateYearMonthDayContent As String
        Get
            Return DateModel.DateYearMonthDayContent
        End Get
        Set(value As String)
            DateModel.DateHourMinuteSecondContent = value
            OnPropertyChanged("DateYearMonthDayContent")
        End Set
    End Property


End Class
