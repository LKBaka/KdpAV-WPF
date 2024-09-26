Class HomePage
    Dim WithEvents t As New Windows.Forms.Timer With {.Interval = 50} '间隔50ms

    Private Sub TextBox_Search_TextChanged(sender As Object, e As TextChangedEventArgs) Handles TextBox_Search.TextChanged
        ListBox_Result.Items.Clear() ' 清空搜索列表

        If TextBox_Search.Text = "" Then Exit Sub '判断是否为空

        For Each f As String In MainWindow.funcs
            If f.Contains(TextBox_Search.Text) Then
                If ListBox_Result.Items.Contains(f) Then Continue For
                ListBox_Result.Items.Add(f)
            End If
        Next
    End Sub

    Private Sub Button_OK_Click(sender As Object, e As RoutedEventArgs) Handles Button_OK.Click
        Try
            Select Case ListBox_Result.Items(ListBox_Result.SelectedIndex)
                Case "扫描"
                    MainWindow.page = "ScanPage"
                Case "任务管理器"
                    Dim tsk As New TaskMgr
                    tsk.Show()
                Case "工具箱"
                    MainWindow.page = "ToolPage"
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub t_Tick(sender As Object, e As EventArgs) Handles t.Tick
        ListBox_Result.Height = CDbl(42 * If(ListBox_Result.Items.Count <= 3, ListBox_Result.Items.Count, 3))
        '若搜索出的数量大于三 高度 = 3乘42 否则 搜索出的数量乘42

        '判断 时和分 是否为个位数，是就添零 否则不添
        lblTitle.Content = If(TimeOfDay.Hour.ToString.Count = 1, "0" + TimeOfDay.Hour.ToString, TimeOfDay.Hour) & "：" & If(TimeOfDay.Minute.ToString.Count = 1, "0" + TimeOfDay.Minute.ToString, TimeOfDay.Minute)
    End Sub

    Private Sub HomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        t.Start()
    End Sub
End Class