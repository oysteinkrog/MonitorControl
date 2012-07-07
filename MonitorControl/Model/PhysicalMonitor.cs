using MonitorControl.Interop;

namespace MonitorControl.Model
{
    public class PhysicalMonitor
    {
        private readonly Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR _physicalMonitor;
        private uint? _brightnessMax;
        private uint? _brightnessMin;

        public PhysicalMonitor(Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR physicalMonitor)
        {
            _physicalMonitor = physicalMonitor;
        }

        public string Description
        {
            get { return _physicalMonitor.szPhysicalMonitorDescription; }
        }

        public uint BrightnessMax
        {
            get
            {
                if (_brightnessMax.HasValue)
                    return _brightnessMax.Value;
                uint min;
                uint val;
                uint max;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, out min, out val, out max);
                _brightnessMin = min;
                _brightnessMax = max;
                return _brightnessMax.Value;
            }
        }

        public uint BrightnessMin
        {
            get
            {
                if (_brightnessMin.HasValue)
                    return _brightnessMin.Value;
                uint min;
                uint val;
                uint max;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, out min, out val, out max);
                _brightnessMin = min;
                _brightnessMax = max;
                return _brightnessMin.Value;
            }
        }

        public uint BrightnessValue
        {
            get
            {
                uint min, max, val;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, out min, out val, out max);
                return val;
            }
            set
            {
                Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, value);
            }
        }

        public double BrightnessValuePercent
        {
            get
            {
                uint min;
                uint val;
                uint max;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, out min, out val, out max);

                return val / (double)(max - min);
            }
            set
            {
                double percent = (BrightnessMax - BrightnessMin) * value;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, (uint)percent);
            }
        }
    }
}