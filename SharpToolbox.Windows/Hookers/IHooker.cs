using System;

namespace SharpToolbox.Windows.Hookers
{
    /// <summary>
    /// Hooker interface, used in KeyboardHooker and MouseHooker
    /// </summary>
    public interface IHooker
    {
        /// <summary>
        /// Returns true if this hooker is activated
        /// </summary>
        bool IsHooked { get; }
        
        /// <summary>
        /// Returns hook id
        /// </summary>
        IntPtr HookId { get; }

        /// <summary>
        /// Start hooking
        /// </summary>
        void Hook();

        /// <summary>
        /// Stop hooking
        /// </summary>
        void UnHook();
    }
}