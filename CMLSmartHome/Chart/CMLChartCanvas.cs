using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace CMLSmartHomeController.Chart
{
    public class CMLChartCanvas
    {
        public int Width { get; }
        public int Height { get; }
        public string Name { get; set; }

        private Graphics _canvas;
        private Bitmap _image;

        private const double offsetX = 0.9;
        private double offsetYBottom = 0.8;
        private double offsetYTop = 0.1;

        internal CMLChartCanvas(Bitmap image)
        {
            Width = image.Width;
            Height = image.Height;

            _image = image;
            _canvas = Graphics.FromImage(_image);
            _canvas.Clear(Color.White);
        }

        /// <summary>
        /// Před snímku do negativu
        /// </summary>
        internal void ConvertBitmapToNegative()
        {
            int width = _image.Width;
            int height = _image.Height;
                  
            //negative
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value
                    Color p = _image.GetPixel(x, y);

                    //extract ARGB value from p
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    //find negative value
                    r = 255 - r;
                    g = 255 - g;
                    b = 255 - b;

                    //set new ARGB value in pixel
                    _image.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
        }

        /// <summary>
        /// Uložení grafu do obrázku
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageFormat"></param>
        internal void Save(string imageName, ImageFormat imageFormat, bool imageNegative )
        {
            if (imageNegative)
            {
                ConvertBitmapToNegative();
            };

            _image.Save(imageName, imageFormat);            
        }

        /// <summary>
        /// Získá obrázek
        /// </summary>
        /// <param name="imageNegative"></param>
        /// <returns></returns>
        internal Bitmap GetBitmap(bool imageNegative)
        {
            if (imageNegative)
            {
                ConvertBitmapToNegative();
            }

            return _image;
        }

        /// <summary>
        /// Uložení grafu do paměti
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="imageFormat"></param>
        internal MemoryStream Save(MemoryStream stream, ImageFormat imageFormat, bool imageNegative)
        {
            if (imageNegative)
            {
                ConvertBitmapToNegative();
            };

            _image.Save(stream, imageFormat);

            return stream;
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
                //var asixMaxPointY = Height - (int)Math.Floor(Height * offsetYBottom);
                var asixMinPointY = (int)Math.Floor(Height * offsetYBottom);

                var pen = new Pen(yAsix.BorderColor);
                PointF[] bezierPoints = new PointF[yAsix.Values.Length+1];

                for (int i = 0; i < yAsix.Values.Length; i++)
                {
                    var distance = ((double)i / yAsix.Values.Length) * (asixXEndPointX - asixStartPointX);
                    var valuePart = (yAsix.Values[i] - yAsix.MinValue) * (1 / (yAsix.MaxValue - yAsix.MinValue));
                    var pointHeight = ((asixMinPointY - asixMaxPointY) * valuePart);

                    var x = (float)(distance + asixStartPointX);
                    var y = (float)((Height * offsetYBottom) - pointHeight);
                    bezierPoints[i] = new PointF(x, y);
                }
                bezierPoints[yAsix.Values.Length] = bezierPoints[yAsix.Values.Length-1];

                _canvas.DrawCurve(pen, bezierPoints);
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
                //var asixMaxPointY = Height - (int)Math.Floor(Height * offsetYBottom);
                var asixMinPointY = (int)Math.Floor(Height * offsetYBottom);

                for (int i = 0; i < yAsix.Values.Length; i++)
                {
                    var distance = ((double)i / yAsix.Values.Length) * (asixXEndPointX - asixStartPointX);
                    var valuePart = (yAsix.Values[i] - yAsix.MinValue) * (1 / (yAsix.MaxValue - yAsix.MinValue));
                    var barHeight = (int)((asixMinPointY - asixMaxPointY) * valuePart);

                    var x = (int)(distance + asixStartPointX);
                    var y = (int)Math.Floor(Height * offsetYBottom) - barHeight;
                    var width = (int)(((double) 1 / yAsix.Values.Length) * (asixXEndPointX - asixStartPointX));

                    var pen = new Pen(yAsix.BorderColor);
                    _canvas.DrawRectangle(pen, x, y, width, barHeight);

                    if (yAsix.Fill)
                    {
                        SolidBrush blueBrush = new SolidBrush(yAsix.BackgroundColor);
                        _canvas.FillRectangle(blueBrush, x+1, y+1, width-1, barHeight-1);
                    }
                }
            }
        }

        /// <summary>
        /// Vykreslení popisu grafu
        /// </summary>
        /// <param name="label"></param>
        internal void DrawTitle(string label)
        {
            if (!string.IsNullOrEmpty(label))
            {
                Name = label;

                var labelFont = new Font(new FontFamily("Lucida Sans"), 9, FontStyle.Regular);
                var labelMeasure = _canvas.MeasureString(label, labelFont);

                var x = Width/2 - (labelMeasure.Width / 2);
                var y = 0;

                _canvas.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                _canvas.DrawString(label,
                    labelFont,
                    Brushes.Black, new PointF(x, y));
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

                //var asixStartPointY = Height - (int)Math.Floor(Height * offsetY);
                var asixStartPointY = (int)Math.Floor(Height * offsetYTop); ;
                var asixEndPointY = (int)Math.Floor(Height * offsetYBottom);

                var pen = new Pen(Brushes.Black);

                _canvas.DrawLines(pen, new Point[] { new Point(asixStartPointX, asixStartPointY), new Point(asixEndPointX, asixEndPointY) });

                for (int i = 0; i <= 4; i++)
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

                    //var length = (int)Math.Floor(Height * (2 * offsetY - 1));
                    var length = (int)Math.Floor(Height * (offsetYBottom - offsetYTop));
                    //var asixDescStartPointY = (int)Math.Floor(Height * offsetY) - (int)(koef * length);
                    var asixDescStartPointY = (int)Math.Floor(Height * offsetYBottom) - (int)(koef * length);
                    //var asixDescEndPointY = (int)Math.Floor(Height * offsetY) - (int)(koef * length);
                    var asixDescEndPointY = (int)Math.Floor(Height * offsetYBottom) - (int)(koef * length);

                    _canvas.DrawLines(pen, new Point[] { new Point(asixDescStartPointX, asixDescStartPointY), new Point(asixDescEndPointX, asixDescEndPointY) });

                    // Popisky
                    var labelFont = new Font(new FontFamily("Verdana"), 10, FontStyle.Regular);

                    var lengthDesc = yAsix.MaxValue - yAsix.MinValue;
                    var label = (yAsix.MinValue + koef * lengthDesc).ToString("#0.0");
                    
                    int x;
                    int y;

                    if (yAsix.Location == Location.Left)
                    {
                        var labelMeasure = _canvas.MeasureString(label, labelFont);
                        x = asixDescStartPointX - (int)(labelMeasure.Width) + separatorLength - 1;
                        y = asixDescStartPointY;
                    }
                    else
                    {
                        x = asixDescEndPointX - separatorLength + 1;
                        y = asixDescStartPointY;
                    }

                    _canvas.DrawString(label, labelFont, Brushes.Black, new PointF(x, y));
                }
                
                // Popiska osy
                if (!string.IsNullOrEmpty(yAsix.Label))
                {
                    var labelFont = new Font(new FontFamily("Lucida Sans"), 9, FontStyle.Regular);
                    var labelMeasure = _canvas.MeasureString(yAsix.Label, labelFont);

                    GraphicsState state = _canvas.Save();
                    _canvas.ResetTransform();

                    int x;
                    int y;

                    if (yAsix.Location == Location.Left)
                    {
                        x = 0;
                        y = (Height / 2) + (int)labelMeasure.Width / 2;
                        _canvas.RotateTransform(270);
                    }
                    else
                    {
                        x = Width;
                        y = (Height / 2) - (int)labelMeasure.Width / 2;
                        _canvas.RotateTransform(90);
                    }
                    _canvas.TranslateTransform(x, y, MatrixOrder.Append);
                    _canvas.DrawString(yAsix.Label, labelFont, Brushes.Black, new PointF(0, 0));

                    _canvas.Restore(state);
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

                var pen = new Pen(Brushes.Black);

                _canvas.DrawLines(pen, new Point[] { new Point(asixStartPointX, asixStartPointY), new Point(asixXEndPointX, asixXEndPointY) });

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
                                _canvas.DrawLines(pen, new Point[] { new Point(startPointX, startPointY), new Point(endPointX, endPointY + 3 * separatorLength) });
                                break;
                            case "6":
                            case "12":
                            case "18":
                                _canvas.DrawLines(pen, new Point[] { new Point(startPointX, startPointY), new Point(endPointX, endPointY + separatorLength) });
                                break;
                            default:
                                _canvas.DrawLines(pen, new Point[] { new Point(startPointX, startPointY), new Point(endPointX, endPointY) });
                                break;
                        }

                        // Popisky
                        switch (xAsix.Values[i])
                        {
                            case "0":
                            case "6":
                            case "12":
                            case "18":
                                var labelFont = new Font(new FontFamily("MS Reference Sans Serif"), 8, FontStyle.Regular);
                                var x = startPointX;
                                var y = startPointY + separatorLength - 2;

                                _canvas.DrawString(xAsix.Values[i].ToString(), labelFont, Brushes.Black, new PointF(x, y));

                                break;
                        }

                        if (xAsix.Values[i] == "0")
                        {
                            den++;

                            var labelFont = new Font(new FontFamily("MS Reference Sans Serif"), 10, FontStyle.Regular);

                            var x = startPointX;
                            var y = startPointY + (int)2.5*separatorLength;

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

                            _canvas.DrawString(label, labelFont, Brushes.Black, new PointF(x, y));                           
                        }
                    }
                }

                // Osa - textový popisek
                if (xAsix.ValuesType == CMLValuesType.String)
                {
                    for (int i = 0; i < xAsix.Values.Length; i++)
                    {
                        var distance = ((double)i / xAsix.Values.Length) * (asixXEndPointX - asixStartPointX);

                        var startPointX = (int)(distance + asixStartPointX);
                        var endPointX = startPointX;
                        var startPointY = asixStartPointY;
                        var endPointY = startPointY + separatorLength;

                        _canvas.DrawLines(pen, new Point[] { new Point(startPointX, startPointY), new Point(endPointX, endPointY) });

                        //:TODO: popisek hodnoty
                    }
                }

            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
