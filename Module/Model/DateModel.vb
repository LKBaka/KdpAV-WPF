Imports System.ComponentModel

Public Class DateModel
    Private _DateHourMinuteSecondContent = ""
    '小时 分钟 秒 （Hour Minute Second）
    Private _DateYearMonthDayContent = $"{DateTime.Now.Year}年{DateTime.Now.Month}月{DateTime.Now.Day}日"
    '年月日 (Year Month Day)
    Public Property DateHourMinuteSecondContent As String
        Get
            Return _DateHourMinuteSecondContent
        End Get
        Set(value As String)
            _DateHourMinuteSecondContent = value
        End Set
    End Property
    Public Property DateYearMonthDayContent As String
        Get
            Return _DateYearMonthDayContent
        End Get
        Set(value As String)
            _DateYearMonthDayContent = value
        End Set
    End Property
End Class