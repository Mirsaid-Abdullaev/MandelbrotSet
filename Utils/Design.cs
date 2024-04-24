using System.Drawing.Drawing2D;

namespace MandelbrotSet.Utils
{
    internal static class Design
    {
        public static Color[] Colours1 = new Color[] { Color.FromArgb(44, 62, 80), Color.FromArgb(52, 152, 219) };
        public static Color[] Colours2 = new Color[] { Color.FromArgb(2, 170, 176), Color.FromArgb(1, 181, 175), Color.FromArgb(0, 189, 174), Color.FromArgb(0, 195, 173), Color.FromArgb(0, 205, 172) };
        public static Color[] Colours3 = new Color[] { Color.FromArgb(238,238,255), Color.FromArgb(173, 255, 183) };
        public static Color[] Colours4 = new Color[] { Color.FromArgb(35, 7, 77), Color.FromArgb(204, 83, 51) };
        public static Color[] Colours5 = new Color[] { Color.FromArgb(55, 59, 68), Color.FromArgb(66, 134, 244) };
        public static Color[] Colours6 = new Color[] { Color.FromArgb(255, 216, 155), Color.FromArgb(25, 84, 123) };
        public static Color[] Colours7 = new Color[] { Color.FromArgb(0, 90, 167), Color.FromArgb(255, 253, 228) };
        public static void SetControlGradient(Control control, Color[] colors)
        {
            ColorBlend colorBlend = new() { Colors = colors, Positions = CalculateGradientPositions(colors.Length) };
            LinearGradientBrush linearGradientBrush = new(control.ClientRectangle, Color.Black, Color.Black, LinearGradientMode.ForwardDiagonal) { InterpolationColors = colorBlend };
            control.BackgroundImage = new Bitmap(1, 1);
            control.BackgroundImage = DrawToBitmap(linearGradientBrush, control.ClientRectangle.Size);
        }
        public static float[] CalculateGradientPositions(int count)
        {
            float[] positions = new float[count];
            for (int i = 0; i < count; i++)
            {
                positions[i] = (float)i / (float)(count - 1);
            }
            return positions;
        }

        public static Bitmap DrawToBitmap(Brush brush, Size size)
        {
            Bitmap bitmap = new(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(brush, new Rectangle(Point.Empty, size));
            }
            return bitmap;
        }
    }
}
