// Copyright © 2007 by Initial Force AS.  All rights reserved.
//
// Author: Øystein E. Krog

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace MonitorControl
{
    public class MultiMonWindow
    {
        private readonly IntPtr _hwnd;
        private readonly Window _window;

        public MultiMonWindow(Window window)
        {
            _window = window;
            _hwnd = (new WindowInteropHelper(_window)).Handle;
        }

        //        public Rectangle Position
        //        {
        //            get
        //            {
        //                return new Rectangle(_window.Left, _window.Top, _window.Width, _window.Height);
        //                var hMonitor = Win32.User32.MonitorFromWindow(_hwnd,Win32.User32.MonitorFromWindowFlags.MONITOR_DEFAULTTONEAREST);
        //            }
        //        }
        //
        //        public Monitor Monitor
        //        {
        //            get
        //            {
        //                var hMonitor = Win32.User32.MonitorFromWindow(_hwnd, Win32.User32.MonitorFromWindowFlags.MONITOR_DEFAULTTONEAREST);
        //            }
        //        }

        public override int GetHashCode()
        {
            return _window.GetHashCode();
        }

        public void MoveToMonitor(Monitor monitor)
        {
            //            if (_window.IsLoaded)
            //            {
            //                _window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Monitor>(MoveToMonitor), monitor);
            //                return;
            //            }
            _window.Left = monitor.WorkArea.Left;
            _window.Top = monitor.WorkArea.Top;
            _window.Width = monitor.WorkArea.Width;
            _window.Height = monitor.WorkArea.Height;

            //TODO: move monitor by win32 functions instead?
            //http://msdn.microsoft.com/en-us/library/dd162826(v=vs.85).aspx
        }
    }

    public class Monitor
    {
        private readonly Win32.User32.MONITORINFOEX _mi;

        public Monitor(IntPtr hMonitor, int number)
        {
            _mi = new Win32.User32.MONITORINFOEX();
            bool success = Win32.User32.GetMonitorInfo(hMonitor, _mi);
            if (!success)
                throw new Exception("failed to get monitor info for monitor with hMonitor " + hMonitor);

            HMonitor = hMonitor;
            Number = number;
            IsPrimary = (_mi.dwFlags & Win32.User32.MONITORINFOF_PRIMARY) == Win32.User32.MONITORINFOF_PRIMARY;
            WorkArea = new Rectangle(_mi.rcWork.left, _mi.rcWork.top, _mi.rcWork.Width, _mi.rcWork.Height);
            FullArea = new Rectangle(_mi.rcMonitor.left, _mi.rcMonitor.top, _mi.rcMonitor.Width, _mi.rcMonitor.Height);
            Name = _mi.szDevice;

            // We use WMI to get the true monitor name..
            // this only works for one monitor..sigh
            //            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DesktopMonitor"))
            //            {
            //                foreach (ManagementObject currentObj in searcher.Get())
            //                {
            //                    String name = currentObj["Name"].ToString();
            //                    String device_id = currentObj["DeviceID"].ToString();
            //                    // ...
            //                }
            //            }
        }

        public IntPtr HMonitor { get; set; }
        public int Number { get; private set; }

        public bool IsPrimary { get; private set; }

        public Rectangle WorkArea { get; private set; }

        public Rectangle FullArea { get; private set; }

        public string Name { get; private set; }


        public override string ToString()
        {
            return string.Format("Monitor {0} ({1})", Number, Name);
        }
    }

    public class MultiMonController
    {
        private readonly ObservableCollection<Monitor> _monitors = new ObservableCollection<Monitor>();
        private readonly ReadOnlyObservableCollection<Monitor> _readonlyMonitors;
        private readonly Dictionary<Window, MultiMonWindow> _windows = new Dictionary<Window, MultiMonWindow>();

        public MultiMonController()
        {
            _readonlyMonitors = new ReadOnlyObservableCollection<Monitor>(_monitors);

            UpdateMonitors();
        }

        public ReadOnlyObservableCollection<Monitor> Monitors
        {
            get { return _readonlyMonitors; }
        }

        public Monitor PrimaryMonitor
        {
            get { return _monitors.FirstOrDefault(a => a.IsPrimary); }
        }

        /// <summary>
        /// Returns the number of Displays using the Win32 functions
        /// </summary>
        /// <returns>collection of Display Info</returns>
        private IEnumerable<Monitor> EnumerateMonitors()
        {
            var col = new List<Monitor>();
            int i = 0;
            Win32.User32.EnumDisplayMonitors(IntPtr.Zero,
                                             IntPtr.Zero,
                                             delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Win32.RECT lprcMonitor, IntPtr dwData)
                                             {
                                                 var di = new Monitor(hMonitor, i);
                                                 col.Add(di);
                                                 i++;
                                                 return true;
                                             },
                                             IntPtr.Zero);

            //            var d = new Win32.User32.DISPLAY_DEVICE();
            //            d.cb = Marshal.SizeOf(d);
            //            try
            //            {
            //                for (uint id = 0; Win32.User32.EnumDisplayDevices(null, id, ref d, 0); id++)
            //                {
            //                    Console.WriteLine(
            //                        String.Format("{0}, {1}, {2}, {3}, {4}, {5}",
            //                                 id,
            //                                 d.DeviceName,
            //                                 d.DeviceString,
            //                                 d.StateFlags,
            //                                 d.DeviceID,
            //                                 d.DeviceKey
            //                                 )
            //                                  );
            //                    d.cb = Marshal.SizeOf(d);
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(String.Format("{0}", ex.ToString()));
            //            }

            return col;
        }

        private void UpdateMonitors()
        {
            _monitors.Clear();
            _monitors.AddAll(EnumerateMonitors());
        }

        public void AddWindow(Window window)
        {
            var mm = new MultiMonWindow(window);

            if (!_windows.ContainsKey(window))
                _windows.Add(window, mm);
        }

        public void MoveWindowToMonitor(Window window, Monitor monitor)
        {
            if (window != null)
            {
                if (_monitors.Contains(monitor) && _windows.ContainsKey(window))
                {
                    try
                    {
                        MultiMonWindow mmwindow = _windows[window];
                        mmwindow.MoveToMonitor(monitor);
//                        Log.Present.Info("MultiMonController.MoveWindowToMonitor: Successfully moved window " + window + " to monitor " + monitor);
                    }
                    catch
                    {
//                        Log.Present.Error("MultiMonController.MoveWindowToMonitor: Failed to move window " + window + " to monitor " + monitor);
                    }
                }
            }
        }
    }
}