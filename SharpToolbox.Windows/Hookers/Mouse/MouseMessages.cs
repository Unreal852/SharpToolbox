namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Mouse Windows Messages
    /// </summary>
    public enum MouseMessages
    {
        /// <summary>
        /// Left Button Down Message
        /// </summary>
        WM_LBUTTONDOWN = 0x0201,
        /// <summary>
        /// Left Button Up Message
        /// </summary>
        WM_LBUTTONUP = 0x0202,
        /// <summary>
        /// Mouse Move Message
        /// </summary>
        WM_MOUSEMOVE = 0x0200,
        /// <summary>
        /// Move Wheel Message
        /// </summary>
        WM_MOUSEWHEEL = 0x020A,
        /// <summary>
        /// Right Button Down Message
        /// </summary>
        WM_RBUTTONDOWN = 0x0204,
        /// <summary>
        /// Right Button Up Message
        /// </summary>
        WM_RBUTTONUP = 0x0205,
        /// <summary>
        /// left Button Double Click Message
        /// </summary>
        WM_LBUTTONDBLCLK = 0x0203,
        /// <summary>
        /// Middle Button Down Message
        /// </summary>
        WM_MBUTTONDOWN = 0x0207,
        /// <summary>
        /// Middle Button Up Message
        /// </summary>
        WM_MBUTTONUP = 0x0208
    }
}