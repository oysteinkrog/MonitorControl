using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MonitorControl.Interop;

namespace MonitorControl.Model
{
    public class HighLevelMonitor : IDisposable
    {
        private readonly Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR[] _pPhysicalMonitorArray;
        private readonly uint _pdwMonitorCapabilities;
        private readonly uint _pdwSupportedColorTemperatures;
        private readonly List<PhysicalMonitor> _physicalMonitorsList;
        private uint _pdwNumberOfPhysicalMonitors;

        public HighLevelMonitor(IntPtr hMonitor)
        {
            Win32.Dxva2.PhysicalMonitorEnumerationApi.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out _pdwNumberOfPhysicalMonitors);
            if (_pdwNumberOfPhysicalMonitors <= 0)
                return;

            _pPhysicalMonitorArray = new Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR[_pdwNumberOfPhysicalMonitors];

            Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorCapabilities(hMonitor, out _pdwMonitorCapabilities, out _pdwSupportedColorTemperatures);

            Win32.Dxva2.PhysicalMonitorEnumerationApi.GetPhysicalMonitorsFromHMONITOR(hMonitor, _pdwNumberOfPhysicalMonitors, _pPhysicalMonitorArray);

            _physicalMonitorsList = _pPhysicalMonitorArray.Select(a => new PhysicalMonitor(a)).ToList();
            PhysicalMonitors = new ReadOnlyCollection<PhysicalMonitor>(_physicalMonitorsList);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // get rid of managed resources
                _physicalMonitorsList.Clear();
                _pdwNumberOfPhysicalMonitors = 0;
            }
            // get rid of unmanaged resources
            Win32.Dxva2.PhysicalMonitorEnumerationApi.DestroyPhysicalMonitors(_pdwNumberOfPhysicalMonitors, _pPhysicalMonitorArray);
        }

        // only if you use unmanaged resources directly
        ~HighLevelMonitor()
        {
            Dispose(false);
        }

        public ReadOnlyCollection<PhysicalMonitor> PhysicalMonitors { get; private set; }
    }
}