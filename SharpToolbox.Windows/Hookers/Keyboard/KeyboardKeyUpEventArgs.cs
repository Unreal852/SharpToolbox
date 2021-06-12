using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Keyboard
{
    /// <summary>
    /// Keyboard Key Up Event Args
    /// </summary>
    public class KeyboardKeyUpEventArgs : HandledEventArgs
    {
        public KeyboardKeyUpEventArgs(EVirtualKeys key)
        {
            Key = key;
        }

        /// <summary>
        /// Released key
        /// </summary>
        public EVirtualKeys Key { get; }
    }
}