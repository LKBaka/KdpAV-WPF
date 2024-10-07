Public Class RelayCommand
    Implements ICommand

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
    ReadOnly _canExecute As Func(Of Boolean)
    ReadOnly _execute As Action

    Public Sub New(argAction As Action, argCanExecute As Func(Of Boolean))
        _canExecute = argCanExecute
        _execute = argAction
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        If _canExecute Is Nothing Then Return True
        Return _canExecute()
    End Function

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _execute()
    End Sub
End Class
