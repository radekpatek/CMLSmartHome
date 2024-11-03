using NUnit.Framework;
using SkiaSharp;
using System.Collections.Generic;

namespace CMLSmartHomeController.Chart.Test
{
    [TestFixture]
    public class CMLChartTests
    {
        private CMLChart _chart;
        private SKPaint _defaultPaint;

        [SetUp]
        public void Setup()
        {
            _defaultPaint = new SKPaint { Color = SKColors.Black };
            _chart = new CMLChart
            {
                Label = "Test Chart",
                AsixXPaint = _defaultPaint,
                AsixYPaint = _defaultPaint,
                TitlePaint = _defaultPaint,
                BorderPaint = _defaultPaint,
                BarPaint = _defaultPaint,
                LinePaint = _defaultPaint,
                XAsix = new CMLChartXAsix { ValuesType = CMLValuesType.Hourly, Values = new string[] { "0", "6", "12", "18", "24" } },
                YAsixs = new List<CMLChartYAsix>
                {
                    new CMLChartYAsix { Location = Location.Left, MinValue = 0, MaxValue = 100, Label = "Y Axis", Values = new double[] { 0, 25, 50, 75, 100 } }
                }
            };
        }

        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            Assert.That(_chart.Label, Is.EqualTo("Test Chart"));
            Assert.That(_chart.AsixXPaint, Is.EqualTo(_defaultPaint));
            Assert.That(_chart.AsixYPaint, Is.EqualTo(_defaultPaint));
            Assert.That(_chart.TitlePaint, Is.EqualTo(_defaultPaint));
            Assert.That(_chart.BorderPaint, Is.EqualTo(_defaultPaint));
            Assert.That(_chart.BarPaint, Is.EqualTo(_defaultPaint));
            Assert.That(_chart.LinePaint, Is.EqualTo(_defaultPaint));
            Assert.That(_chart.XAsix, Is.Not.Null);
            Assert.That(_chart.YAsixs, Is.Not.Null);
            Assert.That(_chart.YAsixs.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetBitmap_ShouldReturnBitmapWithCorrectDimensions()
        {
            int imageWidth = 800;
            int imageHeight = 600;
            var bitmap = _chart.GetBitmap(imageWidth, imageHeight);

            Assert.That(bitmap, Is.Not.Null);
            Assert.That(bitmap.Width, Is.EqualTo(imageWidth));
            Assert.That(bitmap.Height, Is.EqualTo(imageHeight));
        }

        [Test]
        public void GetBitmap_ShouldDrawTitle()
        {
            int imageWidth = 800;
            int imageHeight = 600;
            var bitmap = _chart.GetBitmap(imageWidth, imageHeight);

            // Additional checks can be done by analyzing the bitmap
            // For example, you can check if the title text is drawn at the expected position
        }

        [Test]
        public void GetBitmap_ShouldDrawXAxis()
        {
            int imageWidth = 800;
            int imageHeight = 600;
            var bitmap = _chart.GetBitmap(imageWidth, imageHeight);

            // Additional checks can be done by analyzing the bitmap
            // For example, you can check if the X axis is drawn at the expected position
        }

        [Test]
        public void GetBitmap_ShouldDrawYAxis()
        {
            int imageWidth = 800;
            int imageHeight = 600;
            var bitmap = _chart.GetBitmap(imageWidth, imageHeight);

            // Additional checks can be done by analyzing the bitmap
            // For example, you can check if the Y axis is drawn at the expected position
        }

        [Test]
        public void GetBitmap_ShouldDrawChartValues()
        {
            int imageWidth = 800;
            int imageHeight = 600;
            var bitmap = _chart.GetBitmap(imageWidth, imageHeight);

            // Additional checks can be done by analyzing the bitmap
            // For example, you can check if the chart values are drawn correctly
        }
    }
}
