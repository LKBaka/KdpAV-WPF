Imports iNKORE.UI.WPF.Modern.Controls
Imports System.Reflection

Class TaskMgr
    Dim WithEvents timer1 As New Forms.Timer With {
        .Interval = 1000
    }
    Dim TaskMgrViewModel As New TaskMgrViewModel(Me)



    Private Sub TaskMgr_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        timer1.Start()
    End Sub

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        DataContext = TaskMgrViewModel
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        TaskMgrViewModel.RefreshProcessList()
    End Sub

End Class
