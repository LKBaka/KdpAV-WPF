Imports System.ComponentModel
Imports System.Runtime.InteropServices

Class HomePage
    Dim HomePageVM As New HomePageViewModel(Me)
    Dim WithEvents t As New Windows.Forms.Timer With {.Interval = 50} '间隔50ms


    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.DataContext = HomePageVM

        AddHandler t.Tick, AddressOf HomePageVM.t_Tick
        t.Start()

    End Sub

End Class