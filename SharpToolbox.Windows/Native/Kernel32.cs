using System;
using System.Runtime.InteropServices;

namespace SharpToolbox.Windows.Native;

public class Kernel32
{
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string lpFileName);
}