using System.Runtime.InteropServices;

MessageBox(IntPtr.Zero, "command-line message box", "Attention!", 0);

[DllImport("user32.dll",CharSet=CharSet.Unicode,SetLastError =true)]
static extern int MessageBox(IntPtr hwnd, string lpText, string lpCaption, uint uType);