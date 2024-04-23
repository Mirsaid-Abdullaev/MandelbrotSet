namespace MandelbrotSet.Utils
{
    internal class CurrentState
    {
        public Bitmap ViewBmp; //saving the image of the previous view (interesting caching experiment)

        public ComplexNumber TopRightScaled;
        public ComplexNumber BottomLeftScaled;

        public CurrentState(Bitmap ViewBitmap, ComplexNumber TR, ComplexNumber BL)
        {
            this.ViewBmp = ViewBitmap;
            TopRightScaled = new ComplexNumber() { x = TR.x, y = TR.y };
            BottomLeftScaled = new ComplexNumber() { x = BL.x, y = BL.y };
        }
    }
}
