using SkiaSharp;
using System;
using System.Collections.Generic;

namespace CMLSmartHomeController.Chart
{
    /// <summary>
    /// Canvas grafu
    /// </summary>
    public class CMLChartCanvas
    {
        public int Width { get; }
        public int Height { get; }
        public string Name { get; set; }

        public SKPaint AsixXPaint { get; set; }
        public SKPaint AsixYPaint { get; set; }
        public SKPaint TitlePaint { get; set; }
        public SKPaint BarPaint { get; set; }
        public SKPaint LinePaint { get; set; }

        private SKCanvas _canvas;
        private SKBitmap _image;

        private const int AsixYTextSize = 10;
        private const int AsixYTextSizeBig = 16;
        private const int TitleTextSize = 10;

        private const double offsetX = 0.93;
        private double offsetYBottom = 0.9;
        private double offsetYTop = 0.1;

        public CMLChartCanvas(SKBitmap image, SKColor border)
        {
            Width = image.Width;
            Height = image.Height;
            AsixXPaint = new SKPaint { TextSize = 9, Color = SKColors.Black };
            AsixYPaint = new SKPaint { Color = SKColors.Black };
            TitlePaint = new SKPaint { TextSize = TitleTextSize, Color = SKColors.Black };
            BarPaint = new SKPaint { TextSize = 10, Color = SKColors.Black };
            LinePaint = new SKPaint { TextSize = 10, Color = SKColors.Black };

            _image = image;
            _canvas = new SKCanvas(_image);
            _canvas.Clear(border);

        }

        /// <summary>
        /// Vykreslení popisu grafu
        /// </summary>
        /// <param name="label"></param>
        internal void DrawTitle(string label)
        {
            Name = label;

            if (!string.IsNullOrEmpty(label))
            {
                SKRect bounds = new SKRect();
                TitlePaint.MeasureText(label, ref bounds);

                var x = Width / 2 - bounds.MidX;
                var y = -bounds.Top;

                _canvas.DrawText(label, x, y, TitlePaint);
            }
        }

