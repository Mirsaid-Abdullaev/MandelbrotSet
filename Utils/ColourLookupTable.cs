using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;

namespace MandelbrotSet.Utils
{
    internal class ColourLookupTable //utility class that is used to precompute all colours for a given max iterations count to reduce computation required after every zoom/pan
    {
        public int MaxIterations; //the current maximum iterations that the mandelbrot equation will be allowed to iterate until
        private readonly Color[][] ColourTable; //the array storing the different colour palettes and every possible colour based on the iteration count

        public ColourLookupTable(int MaxIter) //constructor for filling a colourtable
        {
            this.MaxIterations = MaxIter;
            this.ColourTable = new Color[3][]; //currently has 3 different palettes
            ColourTable[0] = new Color[MaxIter];
            ColourTable[1] = new Color[MaxIter];
            ColourTable[2] = new Color[MaxIter];

            for (int i = 0; i < MaxIter; i++) //each iteration count has an associated colour, this is worked out using custom colouring algorithms.
            {
                //To add new ones, simply change one of the existing ones, or add another row to the Colour table array and define a new algorithm here

                double ColourReturnValue = Math.Pow(Math.Pow((double)i / (double)MaxIter, 0.9) * MaxIter, 1.5) % 360 / 360;
                ColourTable[0][i] = Design.HSLToRGB(ColourReturnValue, 0.9, (double) i / (double) MaxIter);
                ColourReturnValue = Math.Pow(SmoothIteration(i), 0.25);
                ColourTable[1][i] = Design.HSLToRGB(ColourReturnValue, 0.75, 0.5); 
                ColourReturnValue = (double)i / MaxIter;
                ColourTable[2][i] = Design.HSLToRGB((double)Math.Pow(ColourReturnValue, 0.25), 0.7, 0.6);
            }
        }
        private static double SmoothIteration(double i) //helper function that performs logarithmic smoothing on a given iteration count
        {
            return (double)(i + 1 - Math.Log2(Math.Log2(i))) / Math.Log2(15);
        }
        public Color GetColor(int i, int ColorPalette) //getter method to return the colour for a given iteration count and palette from the colourtable array
        {
            return ColourTable[ColorPalette - 1][i];
        }
    }
}
