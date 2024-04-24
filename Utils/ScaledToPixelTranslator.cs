using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace MandelbrotSet.Utils
{
    internal class PixelCoord
    {
        public int XPixel;
        public int YPixel;
    }
    internal class ScaledToPixelTranslator
    {
        private readonly double PPSU_X; //constant used for holding the pixels per scaled coord unit (x-direction)
        private readonly double PXSU_X; //second constant used for holding the x-min scaled pixels (x-direction)
        private readonly double PPSU_Y; //same as PPSU_X but in y-direction
        private readonly double PYSU_Y; //same as PXSU_X but for y-direction

        public ScaledToPixelTranslator(Graphics g, ComplexNumber BottomLeft, ComplexNumber TopRight)
        {
            PPSU_X = (double)g.VisibleClipBounds.Width / (TopRight.x - BottomLeft.x);
            PPSU_Y = (double)g.VisibleClipBounds.Height / (TopRight.y - BottomLeft.y);
            PXSU_X = PPSU_X * BottomLeft.x;
            PYSU_Y = PPSU_Y * TopRight.y;
        }
        public ComplexNumber GetScaledCoord(ComplexNumber PixelCoord)
        {
            ComplexNumber ScaledCoord = new ComplexNumber()
            {
                x = (PXSU_X + PixelCoord.x) / PPSU_X,
                y = (PYSU_Y - PixelCoord.y) / PPSU_Y
            };
            return ScaledCoord;
        }
        public ComplexNumber GetScaledCoordChange(ComplexNumber PixelCoord)
        {
            ComplexNumber Result = new ComplexNumber(PixelCoord.x / PPSU_X, PixelCoord.y / PPSU_Y);
            return Result;
        }
    }
}
