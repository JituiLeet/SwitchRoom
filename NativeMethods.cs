using System; using System.Runtime.InteropServices;
namespace SwitchRoom { internal static class NativeMethods {
[DllImport("user32.dll",SetLastError=true)] internal static extern bool RegisterHotKey(IntPtr hWnd,int id,uint modifiers,uint key);
[DllImport("user32.dll",SetLastError=true)] internal static extern bool UnregisterHotKey(IntPtr hWnd,int id);
internal const int WM_HOTKEY=0x0312; internal const uint MOD_ALT=1,MOD_CONTROL=2;
} }