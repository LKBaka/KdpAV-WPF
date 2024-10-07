Imports System.ComponentModel
Imports KdpAV_WPF.DateModel

Public Class SearchViewModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private _SearchModel As New SearchModel
    Private HomePage As HomePage

    Public Sub New(argHomePage As HomePage)
        HomePage = argHomePage
    End Sub

    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub
    Public Property SearchModel As SearchModel
        Get
            If _SearchModel Is Nothing Then
                _SearchModel = New SearchModel
            End If
            Return _SearchModel
        End Get
        Set(value As SearchModel)
            _SearchModel = value
            OnPropertyChanged("SearchModel")
        End Set
    End Property


    Public Property SearchText As String
        Get
            Return SearchModel.SearchText
        End Get
        Set(value As String)
            SearchModel.SearchText = value
            OnPropertyChanged("SearchText")
        End Set
    End Property

    Public Property SearchBoxHeight As Integer
        Get
            Return SearchModel.SearchBoxHeight
        End Get
        Set(value As Integer)
            SearchModel.SearchBoxHeight = value
            OnPropertyChanged("SearchBoxHeight")
        End Set
    End Property

    Public Property SelectedFunction As String
        Get
            Return SearchModel.SelectedFunction
        End Get
        Set(value As String)
            SearchModel.SelectedFunction = value
            OnPropertyChanged("SelectedFunction")
        End Set
    End Property

    Public Property Results As List(Of String)
        Get
            Return SearchModel.Results
        End Get
        Set(value As List(Of String))
            SearchModel.Results = value
            OnPropertyChanged("Results")
        End Set
    End Property

    Public Sub SearchFunction() '搜索功能
        Debug.Print(SearchText)
        If SearchText = "" Then Return '内容为空
        Dim tmp = New List(Of String)
        For Each f In App.funcs
            Debug.Print(f.Contains(SearchText))
            If f.Contains(SearchText) Then
                tmp.Add(f)
                HomePage.ListBox_Result.ItemsSource = tmp
            End If
        Next

    End Sub


    Public Sub CallFunc() '使用功能
        Try
            Select Case SelectedFunction
                Case "扫描"
                    MainWindow.page = "ScanPage"
                Case "任务管理器"
                    Dim tsk As New TaskMgr
                    tsk.Show()
                Case "工具箱"
                    MainWindow.page = "ToolPage"
                Case "设置"
                    MainWindow.page = "SettingsPage"
                Case "IU Song"
                    Dim IU_Song As New IUSong_1
                    IU_Song.Show()
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Public ReadOnly Property CallFuncAction As ICommand
        Get
            Return New RelayCommand(AddressOf CallFunc,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property
    Public ReadOnly Property SearchFunctionAction As ICommand
        Get
            Return New RelayCommand(AddressOf SearchFunction,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property
End Class
