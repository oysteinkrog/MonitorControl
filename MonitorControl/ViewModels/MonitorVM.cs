using System.Linq;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Expression.Interactivity.Core;
using MonitorControl.Model;

namespace MonitorControl.ViewModels
{
    public class MonitorVM : ViewModelBase
    {
        private readonly Monitor _monitor;
        private readonly object _syncRoot = new object();
        private HighLevelMonitor _hlaMonitor;
        private ICommand _restoreColorDefaultsCommand;
        private ICommand _restoreAllDefaultsCommand;


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
            set
            {
                foreach (var pm in _hlaMonitor.PhysicalMonitors)
                {
                    pm.Brightness.Val = value;
                }
            }
        }

        [DependsOn("Brightness")]
        public double BrightnessPercent
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Brightness.MinMaxValToPercent(); }
            set
            {
                foreach (var pm in _hlaMonitor.PhysicalMonitors)
                {
                    pm.Brightness.Val = pm.Brightness.ValFromPercent(value);
                }
            }
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
            set
            {
                foreach (var pm in _hlaMonitor.PhysicalMonitors)
                {
                    pm.Contrast.Val = value;
                }
            }
        }

        public MinMaxVal<uint> GainRed
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Gain.Red; }
        }
        public MinMaxVal<uint> GainBlue
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Gain.Blue; }
        }
        public MinMaxVal<uint> GainGreen
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Gain.Green; }
        }     
        public MinMaxVal<uint> DriveRed
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Drive.Red; }
        }
        public MinMaxVal<uint> DriveBlue
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Drive.Blue; }
        }
        public MinMaxVal<uint> DriveGreen
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Drive.Green; }
        }


    [DependsOn("Contrast")]
        public double ContrastPercent
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Contrast.MinMaxValToPercent(); }
            set
            {
                foreach (var pm in _hlaMonitor.PhysicalMonitors)
                {
                    pm.Contrast.Val = pm.Contrast.ValFromPercent(value);
                }
            }
        }

        public string ColorTemperature
        {
            get { return _hlaMonitor.PhysicalMonitors.First().Description; }
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

        public ICommand RestoreColorDefaultsCommand
        {
            get
            {
                return _restoreColorDefaultsCommand ?? (_restoreColorDefaultsCommand = new ActionCommand(() =>
                    {
                        foreach (var pm in _hlaMonitor.PhysicalMonitors)
                        {
                            pm.RestoreColorDefaults();
                        }
                    }));
            }
        }

        public ICommand RestoreAllDefaultsCommand
        {
            get
            {
                return _restoreAllDefaultsCommand ?? (_restoreAllDefaultsCommand = new ActionCommand(() =>
                    {
                        foreach (var pm in _hlaMonitor.PhysicalMonitors)
                        {
                            pm.RestoreAllDefaults();
                        }
                    }));
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