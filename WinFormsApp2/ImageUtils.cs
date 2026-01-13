using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Indigo
{
    public static class ImageUtils
    {
        // ROTATE (uncut, centered, transparent)
        public static Image RotateHex(Image source, float angle, int width, int height)
        {
            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // rotate around tile center
                g.TranslateTransform(width / 2f, height / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-width / 2f, -height / 2f);

                g.DrawImage(source, 0, 0, width, height);
            }

            return result;
        }

    }
}
