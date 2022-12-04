using System;
using System.Collections.Generic;
using Zeyo.DesktopIconController.Common;

namespace Zeyo.DesktopIconController.Core
{
  public static class DesktopIcon
  {
    public static bool IsVisible
    {
      get
      {
        IntPtr hWnd = IntPtr.Zero;
        /************************************************/
        if (hWnd == IntPtr.Zero)
        {
          Stack<KeyValuePair<string, string>> list = new Stack<KeyValuePair<string, string>>();
          {
            list.Push(new KeyValuePair<string, string>("SysListView32"   .Trim(), "FolderView     ".Trim()));
            list.Push(new KeyValuePair<string, string>("SHELLDLL_DefView".Trim(), "               ".Trim()));
            list.Push(new KeyValuePair<string, string>("Progman"         .Trim(), "Program Manager".Trim()));
          }
          hWnd = DesktopIcon.FindWindowByLevel(WinAPI.GET_DESKTOP_WINDOW(), list);
        }
        if (hWnd == IntPtr.Zero)
        {
          Stack<KeyValuePair<string, string>> list = new Stack<KeyValuePair<string, string>>();
          {
            list.Push(new KeyValuePair<string, string>("SysListView32"   .Trim(), "FolderView".Trim()));
            list.Push(new KeyValuePair<string, string>("SHELLDLL_DefView".Trim(), "          ".Trim()));
            list.Push(new KeyValuePair<string, string>("WorkerW         ".Trim(), "          ".Trim()));
          }
          hWnd = DesktopIcon.FindWindowByLevel(WinAPI.GET_DESKTOP_WINDOW(), list);
        }
        /************************************************/
        return  1==1
        &&      hWnd != IntPtr.Zero
        &&      WinAPI.IS_WINDOW_VISIBLE(hWnd)
        ;
      }
    }

    public static bool Toggle()
    {
      IntPtr hWnd = IntPtr.Zero;
      /************************************************/
      if (hWnd == IntPtr.Zero)
      {
        Stack<KeyValuePair<string, string>> list = new Stack<KeyValuePair<string, string>>();
        {
          list.Push(new KeyValuePair<string, string>("SHELLDLL_DefView".Trim(), "               ".Trim()));
          list.Push(new KeyValuePair<string, string>("Progman"         .Trim(), "Program Manager".Trim()));
        }
        hWnd = DesktopIcon.FindWindowByLevel(WinAPI.GET_DESKTOP_WINDOW(), list);
      }
      if (hWnd == IntPtr.Zero)
      {
        Stack<KeyValuePair<string, string>> list = new Stack<KeyValuePair<string, string>>();
        {
          list.Push(new KeyValuePair<string, string>("SHELLDLL_DefView".Trim(), "".Trim()));
          list.Push(new KeyValuePair<string, string>("WorkerW         ".Trim(), "".Trim()));
        }
        hWnd = DesktopIcon.FindWindowByLevel(WinAPI.GET_DESKTOP_WINDOW(), list);
      }
      if (hWnd == IntPtr.Zero)
      {
        return false;
      }
      /************************************************/
      bool oldVisible = DesktopIcon.IsVisible;
      WinAPI.SEND_MESSAGE(hWnd, WinAPI.WM_COMMAND, WinAPI.CMD_TOGGLE_DESKTOP_ICON, IntPtr.Zero);
      bool newVisible = DesktopIcon.IsVisible;
      return oldVisible != newVisible;
    }

    public static bool Show()
    {
      return  1==1
      &&   !  DesktopIcon.IsVisible
      &&      DesktopIcon.Toggle()
      ;
    }

    public static bool Hide()
    {
      return  1==1
      &&      DesktopIcon.IsVisible
      &&      DesktopIcon.Toggle()
      ;
    }

    private static IntPtr FindWindowByLevel(IntPtr parent, Stack<KeyValuePair<string, string>> childLevel)
    {
      IntPtr retValue = IntPtr.Zero;
      /************************************************/
      KeyValuePair<string, string> pair = childLevel.Pop();
      /************************************************/
      IntPtr child = IntPtr.Zero;
      /************************************************/
      do
      {
        child = WinAPI.FIND_WINDOW_EX(parent, child, pair.Key, pair.Value);
        if (child == IntPtr.Zero)
        {
          retValue = child;
        }
        if (child != IntPtr.Zero && childLevel.Count == 0)
        {
          retValue = child;
        }
        if (child != IntPtr.Zero && childLevel.Count >= 1)
        {
          retValue = DesktopIcon.FindWindowByLevel(child, new Stack<KeyValuePair<string, string>>(
                                                          new Stack<KeyValuePair<string, string>>(childLevel)));
        }
      } while (retValue == IntPtr.Zero && child != IntPtr.Zero);
      /************************************************/
      return retValue;
    }
  }
}