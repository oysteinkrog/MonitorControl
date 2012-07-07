using System;
using System.Drawing;
using System.Linq;
using MonitorControl.Interop;

namespace MonitorControl.Model
{
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

            DeviceName = _mi.szDevice;

            GetMonitorName(number);
        }

        private void GetMonitorName(int number)
        {
            if (OS.IsVistaOrLater)
            {
                using (var hlMonitor = new HighLevelMonitor(HMonitor))
                {
                    if (hlMonitor.PhysicalMonitors.Count > 0)
                    {
                        Name = hlMonitor.PhysicalMonitors.First().Description;
                    }
                    else
                    {
                        Name = "Monitor " + number;
                    }
                }
            }
            // fallback
            Name = "Monitor " + number;
        }

        public IntPtr HMonitor { get; set; }
        public int Number { get; private set; }

        public bool IsPrimary { get; private set; }

        public Rectangle WorkArea { get; private set; }

        public Rectangle FullArea { get; private set; }

        public string DeviceName { get; private set; }
        public string Name { get; private set; }


        public override string ToString()
        {
            return string.Format("Monitor {0} ({1})", Number, Name);
        }
    }
}