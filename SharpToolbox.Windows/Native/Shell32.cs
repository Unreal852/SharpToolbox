using System;
using System.Runtime.InteropServices;

namespace SharpToolbox.Windows.Native
{
    public static class Shell32
    {
        [DllImport("Shell32.dll")]
        public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)]Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);
    }
}