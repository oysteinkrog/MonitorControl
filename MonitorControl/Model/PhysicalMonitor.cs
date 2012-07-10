using MonitorControl.Interop;

namespace MonitorControl.Model
{
    public struct MinMaxVal<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }
        public T Val { get; set; }
    }

    public class PhysicalMonitor
    {
        private readonly Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR _physicalMonitor;

        public PhysicalMonitor(Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR physicalMonitor)
        {
            _physicalMonitor = physicalMonitor;
        }

        public string Description
        {
            get { return _physicalMonitor.szPhysicalMonitorDescription; }
        }

        public MinMaxVal<uint> Brightness
        {
            get
            {
                uint min;
                uint val;
                uint max;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, out min, out val, out max);
                return new MinMaxVal<uint>{Min = min, Max = max, Val = val};
            }
            set
            {
                Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, value.Val);
            }
        }

        public MinMaxVal<uint> Contrast
        {
            get
            {
                uint min;
                uint val;
                uint max;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorContrast(_physicalMonitor.hPhysicalMonitor, out min, out val, out max);
                return new MinMaxVal<uint>{Min = min, Max = max, Val = val};
            }
            set
            {
                Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorContrast(_physicalMonitor.hPhysicalMonitor, value.Val);
            }
        }

        public double BrightnessValuePercent
        {
            get
            {
                var v = Brightness;
                return v.Val / (double)(v.Max - v.Min);
            }
            set
            {
                var v = Brightness;
                double val = (v.Max - v.Min) * value;
                Brightness = new MinMaxVal<uint>() {Val = (uint) val};
            }
        }

        public double ContrastValuePercent
        {
            get
            {
                var v = Contrast;
                return v.Val / (double)(v.Max - v.Min);
            }
            set
            {
                var v = Contrast;
                double val = (v.Max - v.Min) * value;
                Contrast = new MinMaxVal<uint>() { Val = (uint)val };
            }
        }
    }
}