using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Mouse Button Up Event Args
    /// </summary>
    public class MouseButtonUpEventArgs : HandledEventArgs
    {
        public MouseButtonUpEventArgs(MouseButton button)
        {
            Button = button;
        }

        /// <summary>
        /// Released mouse button
        /// </summary>
        public MouseButton Button { get; }
    }
}