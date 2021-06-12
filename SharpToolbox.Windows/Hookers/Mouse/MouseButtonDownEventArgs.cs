using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Mouse Button Down Event Args
    /// </summary>
    public class MouseButtonDownEventArgs : HandledEventArgs
    {
        public MouseButtonDownEventArgs(EMouseButton button)
        {
            Button = button;
        }

        /// <summary>
        /// Pressed mouse button
        /// </summary>
        public EMouseButton Button { get; }
    }
}