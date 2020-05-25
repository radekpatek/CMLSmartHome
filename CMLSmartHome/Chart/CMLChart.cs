using SkiaSharp;
using System.Collections.Generic;

namespace CMLSmartHomeController.Chart
{
    public class CMLChart
    {
        public string Label { get; set; }

        public SKPaint AsixXPaint { get; set; }
        public SKPaint AsixYPaint { get; set; }
        public SKPaint TitlePaint { get; set; }
        public SKPaint BorderPaint { get; set; }
        public SKPaint BarPaint { get; set; }
        public SKPaint LinePaint { get; set; }

        /// <summary>
        /// Typ grafu
        /// </summary>
        protected enum ChartType
        {
            line,
            bar
        }

        public CMLChartXAsix XAsix { get; internal set; }
        public List<CMLChartYAsix> YAsixs { get; internal set; }

        internal SKBitmap GetBitmap(int imageWidth, int imageHeight)
        {
            SKBitmap image = new SKBitmap(imageWidth, imageHeight);

            var canvas = new CMLChartCanvas(image, BorderPaint.Color);

            canvas.AsixXPaint = AsixXPaint;
            canvas.AsixYPaint = AsixYPaint;
            canvas.TitlePaint = TitlePaint;
            canvas.BarPaint = BarPaint;
            canvas.LinePaint = LinePaint;

            canvas.DrawTitle(Label);
            canvas.DrawXAsix(XAsix);
            canvas.DrawYAsixs(YAsixs);
            canvas.DrawChartValues(YAsixs);

            return canvas.GetBitmap();
        }
    }
}
