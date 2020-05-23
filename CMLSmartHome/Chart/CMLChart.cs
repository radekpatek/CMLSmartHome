using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CMLSmartHomeController.Chart
{
    public class CMLChart   
    {
        public string Label { get; set; }

        public CMLChartXAsix XAsix { get; set; }
        public List<CMLChartYAsix> YAsixs { get; set; }

        /// <summary>
        /// Typ grafu
        /// </summary>
        protected enum ChartType
        {
            line,
            bar
        }

        /// <summary>
        /// Uložení grafu do souboru
        /// </summary>
        public void SaveToImage(string imageName, int imageWidth, int imageHeight, ImageFormat imageFormat, bool imageNegative)
        {
            Bitmap image = new Bitmap(imageWidth, imageHeight);

            var canvas = new CMLChartCanvas(image);
            
            canvas.DrawTitle(Label);

            canvas.DrawXAsix(XAsix);

            canvas.DrawYAsixs(YAsixs);

            canvas.DrawChartValues(YAsixs);

            canvas.Save(imageName, imageFormat, imageNegative);            
        }

        /// <summary>
        /// Uložení grafu do memory
        /// </summary>
        public void SaveToImage(MemoryStream stream, int imageWidth, int imageHeight, ImageFormat imageFormat, bool imageNegative)
        {
            Bitmap image = new Bitmap(imageWidth, imageHeight);

            var canvas = new CMLChartCanvas(image);

            canvas.DrawTitle(Label);

            canvas.DrawXAsix(XAsix);

            canvas.DrawYAsixs(YAsixs);

            canvas.DrawChartValues(YAsixs);

            canvas.Save(stream, imageFormat, imageNegative);
        }

        /// <summary>
        /// Uložení grafu do souboru
        /// </summary>
        public Bitmap GetBitmap(int imageWidth, int imageHeight, bool imageNegative)
        {
            Bitmap image = new Bitmap(imageWidth, imageHeight);

            var canvas = new CMLChartCanvas(image);

            canvas.DrawTitle(Label);

            canvas.DrawXAsix(XAsix);

            canvas.DrawYAsixs(YAsixs);

            canvas.DrawChartValues(YAsixs);

            return canvas.GetBitmap(imageNegative);
        }

    }
}
