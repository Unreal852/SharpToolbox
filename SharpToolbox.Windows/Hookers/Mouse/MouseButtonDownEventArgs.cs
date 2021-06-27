using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Mouse Button Down Event Args
    /// </summary>
    public class MouseButtonDownEventArgs : HandledEventArgs
    {
        public MouseButtonDownEventArgs(MouseButton button)
        {
            Button = button;
        }

        /// <summary>
        /// Pressed mouse button
        /// </summary>
        public MouseButton Button { get; }
    }
}