using System;
using MonitorControl.Interop;

namespace MonitorControl.Model
{
    public class PhysicalMonitor
    {
        private readonly Win32.Dxva2.HighlevelMonitorConfigurationApi.MonitorCapabilities _pdwMonitorCapabilities;

        private readonly Win32.Dxva2.HighlevelMonitorConfigurationApi.MonitorSupportedColorTemperatures
            _pdwSupportedColorTemperatures;

        private readonly Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR _physicalMonitor;

        public PhysicalMonitor(Win32.Dxva2.PhysicalMonitorEnumerationApi.PHYSICAL_MONITOR physicalMonitor)
        {
            _physicalMonitor = physicalMonitor;

            Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorCapabilities(_physicalMonitor.hPhysicalMonitor,
                                                                                out _pdwMonitorCapabilities,
                                                                                out _pdwSupportedColorTemperatures);

            Brightness = new MinMaxVal<uint>(
                () =>
                    {
                        uint min, val, max;
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorBrightness(
                            _physicalMonitor.hPhysicalMonitor, out min, out val, out max);
                        return Tuple.Create(min, val, max);
                    },
                (min, val, max) =>
                Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorBrightness(_physicalMonitor.hPhysicalMonitor, val));

            Contrast = new MinMaxVal<uint>(
                () =>
                    {
                        uint min, val, max;
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorContrast(
                            _physicalMonitor.hPhysicalMonitor, out min, out val, out max);
                        return Tuple.Create(min, val, max);
                    },
                (min, val, max) =>
                Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorContrast(_physicalMonitor.hPhysicalMonitor, val));

            Drive = new RGBSetting<uint>(
                new MinMaxVal<uint>(
                    () =>
                        {
                            uint min, val, max;
                            Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorRedGreenOrBlueDrive(
                                _physicalMonitor.hPhysicalMonitor,
                                Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_DRIVE_TYPE.MC_RED_DRIVE, out min,
                                out val, out max);
                            return Tuple.Create(min, val, max);
                        },
                    (min, val, max) => Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorRedGreenOrBlueDrive(
                        _physicalMonitor.hPhysicalMonitor,
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_DRIVE_TYPE.MC_RED_DRIVE,
                        val))
                ,
                new MinMaxVal<uint>(
                    () =>
                        {
                            uint min, val, max;
                            Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorRedGreenOrBlueDrive(
                                _physicalMonitor.hPhysicalMonitor,
                                Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_DRIVE_TYPE.MC_GREEN_DRIVE, out min,
                                out val, out max);
                            return Tuple.Create(min, val, max);
                        },
                    (min, val, max) => Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorRedGreenOrBlueDrive(
                        _physicalMonitor.hPhysicalMonitor,
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_DRIVE_TYPE.MC_GREEN_DRIVE,
                        val))
                , new MinMaxVal<uint>(
                      () =>
                          {
                              uint min, val, max;
                              Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorRedGreenOrBlueDrive(
                                  _physicalMonitor.hPhysicalMonitor,
                                  Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_DRIVE_TYPE.MC_BLUE_DRIVE, out min,
                                  out val, out max);
                              return Tuple.Create(min, val, max);
                          },
                      (min, val, max) => Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorRedGreenOrBlueDrive(
                          _physicalMonitor.hPhysicalMonitor,
                          Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_DRIVE_TYPE.MC_BLUE_DRIVE,
                          val))
                );            
            
            Gain = new RGBSetting<uint>(
                new MinMaxVal<uint>(
                    () =>
                        {
                            uint min, val, max;
                            Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorRedGreenOrBlueGain(
                                _physicalMonitor.hPhysicalMonitor,
                                Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_GAIN_TYPE.MC_RED_GAIN, out min,
                                out val, out max);
                            return Tuple.Create(min, val, max);
                        },
                    (min, val, max) => Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorRedGreenOrBlueGain(
                        _physicalMonitor.hPhysicalMonitor,
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_GAIN_TYPE.MC_RED_GAIN,
                        val))
                ,
                new MinMaxVal<uint>(
                    () =>
                        {
                            uint min, val, max;
                            Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorRedGreenOrBlueGain(
                                _physicalMonitor.hPhysicalMonitor,
                                Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_GAIN_TYPE.MC_GREEN_GAIN, out min,
                                out val, out max);
                            return Tuple.Create(min, val, max);
                        },
                    (min, val, max) => Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorRedGreenOrBlueGain(
                        _physicalMonitor.hPhysicalMonitor,
                        Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_GAIN_TYPE.MC_GREEN_GAIN,
                        val))
                , new MinMaxVal<uint>(
                      () =>
                          {
                              uint min, val, max;
                              Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorRedGreenOrBlueGain(
                                  _physicalMonitor.hPhysicalMonitor,
                                  Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_GAIN_TYPE.MC_BLUE_GAIN, out min,
                                  out val, out max);
                              return Tuple.Create(min, val, max);
                          },
                      (min, val, max) => Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorRedGreenOrBlueGain(
                          _physicalMonitor.hPhysicalMonitor,
                          Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_GAIN_TYPE.MC_BLUE_GAIN,
                          val))
                );
        }

        public string Description
        {
            get { return _physicalMonitor.szPhysicalMonitorDescription; }
        }

        public MinMaxVal<uint> Brightness { get; private set; }

        public MinMaxVal<uint> Contrast { get; private set; }


        public Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_COLOR_TEMPERATURE ColorTemperature
        {
            get
            {
                Win32.Dxva2.HighlevelMonitorConfigurationApi.MC_COLOR_TEMPERATURE colorTemp;
                Win32.Dxva2.HighlevelMonitorConfigurationApi.GetMonitorColorTemperature(
                    _physicalMonitor.hPhysicalMonitor, out colorTemp);
                return colorTemp;
            }
            set
            {
                Win32.Dxva2.HighlevelMonitorConfigurationApi.SetMonitorColorTemperature(
                    _physicalMonitor.hPhysicalMonitor, value);
            }
        }

        public RGBSetting<uint> Drive { get; private set; }

        public RGBSetting<uint> Gain { get; private set; }

        public void RestoreColorDefaults()
        {
            Win32.Dxva2.HighlevelMonitorConfigurationApi.RestoreMonitorFactoryColorDefaults( _physicalMonitor.hPhysicalMonitor);
        }        
        
        public void RestoreAllDefaults()
        {
            Win32.Dxva2.HighlevelMonitorConfigurationApi.RestoreMonitorFactoryDefaults( _physicalMonitor.hPhysicalMonitor);
        }
    }
}