        /// <summary>
        /// Vykreslení osy Y
        /// </summary>
        /// <param name="yAsixs"></param>
        internal void DrawYAsixs(List<CMLChartYAsix> yAsixs)
        {
            foreach (var yAsix in yAsixs)
            {
                // Vykreslení osy
                int asixStartPointX;
                int asixEndPointX;

                if (yAsix.Location == Location.Left)
                {
                    asixStartPointX = Width - (int)Math.Floor(Width * offsetX);
                    asixEndPointX = Width - (int)Math.Floor(Width * offsetX);
                }
                else
                {
                    asixStartPointX = (int)Math.Floor(Width * offsetX);
                    asixEndPointX = (int)Math.Floor(Width * offsetX);
                }

                var asixStartPointY = (int)Math.Floor(Height * offsetYTop); ;
                var asixEndPointY = (int)Math.Floor(Height * offsetYBottom);

                _canvas.DrawLine(new SKPoint(asixStartPointX, asixStartPointY), new SKPoint(asixEndPointX, asixEndPointY), AsixYPaint);

                if (yAsix.ZoroValueMark && yAsix.MinValue < 0)
                {
                    const int separatorLength = 5;

                    var koef = (0 - yAsix.MinValue) / (yAsix.MaxValue - yAsix.MinValue);
                    var length = (int)Math.Floor(Height * (offsetYBottom - offsetYTop));
                    var zeroAsixStartPointX = Width - (int)Math.Floor(Width * offsetX) - separatorLength;
                    var zeroAsixStartPointY = (int)Math.Floor(Height * offsetYBottom) - (int)(koef * length);
                    var zeroAsixEndPointX = (int)Math.Floor(Width * offsetX);
                    var zeroAsixEndPointY = zeroAsixStartPointY;

                    DrawDashedLine(new SKPoint(zeroAsixStartPointX, zeroAsixStartPointY), new SKPoint(zeroAsixEndPointX, zeroAsixEndPointY), 5, 5);
                }

                var minIndex = 0;
                var maxIndex = 4;

                for (int i = minIndex; i <= maxIndex; i++)
                {
                    const int separatorLength = 5;

                    var koef = i * 0.25;

                    int asixDescStartPointX;
                    int asixDescEndPointX;

                    if (yAsix.Location == Location.Left)
                    {
                        asixDescStartPointX = Width - (int)Math.Floor(Width * offsetX) - separatorLength;
                        asixDescEndPointX = Width - (int)Math.Floor(Width * offsetX);
                    }
                    else
                    {
                        asixDescStartPointX = (int)Math.Floor(Width * offsetX);
                        asixDescEndPointX = (int)Math.Floor(Width * offsetX) + separatorLength;
                    }

                    var length = (int)Math.Floor(Height * (offsetYBottom - offsetYTop));
                    var asixDescStartPointY = (int)Math.Floor(Height * offsetYBottom) - (int)(koef * length);
                    var asixDescEndPointY = (int)Math.Floor(Height * offsetYBottom) - (int)(koef * length);

                    _canvas.DrawLine(new SKPoint(asixDescStartPointX, asixDescStartPointY), new SKPoint(asixDescEndPointX, asixDescEndPointY), AsixYPaint);

                    // Popisky
                    var lengthDesc = yAsix.MaxValue - yAsix.MinValue;
                    var label = (yAsix.MinValue + koef * lengthDesc).ToString("#0");

                    int x;
                    int y;
                    SKRect bounds = new SKRect();

                    AsixYPaint.TextSize = (i == minIndex || i == maxIndex) ? AsixYTextSizeBig : AsixYTextSize;
                    AsixYPaint.MeasureText(label, ref bounds);

                    if (yAsix.Location == Location.Left)
                    {
                        x = asixDescStartPointX - (int)(bounds.Width);
                        y = asixDescStartPointY - (int)bounds.Top + separatorLength;
                    }
                    else
                    {
                        x = asixDescEndPointX;
                        y = asixDescStartPointY - (int)bounds.Top + separatorLength;
                    }

                    _canvas.DrawText(label, x, y, AsixYPaint);
                }

                // Popiska osy
                if (!string.IsNullOrEmpty(yAsix.Label))
                {
                    _canvas.Save();


                    int x;
                    int y;
                    SKRect bounds = new SKRect();
                    TitlePaint.MeasureText(yAsix.Label, ref bounds);

                    if (yAsix.Location == Location.Left)
                    {
                        x = (int)-bounds.Top;
                        y = (Height / 2) + (int)bounds.Width / 2;
                        _canvas.RotateDegrees(270, x, y);
                    }
                    else
                    {
                        x = Width + (int)bounds.Top; ;
                        y = (Height / 2) - (int)bounds.Width / 2;

                        _canvas.RotateDegrees(90, x, y);
                    }
                    _canvas.DrawText(yAsix.Label, x, y, TitlePaint);
                    _canvas.Restore();
                }
            }

        }

