Imports System.Globalization
Imports System.IO
Imports System.Text
Imports WPFTest.KdpConst

Public Class config

    Public Shared Sub setConfigs(configs As Dictionary(Of String, String))
        Dim writeString = ""
        Dim cfgPath As String = $"{AppPath}\config.ini"
        Using sw As New StreamWriter(cfgPath, False, Encoding.UTF8)
            For Each key In configs.Keys
                writeString += $"{key}={configs(key)}{vbCrLf}"
            Next

            sw.Write(writeString)
        End Using

    End Sub
    Public Shared Function getConfigs() As Dictionary(Of String, String)
        Dim tmpDict = New Dictionary(Of String, String)

        Using sr As New StreamReader($"{AppPath}\config.ini", encoding:=Encoding.UTF8)
            Dim srStr = sr.ReadToEnd()
            If srStr <> "" Then
                Dim srLines As New List(Of String)
                srLines = srStr.Split(vbCrLf).ToList
                For Each srLine In srLines
                    If srLine = "" Then Continue For
                    Dim srLine_Split = srLine.Split("=")
                    tmpDict.Add(srLine_Split(0), srLine_Split(1))
                Next

            End If
        End Using

        Return tmpDict
    End Function

    Public Shared Function getVirusImportedFuncs() As Dictionary(Of String, Integer)
        Dim tmpDict = New Dictionary(Of String, Integer)

        Using sr As New StreamReader($"{AppPath}\VirusImportedFuncs", encoding:=Encoding.UTF8)
            Dim srStr = sr.ReadToEnd

            If srStr = "" Then Throw New NullReferenceException("文件为空")

            Dim srImportedFuncs As New List(Of String)
            srImportedFuncs = srStr.Split(",").ToList

            For Each srImportedFunc In srImportedFuncs
                Select Case srImportedFunc '25:下 50:中下 100:中 150:中上 200:上
                    Case "SendMessageA", "Sleep", "ShellExecuteA"
                        tmpDict.Add(srImportedFunc, 50)
                    Case "RegCreateKeyExA", "RegCreateKeyA", "RegDeleteValueA", "_CorExeMain", "RegSetValueExA", "RegDeleteValueW", "memset", "RegSetValueA"
                        tmpDict.Add(srImportedFunc, 75)
                    Case "GetVersion"
                        tmpDict.Add(srImportedFunc, 40)
                    Case "SHFormatDrive", "SetFileAttributes", "TerminateProcess", "PostMessageA", "PostMessageW", "NtOpenProcess", "VirtualProtect", "PostQuitMessage", "GetProcAddress", "ShowWindow", "CreateServiceW"
                        tmpDict.Add(srImportedFunc, 100)
                    Case "VirtualAlloc", "NtProtectVirtualMemory", "NtWriteVirtualMemory", "NtAllocateVirtualMemory"
                        tmpDict.Add(srImportedFunc, 150)
                    Case "LoadLibraryW", "LoadLibraryExW"
                        tmpDict.Add(srImportedFunc, 90)
                    Case "FrostCrashedWindow", "SetWindowsHook", "WriteProcessMemory", "ZwTerminateProcess"
                        tmpDict.Add(srImportedFunc, 125)
                    Case Else
                        tmpDict.Add(srImportedFunc, 30)
                End Select
            Next
        End Using

        Return tmpDict
    End Function
End Class
