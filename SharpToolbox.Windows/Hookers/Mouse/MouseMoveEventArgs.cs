using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Mouse Move Event Args
    /// </summary>
    public class MouseMoveEventArgs : HandledEventArgs
    {
        public MouseMoveEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Mouse X location
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Mouse Y location
        /// </summary>
        public int Y { get; }
    }
}