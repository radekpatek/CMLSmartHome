using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphToBitmap.BitmapCnv
{
    public class BitmapConvert
    {
        private Bitmap _bitmap;

        public BitmapConvert(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        /// <summary>
        /// Získá obrázek v podobě pole bajtů
        /// </summary>
        /// <returns></returns>
        public byte[] GetBitmapByByteArray()
        {

            List<byte> bitmap = new List<byte>();

            if (_bitmap.Width % 8 == 0)
            {
                StringBuilder sbLine = new StringBuilder();
                for (int ii = 0; ii < _bitmap.Height; ii++)
                {
                    // loop each row of image
                    for (int jj = 0; jj < _bitmap.Width; jj++)
                    {
                        // loop each pixel in row and add to sbLine string to build
                        // string of bits for black and white image
                        Color pixelColor = _bitmap.GetPixel(jj, ii);
                        sbLine.Append(HexConverter(pixelColor));
                    }
                    // convert sbline string to byte array
                    byte[] buffer = GetBytes(sbLine.ToString());
                    bitmap.AddRange(buffer);
                    sbLine.Clear();
                }
            }
            else
            {
                throw new Exception("CMLChartCanvas.GetBitmap() - Image is not a multiple of 8 pixels wide");
            }

            return bitmap.ToArray();
        }

        /// <summary>
        /// Převod řetězců na pole bajtů. Každých 8 znaků se převede na byte.  
        /// </summary>
        /// <param name="bitString"></param>
        /// <returns></returns>
        private byte[] GetBytes(string bitString)
        {
            return Enumerable.Range(0, bitString.Length / 8).
                Select(pos => Convert.ToByte(
                    bitString.Substring(pos * 8, 8),
                    2)
                ).ToArray();
        }

        /// <summary>
        /// Převod barevného bodu na "0"-černý bod, "1"-bod má jinou barvu 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private String HexConverter(Color color)
        {
            if ((color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2")).Equals("000000"))
            {
                // image black pixel
                return "0";
            }
            else
            {
                // image is not a black pixel
                return "1";
            }
        }

        /// <summary>
        /// Získá obrázek v podobě řetězce hexa znaků
        /// </summary>
        /// <returns></returns>
        public string GetBitmapByString()
        {
            StringBuilder bitmap = new StringBuilder();

            var bitmapByteArray = GetBitmapByByteArray();

            bitmap.Append("{0x" + Environment.NewLine);
            bitmap.Append(BitConverter.ToString(bitmapByteArray).Replace("-", ",0x"));
            bitmap.Append("};" + Environment.NewLine);

            return bitmap.ToString();
        }
    }
}