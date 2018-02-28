// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System;
using System.Runtime.InteropServices;

namespace Dminator
{
    internal static class Win32
    {
        public const int MouseeventfLeftdown = 0x02;
        public const int MouseeventfLeftup = 0x04;

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}