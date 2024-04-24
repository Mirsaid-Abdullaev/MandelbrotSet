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
        private readonly Color[] ColourTable;

        public ColourLookupTable(int MaxIter)
        {
            this.MaxIterations = MaxIter;
            this.ColourTable = new Color[MaxIter];

            for (int i = 0; i < MaxIter; i++)
            {
                double ColourReturnValue = Math.Pow(Math.Pow((double)i / (double)MaxIter, 0.9) * MaxIter, 1.5) % 360 / 360;
                ColourTable[i] = Util.HSLToRGB(ColourReturnValue, 0.9, (double) i / (double) MaxIter);
            }
        }
        private static double SmoothIteration(double i)
        {
            return (double)(i + 1 - Math.Log(Math.Log(i)) / Math.Log(2));
        }

        public Color GetColor(int i)
        {
            return ColourTable[i];
        }
    }
}
