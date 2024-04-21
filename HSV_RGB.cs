using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    internal class HSV_RGB
    {
        public static (int, int, int) HSVtoRGB(double h, double s, double v)
        {
            int hi = Convert.ToInt32(Math.Floor(h / 60)) % 6;
            double f = h / 60 - Math.Floor(h / 60);

            double p = v * (1 - s);
            double q = v * (1 - f * s);
            double t = v * (1 - (1 - f) * s);

            int r, g, b;
            switch (hi)
            {
                case 0:
                    r = Convert.ToInt32(v * 255);
                    g = Convert.ToInt32(t * 255);
                    b = Convert.ToInt32(p * 255);
                    break;
                case 1:
                    r = Convert.ToInt32(q * 255);
                    g = Convert.ToInt32(v * 255);
                    b = Convert.ToInt32(p * 255);
                    break;
                case 2:
                    r = Convert.ToInt32(p * 255);
                    g = Convert.ToInt32(v * 255);
                    b = Convert.ToInt32(t * 255);
                    break;
                case 3:
                    r = Convert.ToInt32(p * 255);
                    g = Convert.ToInt32(q * 255);
                    b = Convert.ToInt32(v * 255);
                    break;
                case 4:
                    r = Convert.ToInt32(t * 255);
                    g = Convert.ToInt32(p * 255);
                    b = Convert.ToInt32(v * 255);
                    break;
                default:
                    r = Convert.ToInt32(v * 255);
                    g = Convert.ToInt32(p * 255);
                    b = Convert.ToInt32(q * 255);
                    break;
            }
            return (r, g, b);
        }
    }
}
