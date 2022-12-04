using System;
using System.Runtime.InteropServices;

namespace Zeyo.DesktopIconController.Common
{
  partial class WinAPI
  {
    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "FindWindowEx")]
    internal static extern IntPtr
    FIND_WINDOW_EX
    (
      IntPtr WND_PARENT,
      IntPtr WND_CHILD_AFTER,
      string CLASS,
      string WINDOW
    );

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetDesktopWindow")]
    internal static extern IntPtr
    GET_DESKTOP_WINDOW();

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "IsWindowVisible")]
    internal static extern bool
    IS_WINDOW_VISIBLE
    (
      IntPtr WND
    );
  }
}