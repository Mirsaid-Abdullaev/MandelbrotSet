using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace MandelbrotSet.Utils
{
    internal class CurrentState //utility class used to hold previous view's parameters to enable the undo function to work alongside the stack
    {
        public ComplexNumber TopRightScaled; //top right coordinate of the previous view's area in complex coordinates
        public ComplexNumber BottomLeftScaled; //bottom left coordinate of previous view's area in complex coordinates
        public int MaxIter; //the value of max iterations at the time of that view
        public CurrentState(ComplexNumber TR, ComplexNumber BL, int MaxIter) //constructor method that sets all these values
        {
            TopRightScaled = new ComplexNumber() { x = TR.x, y = TR.y };
            BottomLeftScaled = new ComplexNumber() { x = BL.x, y = BL.y };
            this.MaxIter = MaxIter;
        }
    }
}
