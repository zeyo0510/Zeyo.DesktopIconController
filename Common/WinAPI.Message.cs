using System;
using System.Runtime.InteropServices;

namespace Zeyo.DesktopIconController.Common
{
  partial class WinAPI
  {
    internal const int WM_COMMAND = 0x0111;

    internal static IntPtr CMD_TOGGLE_DESKTOP_ICON = new IntPtr(0x7402);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessage")]
    internal static extern IntPtr
    SEND_MESSAGE
    (
      IntPtr WND,
      uint MSG,
      IntPtr W_PARAM,
      IntPtr L_PARAM
    );
  }
}