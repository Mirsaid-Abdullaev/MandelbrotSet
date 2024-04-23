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
                double ColourReturnValue = 1 - Math.Log(i) / Math.Log(MaxIter) + Math.Log(2);
                double HueAngle = Math.Pow(ColourReturnValue, 0.25);
                ColourTable[i] = Util.HSLToRGB(HueAngle, 0.9, 0.6);
            }
        }

        public Color GetColor(int i)
        {
            return ColourTable[i];
        }
    }
}
