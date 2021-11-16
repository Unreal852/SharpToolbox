using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Keyboard;

/// <summary>
/// Keyboard Key Down Event Args
/// </summary>
public class KeyboardKeyDownEventArgs : HandledEventArgs
{
    public KeyboardKeyDownEventArgs(VirtualKeys key)
    {
        Key = key;
    }

    /// <summary>
    /// Pressed key
    /// </summary>
    public VirtualKeys Key { get; }
}