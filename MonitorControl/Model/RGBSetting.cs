namespace MonitorControl.Model
{
    public class RGBSetting<T>
    {
        public RGBSetting(MinMaxVal<T> r, MinMaxVal<T> g, MinMaxVal<T> b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public MinMaxVal<T> Red { get; private set; }
        public MinMaxVal<T> Green { get; private set; }
        public MinMaxVal<T> Blue { get; private set; }
    }
}