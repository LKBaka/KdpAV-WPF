Imports System.Runtime.InteropServices
Imports System.Text


Class WIN32API
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode)>
    Shared Function LoadLibrary(lpLibFileName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Shared Function FreeLibrary(hLibModule As IntPtr) As Boolean
    End Function
    ' 定义 FindWindow 函数
    <DllImport("user32.dll", SetLastError:=True)>
    Shared Function FindWindow(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpClassName As String, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpWindowName As String) As IntPtr
    End Function

    ' 定义 FindWindowEx 函数
    <DllImport("user32.dll", SetLastError:=True)>
    Shared Function FindWindowEx(ByVal hWndParent As IntPtr, ByVal hWndChildAfter As IntPtr, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpszClass As String, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpszWindow As String) As IntPtr
    End Function
    ' 定义 SendMessage 函数
    <DllImport("user32.dll", SetLastError:=True)>
    Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    ' 定义 GetWindowText 函数
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Shared Function GetWindowText(ByVal hWnd As IntPtr, ByVal lpString As StringBuilder, ByVal nMaxCount As Integer) As Integer
    End Function

    ' 定义 GetWindowTextLength 函数
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Shared Function GetWindowTextLength(ByVal hWnd As IntPtr) As Integer
    End Function

End Class
