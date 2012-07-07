namespace MonitorControl.Interop
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Interop;

    public partial class Win32
    {
        #region Nested type: User32

        public partial class User32
        {
            [DllImport("user32.dll", ExactSpelling = true)]
            public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);
            public delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);

            [DllImport("user32.dll", ExactSpelling = true)]
            public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, IntPtr lpTimerFunc);

            [DllImport("user32.dll", ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool KillTimer(IntPtr hWnd, IntPtr uIDEvent);


            public enum MonitorFromWindowFlags : int
            {
                MONITOR_DEFAULTTONULL = 0,
                MONITOR_DEFAULTTOPRIMARY = 1,
                MONITOR_DEFAULTTONEAREST = 2
            }

            [DllImport("user32"), SuppressUnmanagedCodeSecurity]
            public static extern bool PostThreadMessage(int threadId, uint msg, ushort wParam, uint lParam);

            [DllImport("user32.dll")]
            public static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            public static void PostMessageSafe(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
            {
                bool returnValue = Win32.User32.PostMessage(hWnd, msg, wParam, lParam);
                if (!returnValue)
                {
                    // An error occured
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }

            [SuppressUnmanagedCodeSecurity]
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool PeekMessage(out MSG message, IntPtr handle, uint filterMin, uint filterMax, uint flags);

            [DllImport("user32.dll")]
            public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

            [DllImport("user32.dll")]
            public static extern bool TranslateMessage([In] ref MSG lpMsg);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out]MONITORINFOEX info);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out]MONITORINFOEX info);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out]MONITORINFO info);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out]MONITORINFO info);


            [DllImport("User32.dll", ExactSpelling = true)]
            public static extern HandleRef MonitorFromPoint(POINTSTRUCT pt, int flags);

            [DllImport("User32")]
            public static extern HandleRef MonitorFromWindow(IntPtr handle, MonitorFromWindowFlags flags);

            // size of a device name string

            #region Nested type: MonitorInfo

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
            public class MONITORINFO
            {
                public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
                public RECT rcMonitor = new RECT();
                public RECT rcWork = new RECT();
                public int dwFlags = 0;
            }

            #endregion

            #region Nested type: MonitorInfoEx

            public const uint MONITORINFOF_PRIMARY = 1;

            private const int CCHDEVICENAME = 32;

            /// <summary>
            /// The MONITORINFOEX structure contains information about a display monitor.
            /// The GetMonitorInfo function stores information into a MONITORINFOEX structure or a MONITORINFO structure.
            /// The MONITORINFOEX structure is a superset of the MONITORINFO structure. The MONITORINFOEX structure adds a string member to contain a name 
            /// for the display monitor.
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
            public class MONITORINFOEX
            {
                public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
                public RECT rcMonitor = new RECT();
                public RECT rcWork = new RECT();
                public int dwFlags = 0;

                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
                public string szDevice;
                //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                //public char[] szDevice = new char[32];
            }

            #endregion

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WNDCLASSEX
            {
                public uint cbSize;
                public ClassStyles style;
                [MarshalAs(UnmanagedType.FunctionPtr)]
                public WndProc lpfnWndProc;
                public int cbClsExtra;
                public int cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                public string lpszMenuName;
                public string lpszClassName;
                public IntPtr hIconSm;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WNDCLASS
            {
                public uint style;
                public WndProc lpfnWndProc;
                public int cbClsExtra;
                public int cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string lpszMenuName;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string lpszClassName;
            }

            [DllImport("user32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "RegisterClassW", CharSet = CharSet.Unicode)]
            public static extern UInt16 RegisterClass([In] ref WNDCLASS lpWndClass);

            [DllImport("user32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "UnregisterClass", CharSet = CharSet.Unicode)]
            public static extern bool UnregisterClass([In] string lpClassName, [In] UInt16 hInstance = 0);

            [DllImport("user32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "RegisterClassExW", CharSet = CharSet.Unicode)]
            public static extern UInt16 RegisterClassEx([In] ref WNDCLASSEX lpwcx);

            [DllImport("user32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "CreateWindowExW", CharSet = CharSet.Unicode)]
            public static extern IntPtr CreateWindowEx(
               WindowStylesEx dwExStyle,
               [MarshalAs(UnmanagedType.LPWStr)]
               string lpClassName,
               [MarshalAs(UnmanagedType.LPWStr)]
               string lpWindowName,
               WindowStyles dwStyle,
               Int32 x,
               Int32 y,
               Int32 nWidth,
               Int32 nHeight,
               IntPtr hWndParent,
               IntPtr hMenu,
               IntPtr hInstance,
               IntPtr lpParam);

            public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", ExactSpelling = true, EntryPoint = "DefWindowProcW", CharSet = CharSet.Unicode)]
            public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
            public static extern bool DestroyWindow(IntPtr hWnd);

            public enum ShowWindowCommands : int
            {
                /// <summary>
                /// Hides the window and activates another window.
                /// </summary>
                SW_HIDE = 0,
                /// <summary>
                /// Activates and displays a window. If the window is minimized or 
                /// maximized, the system restores it to its original size and position.
                /// An application should specify this flag when displaying the window 
                /// for the first time.
                /// </summary>
                SW_SHOWNORMAL = 1,
                /// <summary>
                /// Activates the window and displays it as a minimized window.
                /// </summary>
                SW_SHOWMINIMIZED = 2,
                /// <summary>
                /// Maximizes the specified window.
                /// </summary>
                SW_MAXIMIZE = 3, // is this the right value?
                /// <summary>
                /// Activates the window and displays it as a maximized window.
                /// </summary>       
                SW_SHOWMAXIMIZED = 3,
                /// <summary>
                /// Displays a window in its most recent size and position. This value 
                /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
                /// the window is not activated.
                /// </summary>
                SW_SHOWNOACTIVATE = 4,
                /// <summary>
                /// Activates the window and displays it in its current size and position. 
                /// </summary>
                SW_SHOW = 5,
                /// <summary>
                /// Minimizes the specified window and activates the next top-level 
                /// window in the Z order.
                /// </summary>
                SW_MINIMIZE = 6,
                /// <summary>
                /// Displays the window as a minimized window. This value is similar to
                /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
                /// window is not activated.
                /// </summary>
                SW_SHOWMINNOACTIVE = 7,
                /// <summary>
                /// Displays the window in its current size and position. This value is 
                /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
                /// window is not activated.
                /// </summary>
                SW_SHOWNA = 8,
                /// <summary>
                /// Activates and displays the window. If the window is minimized or 
                /// maximized, the system restores it to its original size and position. 
                /// An application should specify this flag when restoring a minimized window.
                /// </summary>
                SW_RESTORE = 9,
                /// <summary>
                /// Sets the show state based on the SW_* value specified in the 
                /// STARTUPINFO structure passed to the CreateProcess function by the 
                /// program that started the application.
                /// </summary>
                SW_SHOWDEFAULT = 10,
                /// <summary>
                ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
                /// that owns the window is not responding. This flag should only be 
                /// used when minimizing windows from a different thread.
                /// </summary>
                SW_FORCEMINIMIZE = 11
            }

            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

            [DllImport("user32.dll")]
            public static extern bool UpdateWindow(IntPtr hWnd);


            /// <summary>
            /// The GetWindowInfo function retrieves information about the specified window.
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms633516%28VS.85%29.aspx"/>
            /// </summary>
            /// <param name="hwnd">The window handle.</param>
            /// <param name="pwi">The reference to WINDOWINFO structure.</param>
            /// <returns>true on success</returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool GetWindowInfo(HandleRef hwnd, ref WINDOWINFO pwi);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

            /// <summary>
            /// The MonitorFromWindow function retrieves a handle to the display monitor
            /// that has the largest area of intersection with the bounding rectangle
            /// of a specified window.
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/dd145064%28VS.85%29.aspx"/>
            /// </summary>
            /// <param name="hwnd">The window handle.</param>
            /// <param name="dwFlags">Determines the function's return value
            /// if the window does not intersect any display monitor.</param>
            /// <returns>
            /// Monitor HMONITOR handle on success or based on dwFlags for failure
            /// </returns>
            [DllImport("user32.dll")]
            public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

            [DllImport("user32.dll")]
            public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

            public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

            [Flags()]
            public enum DisplayDeviceStateFlags : int
            {
                /// <summary>The device is part of the desktop.</summary>
                AttachedToDesktop = 0x1,
                MultiDriver = 0x2,
                /// <summary>The device is part of the desktop.</summary>
                PrimaryDevice = 0x4,
                /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
                MirroringDriver = 0x8,
                /// <summary>The device is VGA compatible.</summary>
                VGACompatible = 0x16,
                /// <summary>The device is removable; it cannot be the primary display.</summary>
                Removable = 0x20,
                /// <summary>The device has more display modes than its output devices support.</summary>
                ModesPruned = 0x8000000,
                Remote = 0x4000000,
                Disconnect = 0x2000000
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct DISPLAY_DEVICE
            {
                [MarshalAs(UnmanagedType.U4)]
                public int cb;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                public string DeviceName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
                public string DeviceString;
                [MarshalAs(UnmanagedType.U4)]
                public DisplayDeviceStateFlags StateFlags;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
                public string DeviceID;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
                public string DeviceKey;
            }

            [DllImport("user32.dll")]
            public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        }

        #endregion
    }
}