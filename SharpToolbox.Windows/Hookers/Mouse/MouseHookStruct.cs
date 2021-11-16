using System;
using System.Runtime.InteropServices;
using SharpToolbox.Windows.Native;

namespace SharpToolbox.Windows.Hookers.Mouse;

/// <summary>
/// Mouse Hook Structure
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal class MouseHookStruct
{
    public NativeMousePoint pt;
    public uint             mouseData;
    public uint             flags;
    public uint             time;
    public IntPtr           dwExtraInfo;
}