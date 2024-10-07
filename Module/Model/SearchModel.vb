Imports System.ComponentModel

Public Class SearchModel
    Private _SearchText = ""
    Private _SearchBoxHeight = 0
    Private _Results = New List(Of String)
    Private _SelectedFunction = ""

    Public Property SearchText As String
        Get
            Return _SearchText
        End Get
        Set(value As String)
            _SearchText = value
        End Set
    End Property
    Public Property SearchBoxHeight As Integer
        Get
            Return _SearchBoxHeight
        End Get
        Set(value As Integer)
            _SearchBoxHeight = value
        End Set
    End Property

    Public Property Results As List(Of String)
        Get
            Return _Results
        End Get
        Set(value As List(Of String))
            _Results = value
        End Set
    End Property

    Public Property SelectedFunction As String
        Get
            Return _SelectedFunction
        End Get
        Set(value As String)
            _SelectedFunction = value
        End Set
    End Property
End Class