using SkiaSharp;
using System.Collections.Generic;

namespace CMLSmartHomeController.Chart
{
    /// <summary>
    /// CMLChart - Graf
    /// </summary>
    public class CMLChart
    {
        /// <summary>
        /// Popisek
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Malování osy X
        /// </summary>
        public SKPaint AsixXPaint { get; set; }
        /// <summary>
        /// Malování osy Y
        /// </summary>
        public SKPaint AsixYPaint { get; set; }
        /// <summary>
        /// Malování titulku
        /// </summary>
        public SKPaint TitlePaint { get; set; }
        /// <summary>
        /// Malování pozadí
        /// </summary>
        public SKPaint BorderPaint { get; set; }
        /// <summary>
        /// Malování Baru
        /// </summary>
        public SKPaint BarPaint { get; set; }
        /// <summary>
        /// Malování čáry
        /// </summary>
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
