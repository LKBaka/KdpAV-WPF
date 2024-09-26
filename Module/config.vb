Public Class config
    Dim cfgPath As String = ""
    Public Sub New(argCfgPath As String)
        cfgPath = argcfgPath
    End Sub
    'Public Function ReadAllConfigs() As Dictionary(Of String, String)
    '    Dim cfgDict As New Dictionary(Of String, String)
    '    Using sr As New IO.StreamReader(cfgPath, System.Text.Encoding.UTF8)
    '        Dim lines = sr.ReadToEnd().Split(vbCrLf)
    '        For Each l As String In lines
    '            cfgDict.Add(l.Split("=")(0), l.Split("=")(1))
    '        Next

    '    End Using
    '    Return cfgDict
    'End Function

    Public Function ReadAllConfigs() As Dictionary(Of String, String)
        Dim cfgDict As New Dictionary(Of String, String)
        Using sr As New IO.StreamReader(cfgPath, System.Text.Encoding.UTF8)
            Dim lines = sr.ReadToEnd().Split(vbCrLf).ToList
            For Each l As String In lines
                If lines.IndexOf(l) <> 0 Then
                    cfgDict.Add(l.Split("=")(0).Replace(vbCrLf, "").Remove(0, 1), l.Split("=")(1).Replace(vbCrLf, ""))
                Else
                    cfgDict.Add(l.Split("=")(0).Replace(vbCrLf, ""), l.Split("=")(1).Replace(vbCrLf, ""))
                End If
            Next

        End Using
        Return cfgDict
    End Function

    Public Sub WriteAllConfigs(cfgs As Dictionary(Of String, String))
        Dim str = ""
        For Each tuple In cfgs.Keys.Select(Function(n, index) New With {Key .Value = n, Key .Index = index})
            str += tuple.Value & "=" & cfgs(tuple.Value) & If(tuple.Index <> cfgs.Keys.Count - 1, vbCrLf, "")
        Next
        str.Remove(str.LastIndexOf(vbCrLf), 1)
        Using sw As New IO.StreamWriter(cfgPath, False, Text.Encoding.UTF8)
            sw.Write(str)
        End Using
    End Sub
    Public Function ReadAllMD5Data() As ArrayList
        Dim lst As New ArrayList
        For Each f As String In IO.Directory.GetFiles(cfgPath)
            Dim tmpLst As ArrayList
            Using sr As New IO.StreamReader(f, System.Text.Encoding.UTF8)
                tmpLst = New ArrayList(sr.ReadToEnd().Split(vbCrLf))
            End Using
            For Each t As String In tmpLst
                lst.Add(t.Replace(vbCrLf, ""))
            Next
        Next

        Return lst
    End Function

    Public Function ReadAllPEImortedFuncsData() As Dictionary(Of String, Integer)
        Dim scoreDict As New Dictionary(Of String, Integer)

        Using sr As New IO.StreamReader(cfgPath, System.Text.Encoding.UTF8)
            Dim lines = sr.ReadToEnd().Split(vbCrLf).ToList
            For Each l As String In lines
                Try
                    If lines.IndexOf(l) <> 0 Then
                        scoreDict.Add(l.Split("=")(0).Replace(vbCrLf, "").Remove(0, 1), l.Split("=")(1).Replace(vbCrLf, ""))
                    Else
                        scoreDict.Add(l.Split("=")(0).Replace(vbCrLf, ""), l.Split("=")(1).Replace(vbCrLf, ""))
                    End If
                Catch ex As Exception
                End Try
            Next

        End Using

        Return scoreDict

    End Function
End Class
