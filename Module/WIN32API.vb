Imports System.Runtime.InteropServices
Imports System.Windows.Interop

Public Class Win32API
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function TerminateProcess(ByVal hProcess As IntPtr, ByVal uExitCode As UInteger) As Boolean
    End Function
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
    Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long
    Public Declare Function SetWindowRgn Lib "USER32" (ByVal hWnd As Long, ByVal hRgn As Long, ByVal bRedraw As Boolean) As Long
    Public Declare Function CreateRoundRectRgn Lib "gdi32" (ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long, ByVal X3 As Long, ByVal Y3 As Long) As Long
    Public Declare Function DeleteObject Lib "gdi32" (ByVal hObject As Long) As Long
    '''ÉùÃ÷API'''
    Public Declare Function ReleaseCapture Lib "user32" () As Long
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
    Public Declare Function SetLayeredWindowAttributes Lib "user32" (ByVal hwnd As Long, ByVal crKey As Long, ByVal dwFlags As Long) As Long
    Public Declare Function SetLayeredWindowAttributes Lib "user32" (ByVal hwnd As Long, ByVal crKey As Long, ByVal bAlpha As Byte, ByVal dwFlags As Long) As Long
    Public Const WS_EX_LAYERED = &H80000
    Public Const GWL_EXSTYLE = (-20)
    Public Const LWA_ALPHA = &H2
    Public Const LWA_COLORKEY = &H1

    <DllImport("User32.dll")>
    Public Shared Sub SetCursorPos(x As Integer, y As Integer)
    End Sub

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetCursorPos(ByRef pt As POINT) As Boolean
    End Function
    ' DWMWINDOWATTRIBUTE enumeration
    Public Enum DWMWINDOWATTRIBUTE As UInteger
        DWMWA_WINDOW_CORNER_PREFERENCE = 33
    End Enum

    ' DWM_WINDOW_CORNER_PREFERENCE enumeration
    Public Enum DWM_WINDOW_CORNER_PREFERENCE As UInteger
        DWMWCP_DEFAULT = 0
        DWMWCP_DONOTROUND = 1
        DWMWCP_ROUND = 2
        DWMWCP_ROUND_SMALL = 3
    End Enum

    ' DwmSetWindowAttribute function
    <DllImport("dwmapi.dll")>
    Public Shared Function DwmSetWindowAttribute(hwnd As IntPtr, attribute As DWMWINDOWATTRIBUTE, ByRef pvAttribute As Integer, ByRef cbAttribute As UInteger) As Integer
    End Function
    <DllImport("User32.dll")>
    Public Shared Function DestroyWindow(hwnd As IntPtr) As Boolean
    End Function


    Public Declare Sub GetSystemInfo Lib "kernel32.dll" (ByRef pSI As SYSTEM_INFO)
    Public Declare Function GetSystemTimes Lib "kernel32.dll" (ByRef lpIdleTime As FILETIME, ByRef lpKernelTime As FILETIME, ByRef lpUserTime As FILETIME) As Boolean
    Public Declare Function QueryPerformanceCounter Lib "kernel32.dll" (ByRef lpPerformanceCount As Long) As Boolean
    Public Declare Function QueryPerformanceFrequency Lib "kernel32.dll" (ByRef lpFrequency As Long) As Boolean


End Class

Public Structure FILETIME
    Public dwLowDateTime As UInteger
    Public dwHighDateTime As UInteger
End Structure



Public Structure POINT
    Public X As Int32
    Public Y As Int32
    Public Sub New(x As Int32, y As Int32)
        Me.X = x
        Me.Y = y
    End Sub
End Structure

Public Structure SYSTEM_INFO
    Public wProcessorArchitecture As UShort
    Public wReserved As UShort
    Public dwPageSize As UInteger
    Public lpMinimumApplicationAddress As IntPtr
    Public lpMaximumApplicationAddress As IntPtr
    Public dwActiveProcessorMask As IntPtr
    Public dwNumberOfProcessors As UInteger
    Public dwProcessorType As UInteger
    Public dwAllocationGranularity As UInteger
    Public wProcessorLevel As UShort
    Public wProcessorRevision As UShort
End Structure

Public Structure CPU_USAGE
    Public idleTime As ULong
    Public kernelTime As ULong
    Public userTime As ULong
End Structure

