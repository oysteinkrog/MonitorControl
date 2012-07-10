using System.Linq;
using System.Windows.Markup;
using MonitorControl.Model;

namespace MonitorControl.ViewModels
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
            get { return _hlaMonitor.PhysicalMonitors.First().Brightness.Max; }
        }

        public uint BrightnessMin
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Brightness.Min; }
        }

        public uint Brightness
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Brightness.Val; }
            set { _hlaMonitor.PhysicalMonitors.First().Brightness = new MinMaxVal<uint> {Val = value}; }
        }


        [DependsOn("Brightness")]
        public double BrightnessPercent
        {
            get { return _hlaMonitor.PhysicalMonitors.First().BrightnessValuePercent; }
            set { _hlaMonitor.PhysicalMonitors.First().BrightnessValuePercent = value; }
        }


        public uint ContrastMax
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Contrast.Max; }
        }

        public uint ContrastMin
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Contrast.Min; }
        }

        public uint Contrast
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Contrast.Val; }
            set { _hlaMonitor.PhysicalMonitors.First().Contrast = new MinMaxVal<uint> {Val = value}; }
        }

        [DependsOn("Contrast")]
        public double ContrastPercent
        {
            get { return _hlaMonitor.PhysicalMonitors.First().ContrastValuePercent; }
            set { _hlaMonitor.PhysicalMonitors.First().ContrastValuePercent = value; }
        }

        public string Name
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Description; }
        }

        public bool CanSetBrightness
        {
            get
            {
                // we can't trust the "capabilities", use simple check instead   
                return (BrightnessMax - BrightnessMin) > 0;
            }
        }
        public bool CanSetContrast
        {
            get
            {
                // we can't trust the "capabilities", use simple check instead   
                return (ContrastMax - ContrastMin) > 0;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _hlaMonitor.Dispose();
            _hlaMonitor = null;
            base.Dispose(true);
        }
    }
}