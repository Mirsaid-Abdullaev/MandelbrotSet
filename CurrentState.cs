using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSet
{
    internal class CurrentState
    {
        public Bitmap View; //saving the image of the previous view (interesting caching experiment)

        public PointF TopLeftScaledCoord;
        public PointF TopRightScaledCoord;
        public PointF BottomLeftScaledCoord;
        public PointF BottomRightScaledCoord;


    }
}
