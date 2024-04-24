namespace MandelbrotSet.Utils
{
    internal class CurrentState
    {
        public ComplexNumber TopRightScaled;
        public ComplexNumber BottomLeftScaled;
        public int MaxIter;
        public CurrentState(ComplexNumber TR, ComplexNumber BL, int MaxIter)
        {
            TopRightScaled = new ComplexNumber() { x = TR.x, y = TR.y };
            BottomLeftScaled = new ComplexNumber() { x = BL.x, y = BL.y };
            this.MaxIter = MaxIter;
        }
    }
}
