using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Mouse Button Up Event Args
    /// </summary>
    public class MouseButtonUpEventArgs : HandledEventArgs
    {
        public MouseButtonUpEventArgs(EMouseButton button)
        {
            Button = button;
        }

        /// <summary>
        /// Released mouse button
        /// </summary>
        public EMouseButton Button { get; }
    }
}