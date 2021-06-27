using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpToolbox.Exceptions;
using SharpToolbox.Windows.Native;

namespace SharpToolbox.Windows.Hookers.Keyboard
{
    /// <summary>
    /// Provide keyboard hooks
    /// </summary>
    public class KeyboardHook : IHooker
    {
        private const int WM_KEYDOWN    = 0x100;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_KEYUP      = 0x101;
        private const int WM_SYSKEYUP   = 0x105;

        public KeyboardHook()
        {
        }

        ~KeyboardHook()
        {
            UnHook();
        }

        public bool IsHooked { get; private set; } = false;

        public IntPtr HookId { get; private set; } = IntPtr.Zero;

        /// <summary>
        /// Hook handle callback
        /// </summary>
        private HookHandlerDelegate HookHandler { get; set; }

        /// <summary>
        /// Called when the user press a key
        /// </summary>
        public event EventHandler<KeyboardKeyDownEventArgs> KeyDown;

        /// <summary>
        /// Called when the user release a key
        /// </summary>
        public event EventHandler<KeyboardKeyUpEventArgs> KeyUp;

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
                HookId = User32.SetWindowsHookEx(13, HookHandler, Kernel32.GetModuleHandle(module.ModuleName), 0);
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
        /// Handle keys
        /// </summary>
        /// <param name="nCode">nCode</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns>IntPtr result</returns>
        private IntPtr Hooker(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return User32.CallNextHookEx(HookId, nCode, wParam, lParam);
            int param = wParam.ToInt32();
            bool handled = false;
            switch (param)
            {
                case WM_KEYDOWN:
                case WM_SYSKEYDOWN:
                {
                    if (KeyDown == null)
                        break;
                    KeyboardKeyDownEventArgs keydown = new KeyboardKeyDownEventArgs((VirtualKeys) Marshal.ReadInt32(lParam));
                    KeyDown.Invoke(this, keydown);
                    handled = keydown.Handled;
                    break;
                }
                case WM_KEYUP:
                case WM_SYSKEYUP:
                {
                    if (KeyUp == null)
                        break;
                    KeyboardKeyUpEventArgs keyup = new KeyboardKeyUpEventArgs((VirtualKeys) Marshal.ReadInt32(lParam));
                    KeyUp.Invoke(this, keyup);
                    handled = keyup.Handled;
                    break;
                }
            }

            return handled ? new IntPtr(1) : User32.CallNextHookEx(HookId, nCode, wParam, lParam);
        }
    }
}