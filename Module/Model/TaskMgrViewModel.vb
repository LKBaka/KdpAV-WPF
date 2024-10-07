Imports System.ComponentModel
Imports System.Net.Security
Imports iNKORE.UI.WPF.Modern.Controls
Imports KdpAV_WPF.DateModel

Public Class TaskMgrViewModel
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private _TaskMgrModel As New TaskMgrModel
    Private frm As Window
    Public Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
    End Sub
    Public Property TaskMgrModel As TaskMgrModel
        Get
            If _TaskMgrModel Is Nothing Then
                _TaskMgrModel = New TaskMgrModel
            End If
            Return _TaskMgrModel
        End Get
        Set(value As TaskMgrModel)
            _TaskMgrModel = value
            OnPropertyChanged("TaskMgrModel")
        End Set
    End Property
    Public Property ProcItems As List(Of String)
        Get
            Return TaskMgrModel.ProcItems
        End Get
        Set(value As List(Of String))
            TaskMgrModel.ProcItems = value
            OnPropertyChanged("ProcItems")
        End Set
    End Property

    Public Property SelectedItem As String
        Get
            Return TaskMgrModel.SelectedItem
        End Get
        Set(value As String)
            TaskMgrModel.SelectedItem = value
            OnPropertyChanged("SelectedItem")
        End Set
    End Property
    Public Property SelectedIndex As Integer
        Get
            Return TaskMgrModel.SelectedIndex
        End Get
        Set(value As Integer)
            TaskMgrModel.SelectedIndex = value
            OnPropertyChanged("SelectedIndex")
        End Set
    End Property

    Public Property SearchProcess As String
        Get
            Return TaskMgrModel.SearchProcess
        End Get
        Set(value As String)
            TaskMgrModel.SearchProcess = value
            OnPropertyChanged("SearchProcess")
        End Set
    End Property

    Public Async Sub RefreshProcessList()
        Await Task.Run(
            Sub()

                Dim tmp As New List(Of String)
                If SearchProcess <> "" Then
                    For Each p In Process.GetProcesses
                        If p.ProcessName = "KdpAV-WPF" Then Continue For
                        If p.ProcessName.Contains(SearchProcess) Then
                            tmp.Add(p.ProcessName)
                        End If
                    Next
                Else
                    For Each p In Process.GetProcesses
                        If p.ProcessName = "KdpAV-WPF" Then Continue For
                        tmp.Add(p.ProcessName)
                    Next
                End If



                ProcItems = tmp
            End Sub)
    End Sub


    Private Sub Close_Form()
        frm.Close()
    End Sub

    Private Sub OpenFile()
        Dim ofd As New Windows.Forms.OpenFileDialog With {
            .Title = "选择欲打开的文件"
        }


        If ofd.ShowDialog = Forms.DialogResult.OK Then
            Process.Start(ofd.FileName)
        End If
    End Sub

    Private Sub OpenSelectedFilePath()
        For Each p In Process.GetProcessesByName(SelectedItem)
            Dim procInfo As New ProcessStartInfo With {
                .FileName = "explorer.exe",
                .Arguments = " " & "/select," & p.MainModule.FileName
            }
            Dim proc As New Process With {
                .StartInfo = procInfo
            }
            proc.Start()

            Exit For
        Next
    End Sub

    Private Async Sub KillSelectedProcess()
        Debug.Print(SelectedItem)
        For Each p In Process.GetProcessesByName(SelectedItem)
            p.Kill()
        Next

        Dim msg As New ContentDialog With {
            .Title = "操作成功完成!",
            .Content = "进程 " & SelectedItem & " 已被结束",
            .CloseButtonText = "确定",
            .DefaultButton = ContentDialogButton.Close
        }
        Await msg.ShowAsync

    End Sub

    Private Async Sub FindProcess()
        Await Task.Run(
           Sub()
               'TaskMgrModel = TaskMgrModel
               If SearchProcess = "" Then Exit Sub
               Dim tmp As New List(Of String)
               For Each p In Process.GetProcesses
                   If p.ProcessName = "KdpAV-WPF" Then Continue For
                   If p.ProcessName.Contains(SearchProcess) Then
                       tmp.Add(p.ProcessName)
                   End If
               Next

               ProcItems = tmp
           End Sub)
    End Sub

    Public ReadOnly Property FindProcessAction As ICommand
        Get
            Return New RelayCommand(AddressOf FindProcess,
                                 Function()
                                     Return True
                                 End Function)
        End Get
    End Property
    Public ReadOnly Property RefreshProcessListAction As ICommand
        Get
            Return New RelayCommand(AddressOf RefreshProcessList,
                                 Function()
                                     Return True
                                 End Function)
        End Get
    End Property

    Public ReadOnly Property KillSelectedProcessAction As ICommand
        Get
            Return New RelayCommand(AddressOf KillSelectedProcess,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property


    Public ReadOnly Property OpenSelectedProcFilePathAction As ICommand
        Get
            Return New RelayCommand(AddressOf OpenSelectedFilePath,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property
    Public ReadOnly Property OpenFileAction As ICommand
        Get
            Return New RelayCommand(AddressOf OpenFile,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property

    Public ReadOnly Property Close_FormAction As ICommand
        Get
            Return New RelayCommand(AddressOf Close_Form,
                                    Function()
                                        Return True
                                    End Function)
        End Get
    End Property

    Public Sub New(window As Window)
        frm = window
    End Sub
End Class
