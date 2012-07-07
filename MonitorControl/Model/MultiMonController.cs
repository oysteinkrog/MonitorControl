// Copyright © 2007 by Initial Force AS.  All rights reserved.
//
// Author: Øystein E. Krog

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MonitorControl.Interop;
using MonitorControl.Utilities;

namespace MonitorControl.Model
{
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
    }
}