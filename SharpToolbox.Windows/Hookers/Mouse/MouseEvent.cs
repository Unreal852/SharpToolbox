namespace SharpToolbox.Windows.Hookers.Mouse;

/// <summary>
/// Mouse event
/// </summary>
public enum MouseEvent : uint
{
    /// <summary>
    /// Left Button Down
    /// </summary>
    LeftDown = 0x00000002,
    /// <summary>
    /// Left Button Up
    /// </summary>
    LeftUp = 0x00000004,
    /// <summary>
    /// Middle Button Down
    /// </summary>
    MiddleDown = 0x00000020,
    /// <summary>
    /// Middle Button Up
    /// </summary>
    MiddleUp = 0x00000040,
    /// <summary>
    /// Mouse Move
    /// </summary>
    Move = 0x00000001,
    /// <summary>
    /// Uh ?
    /// </summary>
    Absolute = 0x00008000,
    /// <summary>
    /// Right Button Down
    /// </summary>
    RightDown = 0x00000008,
    /// <summary>
    /// Right Button Up
    /// </summary>
    RightUp = 0x00000010
}