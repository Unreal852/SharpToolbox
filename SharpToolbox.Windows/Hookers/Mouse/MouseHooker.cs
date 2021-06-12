using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpToolbox.Exceptions;
using SharpToolbox.Windows.Native;

namespace SharpToolbox.Windows.Hookers.Mouse
{
    /// <summary>
    /// Provide mouse hooks
    /// </summary>
    public class MouseHooker : IHooker
    {
        public MouseHooker()
        {
        }

        ~MouseHooker()
        {
            UnHook();
        }

        public bool IsHooked { get; private set; } = false;

        public IntPtr HookId { get; private set; } = IntPtr.Zero;

        private HookHandlerDelegate HookHandler { get; set; }
        
        /// <summary>
        /// Called when a mouse button is pressed
        /// </summary>
        public event EventHandler<MouseButtonDownEventArgs> MouseButtonDown;

        /// <summary>
        /// Called when a mouse button is released
        /// </summary>
        public event EventHandler<MouseButtonUpEventArgs> MouseButtonUp;

        /// <summary>
        /// Called when a left double click is pressed
        /// </summary>
        public event EventHandler<MouseButtonDoubleClickEventArgs> MouseButtonDoubleClick;

        /// <summary>
        /// Called when the mouse wheel move
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        /// <summary>
        /// Called when the mouse is moved
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> MouseMove;

        /// <summary>
        /// Hook handler delegate
        /// </summary>
        /// <param name="nCode">nCode</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        private delegate IntPtr HookHandlerDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        public void Hook()
        {
            if (IsHooked)
                return;
            HookHandler = Hooker;
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
            {
                Throw.IfNull(module, nameof(module), "Main module couldn't be found");
                HookId = User32.SetWindowsHookEx(14, HookHandler, Kernel32.GetModuleHandle(module.ModuleName), 0);
            }

            IsHooked = true;
        }

        public void UnHook()
        {
            if (!IsHooked)
                return;
            User32.UnhookWindowsHookEx(HookId);
            HookId = IntPtr.Zero;
            HookHandler = null;
            IsHooked = false;
        }

        /// <summary>
        /// Handle mouse events
        /// </summary>
        /// <param name="nCode">nCode</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns>IntPtr result</returns>
        private IntPtr Hooker(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return User32.CallNextHookEx(HookId, nCode, wParam, lParam);
            bool handled = false;
            switch ((EMouseMessages) wParam)
            {
                case EMouseMessages.WM_LBUTTONDOWN:
                {
                    if (MouseButtonDown == null)
                        break;
                    MouseButtonDownEventArgs e = new MouseButtonDownEventArgs(EMouseButton.Left);
                    MouseButtonDown.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_LBUTTONUP:
                {
                    if (MouseButtonUp == null)
                        break;
                    MouseButtonUpEventArgs e = new MouseButtonUpEventArgs(EMouseButton.Left);
                    MouseButtonUp.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_LBUTTONDBLCLK:
                {
                    if (MouseButtonDoubleClick == null)
                        break;
                    MouseButtonDoubleClickEventArgs e = new MouseButtonDoubleClickEventArgs();
                    MouseButtonDoubleClick.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_RBUTTONDOWN:
                {
                    if (MouseButtonDown == null)
                        break;
                    MouseButtonDownEventArgs e = new MouseButtonDownEventArgs(EMouseButton.Right);
                    MouseButtonDown.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_RBUTTONUP:
                {
                    if (MouseButtonUp == null)
                        break;
                    MouseButtonUpEventArgs e = new MouseButtonUpEventArgs(EMouseButton.Right);
                    MouseButtonUp.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_MBUTTONDOWN:
                {
                    if (MouseButtonDown == null)
                        break;
                    MouseButtonDownEventArgs e = new MouseButtonDownEventArgs(EMouseButton.Middle);
                    MouseButtonDown.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_MBUTTONUP:
                {
                    if (MouseButtonUp == null)
                        break;
                    MouseButtonUpEventArgs e = new MouseButtonUpEventArgs(EMouseButton.Middle);
                    MouseButtonUp.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_MOUSEWHEEL:
                {
                    if (MouseWheel == null)
                        break;
                    MouseHookStruct mhs = (MouseHookStruct) Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                    MouseWheelEventArgs e = new MouseWheelEventArgs(mhs.mouseData == 7864320
                        ? EMouseWheelDirection.Up
                        : EMouseWheelDirection.Down);
                    MouseWheel.Invoke(this, e);
                    handled = e.Handled;
                    break;
                }
                case EMouseMessages.WM_MOUSEMOVE:
                {
                    if (MouseMove == null)
                        break;
                    MouseHookStruct mhs = (MouseHookStruct) Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                    MouseMoveEventArgs e = new MouseMoveEventArgs(mhs.pt.x, mhs.pt.y);
                    MouseMove.Invoke(this,  e);
                    handled = e.Handled;
                    break;
                }
            }
            if(handled)
                return new IntPtr(1);
            return User32.CallNextHookEx(HookId, nCode, wParam, lParam);
        }
    }
}