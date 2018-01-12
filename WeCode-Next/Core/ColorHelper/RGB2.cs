using System;

namespace WeCode_Next.Core.ColorHelper
{
    class RGB2
    {
        /// <summary>
        /// Converts RGB to HSB.
        /// </summary>
        public static HSB RGBtoHSB(int red, int green, int blue)
        {
            // normalize red, green and blue values
            double r = ((double)red / 255.0);
            double g = ((double)green / 255.0);
            double b = ((double)blue / 255.0);

            // conversion start
            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            double h = 0.0;
            if (max == r && g >= b)
            {
                h = 60 * (g - b) / (max - min);
            }
            else if (max == r && g < b)
            {
                h = 60 * (g - b) / (max - min) + 360;
            }
            else if (max == g)
            {
                h = 60 * (b - r) / (max - min) + 120;
            }
            else if (max == b)
            {
                h = 60 * (r - g) / (max - min) + 240;
            }

            double s = (max == 0) ? 0.0 : (1.0 - (min / max));

            return new HSB(h, s, (double)max);
        }
    }
}
