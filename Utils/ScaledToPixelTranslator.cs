using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace MandelbrotSet.Utils
{
    internal class ScaledToPixelTranslator //utility class to convert between screen pixels and complex coordinates based on scale factor calculations
    {
        private readonly double PPSU_X; //constant used for holding the pixels per scaled coord unit (x-direction)
        private readonly double PPSU_Y; //same as PPSU_X but in y-direction

        public ScaledToPixelTranslator(Graphics g, ComplexNumber BottomLeft, ComplexNumber TopRight) //works out the above constants required for future conversions
        {
            PPSU_X = (double)g.VisibleClipBounds.Width / (TopRight.x - BottomLeft.x);
            PPSU_Y = (double)g.VisibleClipBounds.Height / (TopRight.y - BottomLeft.y);
        }
        public ComplexNumber GetScaledCoordChange(ComplexNumber PixelCoord) //gets the necessary change in complex-number adjusted values given a screen pixel value change
        {
            ComplexNumber Result = new ComplexNumber(PixelCoord.x / PPSU_X, PixelCoord.y / PPSU_Y);
            return Result;
        }
    }
}
