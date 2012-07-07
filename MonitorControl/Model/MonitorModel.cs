using System;

namespace MonitorControl.Model
{
    using MonitorControl.Interop;

    public class MonitorModel
    {
        private readonly MultiMonController _controller;

        public MonitorModel()
        {
            _controller = new MultiMonController();
        }

        public uint BrightnessMax { get; set; }
        public uint BrightnessMin { get; set; }
        public uint BrightnessVal { get; set; }
    }
}