using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace CMLSmartHomeController.Chart.Test
{
    [TestFixture]
    public class CMLChartCanvasTests
    {
        private CMLChartCanvas _canvas;        
        private SKBitmap _bitmap;

        [TearDown]
        public void TearDown()
        {
            _bitmap.Dispose();
        }
        private SKColor _borderColor;

        [SetUp]
        public void Setup()
        {
            _bitmap = new SKBitmap(800, 600);
            _borderColor = SKColors.White;
            _canvas = new CMLChartCanvas(_bitmap, _borderColor);
        }

        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            Assert.That(_canvas.Width, Is.EqualTo(800));
            Assert.That(_canvas.Height, Is.EqualTo(600));
            Assert.That(_canvas.AsixXPaint, Is.Not.Null);
            Assert.That(_canvas.AsixYPaint, Is.Not.Null);
            Assert.That(_canvas.TitlePaint, Is.Not.Null);
            Assert.That(_canvas.BarPaint, Is.Not.Null);
            Assert.That(_canvas.LinePaint, Is.Not.Null);
        }

        [Test]
        public void DrawTitle_ShouldSetNameAndDrawText()
        {
            string title = "Test Title";
            _canvas.DrawTitle(title);

            Assert.That(_canvas.Name, Is.EqualTo(title));
            // Additional checks can be done by analyzing the bitmap
        }

        [Test]
        public void DrawYAsixs_ShouldDrawYAxis()
        {
            var yAsixs = new List<CMLChartYAsix>
            {
                new CMLChartYAsix { Location = Location.Left, MinValue = 0, MaxValue = 100, Label = "Y Axis", Values = new double[] { 0, 25, 50, 75, 100 } }
            };

            _canvas.DrawYAsixs(yAsixs);

            // Additional checks can be done by analyzing the bitmap
        }

        [Test]
        public void DrawXAsix_ShouldDrawXAxis()
        {
            var xAsix = new CMLChartXAsix { ValuesType = CMLValuesType.Hourly, Values = new string[] { "0", "6", "12", "18", "24" } };

            _canvas.DrawXAsix(xAsix);

            // Additional checks can be done by analyzing the bitmap
        }

        [Test]
        public void DrawChartValues_ShouldDrawChart()
        {
            var yAsixs = new List<CMLChartYAsix>
            {
                new CMLChartYAsix { Type = PresentationType.Bar, MinValue = 0, MaxValue = 100, Values = new double[] { 10, 20, 30, 40, 50 } }
            };

            _canvas.DrawChartValues(yAsixs);

            // Additional checks can be done by analyzing the bitmap
        }

        [Test]
        public void GetBitmap_ShouldReturnBitmap()
        {
            var bitmap = _canvas.GetBitmap();

            Assert.That(bitmap, Is.Not.Null);
            Assert.That(bitmap, Is.EqualTo(_bitmap));
        }
    }
}
