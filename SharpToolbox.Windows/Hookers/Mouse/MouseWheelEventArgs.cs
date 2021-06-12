using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Mouse Wheel Event Args
    /// </summary>
    public class MouseWheelEventArgs : HandledEventArgs
    {
        public MouseWheelEventArgs(EMouseWheelDirection wheelDirection)
        {
            WheelDirection = wheelDirection;
        }

        /// <summary>
        /// Mouse wheel direction
        /// </summary>
        public EMouseWheelDirection WheelDirection { get; }
    }
}