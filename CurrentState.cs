namespace MandelbrotSet
{
    internal class CurrentState
    {
        public Bitmap View; //saving the image of the previous view (interesting caching experiment)

        public PointF TopLeftScaledCoord;
        public PointF BottomRightScaledCoord;

        public CurrentState(Bitmap View, PointF TL, PointF BR)
        {
            this.View = View;
            this.TopLeftScaledCoord = new PointF() { X = TL.X, Y = TL.Y };
            this.BottomRightScaledCoord = new PointF() { X = BR.X, Y = BR.Y };
        }
    }
}
