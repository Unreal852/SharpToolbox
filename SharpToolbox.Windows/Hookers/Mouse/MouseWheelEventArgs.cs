using System.ComponentModel;

namespace SharpToolbox.Windows.Hookers.Mouse;

/// <summary>
/// Mouse Wheel Event Args
/// </summary>
public class MouseWheelEventArgs : HandledEventArgs
{
    public MouseWheelEventArgs(MouseWheelDirection wheelDirection)
    {
        WheelDirection = wheelDirection;
    }

    /// <summary>
    /// Mouse wheel direction
    /// </summary>
    public MouseWheelDirection WheelDirection { get; }
}