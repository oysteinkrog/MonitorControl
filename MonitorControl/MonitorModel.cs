using System;

namespace MonitorControl
{
    public class MonitorModel
    {
        private readonly MultiMonController _controller;

        public MonitorModel()
        {
            _controller = new MultiMonController();


            Test();
        }

        public uint BrightnessMax { get; set; }
        public uint BrightnessMin { get; set; }
        public uint BrightnessVal { get; set; }

        private void Test()
        {
            foreach (var monitor in _controller.Monitors)
            {
                uint pdwNumberOfPhysicalMonitors;
                Win32.Dxva2.PhysicalMonitorEnumerationApi.GetNumberOfPhysicalMonitorsFromHMONITOR(monitor.HMonitor, out pdwNumberOfPhysicalMonitors);

                Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR[] pPhysicalMonitorArray = new Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR[pdwNumberOfPhysicalMonitors];

                Win32.Dxva2.PhysicalMonitorEnumerationApi.GetPhysicalMonitorsFromHMONITOR(monitor.HMonitor, pdwNumberOfPhysicalMonitors,
                                                            pPhysicalMonitorArray);
                try
                {
                    uint pdwMonitorCapabilities;
                    uint pdwSupportedColorTemperatures;
                    Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorCapabilities(monitor.HMonitor, out pdwMonitorCapabilities, out pdwSupportedColorTemperatures);

                    foreach (Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR physicalMonitor in pPhysicalMonitorArray)
                    {
                        uint min;
                        uint val;
                        uint max;
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorBrightness(physicalMonitor.hPhysicalMonitor,
                                                         out min,
                                                         out val,
                                                         out max);

                        double percent = (max - min)*0.8;
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorBrightness(physicalMonitor.hPhysicalMonitor, (uint)percent);
                    }
                }
                finally
                {
                    Win32.Dxva2.PhysicalMonitorEnumerationApi.DestroyPhysicalMonitors(pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
                }
            }
        }
    }
}