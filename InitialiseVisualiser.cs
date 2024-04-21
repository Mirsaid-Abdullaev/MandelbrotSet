using System.Drawing.Drawing2D;
using System.Drawing;

namespace MandelbrotSet
{
    public partial class InitialiseVisualiser : Form
    {
        public int Resolution = 550;
        public int MaxIter = 250;
        public InitialiseVisualiser()
        {
            InitializeComponent();
            SetControlGradient(this, BkgColours);
            lblCurrentRes.Text = $"Current Resolution: {ResolutionChoice.Value} x {ResolutionChoice.Value / 8 * 5}";
        }


        private void BtnInitialiseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private readonly Color[] BkgColours = new Color[] { Color.FromArgb(44, 62, 80), Color.FromArgb(52, 152, 219) };
        private void SetControlGradient(Control control, Color[] colors)
        {
            ColorBlend colorBlend = new() { Colors = colors, Positions = CalculateGradientPositions(colors.Length) };
            LinearGradientBrush linearGradientBrush = new(control.ClientRectangle, Color.Black, Color.Black, LinearGradientMode.ForwardDiagonal) { InterpolationColors = colorBlend };
            control.BackgroundImage = new Bitmap(1, 1);
            control.BackgroundImage = DrawToBitmap(linearGradientBrush, control.ClientRectangle.Size);
            control.ForeColor = Color.DodgerBlue;
        }
        private float[] CalculateGradientPositions(int count)
        {
            float[] positions = new float[count];
            for (int i = 0; i < count; i++)
            {
                positions[i] = (float)i / (float)(count - 1);
            }
            return positions;
        }

        private Bitmap DrawToBitmap(Brush brush, Size size)
        {
            Bitmap bitmap = new(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(brush, new Rectangle(Point.Empty, size));
            }
            return bitmap;
        }

        private void ResolutionChoice_ValueChanged(object sender, EventArgs e)
        {
            Resolution = ResolutionChoice.Value;
        }

        private void ResolutionChoice_Scroll(object sender, EventArgs e)
        {
            lblCurrentRes.Text = $"Current Resolution: {ResolutionChoice.Value} x {(int)((double)(ResolutionChoice.Value * 5) / 8.0)}";
        }

        private void MaxIterChoice_Scroll(object sender, EventArgs e)
        {
            lblCurrentMaxIter.Text = $"Current Max Iterations: {MaxIterChoice.Value}";
        }

        private void MaxIterChoice_ValueChanged(object sender, EventArgs e)
        {
            MaxIter = MaxIterChoice.Value;
        }
    }
}
