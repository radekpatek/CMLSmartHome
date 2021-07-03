using System.Drawing;
using System.Linq;

namespace CMLSmartHomeController.Chart
{
    /// <summary>
    /// Osa Y
    /// </summary>
    public class CMLChartYAsix
    {
        public string Label { get; set; }
        public bool Fill { get; set; }
        public Color BackgroundColor { get; set; }
        public Color BorderColor { get; set; }
        public double[] Values { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public bool ZoroValueMark { get; set; }

        public int Id { get; set; }
        public PresentationType Type { get; set; }
        public Location Location { get; set; }

        public CMLChartYAsix(double[] values)
        {
            Fill = true;
            BackgroundColor = Color.Black;
            BorderColor = Color.Black;
            Type = PresentationType.Line;
            Location = Location.Left;
            Values = values;
            if (values != null)
            {
                MinValue = values.Min();
                MaxValue = values.Max();
            }
        }

        public CMLChartYAsix(string label, bool fill, Color backgroundColor, Color borderColor, double[] values, int id, PresentationType type, Location location, bool zoroValueMark)
        {
            Label = label;
            Fill = fill;
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
            Id = id;
            Type = type;
            Location = location;
            ZoroValueMark = zoroValueMark;
            Values = values;
            if (values != null)
            {
                MinValue = values.Min();
                MaxValue = values.Max();
            }
        }
    }

    public enum PresentationType
    {
        Bar,
        Line
    }

    public enum Location
    {
        Left,
        Right
    }
}