        /// <summary>
        /// Vykreslení osy X
        /// </summary>
        /// <param name="xAsix"></param>
        internal void DrawXAsix(CMLChartXAsix xAsix)
        {
            const int separatorLength = 5;

            if (xAsix != null)
            {
                var asixStartPointX = Width - (int)Math.Floor(Width * offsetX);
                var asixXEndPointX = (int)Math.Floor(Width * offsetX);
                var asixStartPointY = (int)Math.Floor(Height * offsetYBottom);
                var asixXEndPointY = (int)Math.Floor(Height * offsetYBottom);

                _canvas.DrawLine(new SKPoint(asixStartPointX, asixStartPointY), new SKPoint(asixXEndPointX, asixXEndPointY), AsixXPaint);

                // Osa - hodiny
                if (xAsix.ValuesType == CMLValuesType.Hourly)
                {
                    var den = 0;

                    for (int i = 0; i < xAsix.Values.Length; i++)
                    {
                        var distance = ((double)i / xAsix.Values.Length) * (asixXEndPointX - asixStartPointX);

                        var startPointX = (int)(distance + asixStartPointX);
                        var endPointX = startPointX;
                        var startPointY = asixStartPointY;
                        var endPointY = startPointY + separatorLength;

                        // Přepážky
                        switch (xAsix.Values[i])
                        {
                            case "0":
                                _canvas.DrawLine(new SKPoint(startPointX, startPointY), new SKPoint(endPointX, endPointY + 3 * separatorLength), AsixXPaint);
                                break;
                            case "6":
                            case "12":
                            case "18":
                                _canvas.DrawLine(new SKPoint(startPointX, startPointY), new SKPoint(endPointX, endPointY + separatorLength), AsixXPaint);
                                break;
                            default:
                                _canvas.DrawLine(new SKPoint(startPointX, startPointY), new SKPoint(endPointX, endPointY), AsixXPaint);
                                break;
                        }


                        // Popisky
                        switch (xAsix.Values[i])
                        {
                            case "0":
                            case "6":
                            case "12":
                            case "18":
                                SKRect bounds = new SKRect();
                                AsixXPaint.MeasureText(xAsix.Values[i].ToString(), ref bounds);

                                var x = startPointX + 2;
                                var y = startPointY + separatorLength - bounds.Top;

                                _canvas.DrawText(xAsix.Values[i].ToString(), x, y, AsixXPaint);

                                break;
                        }

                        if (xAsix.Values[i] == "0")
                        {
                            den++;
                            var label = "N/A";
                            switch (den)
                            {
                                case 1:
                                    label = "zítra";
                                    break;
                                case 2:
                                    label = "pozítří";
                                    break;
                            }
                            SKRect bounds = new SKRect();
                            AsixXPaint.MeasureText(label, ref bounds);

                            var x = startPointX + 2;
                            var y = startPointY + (int)2 * separatorLength + 1 - bounds.Top;

                            _canvas.DrawText(label, x, y, AsixXPaint);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Vyhreslení křívek grafu
        /// </summary>
        internal void DrawChartValues(List<CMLChartYAsix> yAsixs)
        {
            foreach (var yAsix in yAsixs)
            {
                switch (yAsix.Type)
                {
                    case PresentationType.Bar:
                        DrawBarChartValues(yAsix);
                        break;
                    case PresentationType.Line:
                        DrawLineChartValues(yAsix);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Vykreslení přerušované čáry
        /// </summary>
        /// <param name="startPoint">Počáteční bod</param>
        /// <param name="endPoint">Koncový bod</param>
        /// <param name="lineLength">Délka plné čáry</param>
        /// <param name="spaceLength">Délka prázdné čáry</param>
        private void DrawDashedLine(SKPoint startPoint, SKPoint endPoint, int lineLength, int spaceLength)
        {
            var endPointX = startPoint.X;
            var endPointY = startPoint.Y;
            float startPointX;
            float startPointY;

            var length = Math.Sqrt(Math.Pow(Math.Abs(endPoint.X - startPoint.X), 2) + Math.Pow(Math.Abs(endPoint.Y - startPoint.Y), 2));
            var xKoef = (length == 0) ? 0 : (endPoint.X - startPoint.X) / length;
            var yKoef = (length == 0) ? 0 : (endPoint.Y - startPoint.Y) / length;

            var count = length / (lineLength + spaceLength);

            for (int i = 0; i < count; i++)
            {
                // plná čára
                startPointX = endPointX;
                startPointY = endPointY;
                endPointX = startPointX + (float)(lineLength * xKoef);
                endPointY = startPointY + (float)(lineLength * yKoef);
                _canvas.DrawLine(new SKPoint(startPointX, startPointY), new SKPoint(endPointX, endPointY), AsixYPaint);

                // prázdná čára 
                startPointX = endPointX;
                startPointY = endPointY;
                endPointX = startPointX + (float)(spaceLength * xKoef);
                endPointY = startPointY + (float)(spaceLength * yKoef);
            }
        }

        /// <summary>
        /// Vykreslení hodnot sloupcového grafu
        /// </summary>
        /// <param name="yAsix"></param>
        private void DrawBarChartValues(CMLChartYAsix yAsix)
        {
            if (yAsix != null)
            {
                var asixStartPointX = Width - (int)Math.Floor(Width * offsetX);
                var asixXEndPointX = (int)Math.Floor(Width * offsetX);
                var asixMaxPointY = (int)Math.Floor(Height * offsetYTop);
                var asixMinPointY = (int)Math.Floor(Height * offsetYBottom);

                for (int i = 0; i < yAsix.Values.Length; i++)
                {
                    var distance = ((double)i / yAsix.Values.Length) * (asixXEndPointX - asixStartPointX);
                    var valuePart = (yAsix.Values[i] - yAsix.MinValue) * (1 / (yAsix.MaxValue - yAsix.MinValue));
                    var barHeight = (int)((asixMinPointY - asixMaxPointY) * valuePart);

                    var x = (int)(distance + asixStartPointX);
                    var y = (int)Math.Floor(Height * offsetYBottom) - barHeight;
                    var width = (int)(((double)1 / yAsix.Values.Length) * (asixXEndPointX - asixStartPointX));

                    if (yAsix.Fill)
                    {
                        BarPaint.Style = SKPaintStyle.Fill;
                    }

                    _canvas.DrawRect(new SKRect(x, y, x + width, y + barHeight), BarPaint);

                }
            }
        }

        /// <summary>
        /// Vykreslení hodnot čárového grafu
        /// </summary>
        /// <param name="yAsix"></param>
        private void DrawLineChartValues(CMLChartYAsix yAsix)
        {
            if (yAsix != null)
            {
                var asixStartPointX = Width - (int)Math.Floor(Width * offsetX);
                var asixXEndPointX = (int)Math.Floor(Width * offsetX);
                var asixMaxPointY = (int)Math.Floor(Height * offsetYTop);
                var asixMinPointY = (int)Math.Floor(Height * offsetYBottom);

                SKPoint[] bezierPoints = new SKPoint[yAsix.Values.Length + 1];

                for (int i = 0; i < yAsix.Values.Length; i++)
                {
                    var distance = ((double)i / yAsix.Values.Length) * (asixXEndPointX - asixStartPointX);
                    var valuePart = (yAsix.Values[i] - yAsix.MinValue) * (1 / (yAsix.MaxValue - yAsix.MinValue));
                    var pointHeight = ((asixMinPointY - asixMaxPointY) * valuePart);

                    var x = (float)(distance + asixStartPointX);
                    var y = (float)((Height * offsetYBottom) - pointHeight);
                    bezierPoints[i] = new SKPoint(x, y);
                }
                bezierPoints[yAsix.Values.Length] = bezierPoints[yAsix.Values.Length - 1];

                SKPath path = new SKPath();
                path.MoveTo(bezierPoints[0]);

                for (int i = 1; i < bezierPoints.Length; i += 3)
                {
                    path.CubicTo(bezierPoints[i], bezierPoints[i + 1], bezierPoints[i + 2]);
                }
                path.MoveTo(bezierPoints[bezierPoints.Length - 1]);

                LinePaint.Style = SKPaintStyle.Stroke;
                _canvas.DrawPath(path, LinePaint);
            }
        }

        /// <summary>
        /// Získá obrázek
        /// </summary>
        /// <returns></returns>
        internal SKBitmap GetBitmap()
        {
            return _image;
        }
    }
}
