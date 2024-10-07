Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Media.Imaging
Public Class HomePageViewModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private HomePage As HomePage

    Private _SearchViewModel As SearchViewModel
    Private _DateViewModel As New DateViewModel
    Private _ImagePath = CommonVars.AppPath & "\Me\bmw.png"

    Public Sub New(argHomePage As HomePage)
        HomePage = argHomePage
    End Sub
    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub
    Public Property SearchViewModel As SearchViewModel
        Get
            If _SearchViewModel Is Nothing Then
                _SearchViewModel = New SearchViewModel(HomePage)
            End If
            Return _SearchViewModel
        End Get
        Set(value As SearchViewModel)
            _SearchViewModel = value
            OnPropertyChanged("SearchViewModel")
        End Set
    End Property

    Public Property DateViewModel As DateViewModel
        Get
            If _DateViewModel Is Nothing Then
                _DateViewModel = New DateViewModel
            End If
            Return _DateViewModel
        End Get
        Set(value As DateViewModel)
            _DateViewModel = value
            OnPropertyChanged("DateViewModel")
        End Set
    End Property

    Public Property ImagePath As String
        Get
            Return _ImagePath
        End Get
        Set(value As String)
            _ImagePath = value
            OnPropertyChanged("ImagePath")
        End Set
    End Property

    Public Sub t_Tick(sender As Object, e As EventArgs)
        SearchViewModel.SearchBoxHeight = 126

        DateViewModel.DateHourMinuteSecondContent =
        $"{If(TimeOfDay.Hour.ToString.Count = 1, "0" + TimeOfDay.Hour.ToString, TimeOfDay.Hour)}
         :{If(TimeOfDay.Minute.ToString.Count = 1, "0" + TimeOfDay.Minute.ToString, TimeOfDay.Minute)}
         :{If(TimeOfDay.Second.ToString.Count = 1, "0" + TimeOfDay.Second.ToString, TimeOfDay.Second)}".Replace(vbCrLf, "").Replace(" ", "")
        DateViewModel.DateYearMonthDayContent = $"{DateTime.Now.Year}年{DateTime.Now.Month}月{DateTime.Now.Day}日"
    End Sub



End Class


Public Class ImageConvert
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim bi As New BitmapImage()
        ' BitmapImage.UriSource must be in a BeginInit/EndInit block.
        bi.BeginInit()
        bi.UriSource = New Uri(value.ToString(), UriKind.Absolute)
        bi.EndInit()
        Return bi
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Nothing
    End Function
End Class

