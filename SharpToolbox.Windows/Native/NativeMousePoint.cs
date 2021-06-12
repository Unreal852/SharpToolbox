using System.Runtime.InteropServices;

namespace SharpToolbox.Windows.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMousePoint
    {
        public int x;
        public int y;

        public NativeMousePoint(int xPos, int yPos)
        {
            x = xPos;
            y = yPos;
        }
    }
}