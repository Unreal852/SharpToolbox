using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Keyboard
{
    /// <summary>
    /// Keyboard Key Down Event Args
    /// </summary>
    public class KeyboardKeyDownEventArgs : HandledEventArgs
    {
        public KeyboardKeyDownEventArgs(EVirtualKeys key)
        {
            Key = key;
        }

        /// <summary>
        /// Pressed key
        /// </summary>
        public EVirtualKeys Key { get; }
    }
}