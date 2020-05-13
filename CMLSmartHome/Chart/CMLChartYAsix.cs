﻿using System;
using System.Drawing;
using System.Linq;

namespace GraphToBitmap.Chart
{
    public class CMLYAsix
    {
        public string Label { get; set; }
        public bool Fill { get; set; }
        public Color BackgroundColor { get; set; }
        public Color BorderColor { get; set; }
        public double[] Values { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public int Id { get; set; }
        public PresentationType Type { get; set; }
        public Location Location { get; set; }

        public CMLYAsix(double[] values)
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

        public CMLYAsix(string label, bool fill, Color backgroundColor, Color borderColor, double[] values, int id, PresentationType type, Location location)
        {
            Label = label;
            Fill = fill;
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
            Id = id;
            Type = type;
            Location = location;
            Values = values;
            if (values != null)
            {
                MinValue = values.Min();
                MaxValue = values.Max();
            }
        }
    }
}

namespace GraphToBitmap
{
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
