using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MandelbrotSet.Utils
{
    internal class ColourLookupTable
    {
        public int MaxIterations;
        private readonly Color[][] ColourTable;

        public ColourLookupTable(int MaxIter)
        {
            this.MaxIterations = MaxIter;
            this.ColourTable = new Color[3][];
            ColourTable[0] = new Color[MaxIter];
            ColourTable[1] = new Color[MaxIter];
            ColourTable[2] = new Color[MaxIter];
            for (int i = 0; i < MaxIter; i++)
            {
                double ColourReturnValue = Math.Pow(Math.Pow((double)i / (double)MaxIter, 0.9) * MaxIter, 1.5) % 360 / 360;
                ColourTable[0][i] = Util.HSLToRGB(ColourReturnValue, 0.9, (double) i / (double) MaxIter);
            }
            for (int i = 0; i < MaxIter; i++)
            {
                double ColourReturnValue = Math.Pow(SmoothIteration(i), 0.25);
                ColourTable[1][i] = Util.HSLToRGB(ColourReturnValue, 0.75, 0.5);
            }
            for (int i = 0; i < MaxIter; i++)
            {
                double ColourReturnValue = (double)i / MaxIter;
                ColourTable[2][i] = Util.HSLToRGB((double)Math.Pow(ColourReturnValue, 0.25), 0.7, 0.6);
            }
        }
        private static double SmoothIteration(double i)
        {
            return (double)(i + 1 - Math.Log2(Math.Log2(i))) / Math.Log2(15);
        }
        public Color GetColor(int i, int ColorPalette)
        {
            return ColourTable[ColorPalette - 1][i];
        }
    }
}
