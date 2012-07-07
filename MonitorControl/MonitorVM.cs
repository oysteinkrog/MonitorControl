using System.Linq;
using Elysium.Theme.ViewModels;
using MonitorControl.Model;
using phyApi = MonitorControl.Interop.Win32.Dxva2.PhysicalMonitorEnumerationApi;
using hlaApi = MonitorControl.Interop.Win32.Dxva2.HighlevelMonitorConfigurationApi;

namespace MonitorControl
{
    public class MonitorVM : ViewModelBase
    {
        private readonly Monitor _monitor;
        private readonly object _syncRoot = new object();
        private HighLevelMonitor _hlaMonitor;


        public MonitorVM(Monitor monitor)
        {
            _monitor = monitor;
            _hlaMonitor = new HighLevelMonitor(_monitor.HMonitor);
        }

        public uint BrightnessMax
        {
            get { return _hlaMonitor.PhysicalMonitors.First().BrightnessMax; }
        }

        public uint BrightnessMin
        {
            get { return _hlaMonitor.PhysicalMonitors.First().BrightnessMin; }
        }

        public uint Brightness
        {
            get { return _hlaMonitor.PhysicalMonitors.First().BrightnessValue; }
            set { _hlaMonitor.PhysicalMonitors.First().BrightnessValue = value; }
        }

        public string Name
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Description; }
        }

        public bool CanSetBrightness
        {
            get { return true; }
        }

        protected override void Dispose(bool disposing)
        {
            _hlaMonitor.Dispose();
            _hlaMonitor = null;
            base.Dispose(true);
        }
    }
}