Imports System.Globalization

Public Class BoolConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object _
        Implements IValueConverter.Convert

        If TypeOf value Is Boolean Then
            Return Not DirectCast(value, Boolean)
        End If

        Return value
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object _
        Implements IValueConverter.ConvertBack

        If TypeOf value Is Boolean Then
            Return Not DirectCast(value, Boolean)
        End If

        Return value
    End Function
End Class
