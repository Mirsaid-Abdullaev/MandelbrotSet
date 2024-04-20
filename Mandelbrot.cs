namespace MandelbrotSet
{
    public partial class Mandelbrot : Form
    {
        private int XRes;
        private int YRes;

        private int ControlXSize;
        private int ControlYSize;

        private PointF TopLeftScaledCoord;
        private PointF TopRightScaledCoord;
        private PointF BottomLeftScaledCoord;
        private PointF BottomRightScaledCoord;

        private bool IsComputing;
        //private Thread calculatorThread;
        private Thread bkgDrawThread;

        //private bool[][] InMandelbrot;
        private double[][] InMandelbrot;

        private PointF[][] Centres;
        private PointF[][] ScaledCoords;

        private RectangleF[][] Pixels;

        private int MaxIter;


        private Stack<CurrentState> ViewStack;




        public Mandelbrot(int Resolution, int MaxIter)
        {
            InitializeComponent();

            this.XRes = Resolution;
            this.YRes = (int)((double)XRes / (double)1.33);

            this.MaxIter = MaxIter;

            //calculatorThread = new Thread(CalculateNewPositions) { IsBackground = true };
            bkgDrawThread = new Thread(PaintArea) { IsBackground = true };

            InMandelbrot = new double[XRes][]; //will be an array of resolution x 5/8 resolution representing all the pixel areas of the screen
            Centres = new PointF[YRes][];

            ScaledCoords = new PointF[YRes][];

            TopLeftScaledCoord = new PointF(-2, 2);
            TopRightScaledCoord = new PointF(2, 2);
            BottomLeftScaledCoord = new PointF(-2, -2);
            BottomRightScaledCoord = new PointF(2, -2);
            Pixels = new RectangleF[YRes][];

            ControlXSize = this.Width - 17;
            ControlYSize = this.Height - 40;
            //initial coordinate map will be the area bounded by these coordinates, the centre of the mandelbrot set.
        }

        //private void CalculateNewPositions()
        //{

        //}

        private void Mandelbrot_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < YRes; i++)
            //{
            //    Centres[i] = new PointF[XRes];
            //    for (int j = 0; j < XRes; j++)
            //    {
            //        Centres[i][j] = new PointF((float)ControlXSize / (float)2 + ControlXSize * (float)j / (float)XRes, (float)ControlYSize / (float)2 + ControlYSize * (float)i / (float)YRes);
            //    }
            //} //saves and calculates the centres of each rectangle "pixel" area on the screen
            for (int i = 0; i < YRes; i++)
            {
                Pixels[i] = new RectangleF[XRes];
                for (int j = 0; j < XRes; j++)
                {
                    Pixels[i][j] = new RectangleF(new PointF((float)ControlXSize / (float)XRes * j, (float)ControlYSize / (float)YRes * i), new SizeF((float)ControlXSize / (float)XRes, (float)ControlYSize / (float)YRes));
                }
            }

            for (int i = YRes - 1; i >= 0; i--)
            {
                ScaledCoords[i] = new PointF[XRes];
                //InMandelbrot[i] = new bool[XRes];
                InMandelbrot[i] = new double[XRes];
                for (int j = 0; j < XRes; j++)
                {
                    ScaledCoords[i][j] = new PointF(TopLeftScaledCoord.X + (TopRightScaledCoord.X - TopLeftScaledCoord.X) * (float)(1 + 2 * j) / (float)(2 * XRes),
                                                    BottomLeftScaledCoord.Y + (TopLeftScaledCoord.Y - BottomLeftScaledCoord.Y) * (float)(1 + 2 * i) / (float)(2 * YRes));
                    InMandelbrot[i][j] = GetEscapeCountRatio(ScaledCoords[i][j]); //IsInMandelbrotSet(ScaledCoords[i][j]);
                }
            } //saves and calculates the centres of each rectangle "pixel" area's actual coordinates
        }

        private void PaintArea()
        {
            using (Graphics g = this.CreateGraphics())
            {
                using (SolidBrush solidBrush = new SolidBrush(Color.Black))
                {
                    for (int i = 0; i < YRes; i++)
                    {
                        for (int j = 0; j < XRes; j++)
                        {
                            //if (InMandelbrot[i][j])
                            //{
                            //    g.FillRectangle(solidBrush, Pixels[i][j]);
                            //}
                            solidBrush.Color = GetGreyscaleColor(InMandelbrot[i][j]);
                            g.FillRectangle(solidBrush, Pixels[i][j]);
                        }
                    }
                }
            }
        }
        //private void PaintArea()
        //{
        //    // Create a new graphics object outside of the parallel loop
        //    using (Graphics g = this.CreateGraphics())
        //    {
        //        Parallel.For(0, YRes, i =>
        //        {
        //            for (int j = 0; j < XRes; j++)
        //            {
        //                // Get the grayscale color for the current pixel
        //                Color color = GetGreyscaleColor(InMandelbrot[i][j]);

        //                // Fill the rectangle with the corresponding color
        //                DrawPixel(g, color, Pixels[i][j]);
        //            }
        //        });
        //    }
        //}

        //private void DrawPixel(Graphics g, Color color, RectangleF pixel)
        //{
        //    // Synchronize access to the graphics object
        //    lock (g)
        //    {
        //        using (SolidBrush solidBrush = new SolidBrush(color))
        //        {
        //            g.FillRectangle(solidBrush, pixel);
        //        }
        //    }
        //}
        private bool IsInMandelbrotSet(PointF candidatePoint)
        {
            double magnitude = Math.Sqrt(candidatePoint.X * candidatePoint.X + candidatePoint.Y * candidatePoint.Y);
            if (magnitude > 2)
            {
                return false;
            }
            PointF newPoint = new PointF(0, 0);

            for (int i = 0; i < 100; i++)
            {
                newPoint.X = newPoint.X * newPoint.X - newPoint.Y * newPoint.Y + candidatePoint.X; //X = real part of z(n) = z(n-1)*2 + c
                newPoint.Y = 2 * newPoint.X * newPoint.Y + candidatePoint.Y; //Y is imaginary part of z(n)
                magnitude = Math.Sqrt(newPoint.X * newPoint.X + newPoint.Y * newPoint.Y);
                if (magnitude > 2)
                {
                    return false;
                }
            }
            return true;
        }

        private double GetEscapeCountRatio(PointF candidatePoint)
        {
            double magnitude = Math.Sqrt(candidatePoint.X * candidatePoint.X + candidatePoint.Y * candidatePoint.Y);
            if (magnitude > 15)
            {
                return 1;
            }
            PointF newPoint = new PointF(0, 0);

            for (int i = 0; i < MaxIter; i++)
            {
                newPoint.X = newPoint.X * newPoint.X - newPoint.Y * newPoint.Y + candidatePoint.X; //X = real part of z(n) = z(n-1)*2 + c
                newPoint.Y = 2 * newPoint.X * newPoint.Y + candidatePoint.Y; //Y is imaginary part of z(n)
                magnitude = Math.Sqrt(newPoint.X * newPoint.X + newPoint.Y * newPoint.Y);
                if (magnitude > 5)
                {
                    return (double)i / (double)MaxIter;
                }
            }
            return 1;
        }

        private void Mandelbrot_Click(object sender, EventArgs e)
        {
            bkgDrawThread.Start();
            //MouseEventArgs me = (MouseEventArgs)e;
            //MessageBox.Show($"Location: {me.Location.X},{me.Location.Y}");
        }

        private void Mandelbrot_MouseMove(object sender, MouseEventArgs e)
        {
            lblMousePos.Text = $"Mouse Position: ({(float)e.X / (float)ControlXSize * (TopRightScaledCoord.X - TopLeftScaledCoord.X) + TopLeftScaledCoord.X}, {(float)(ControlYSize - e.Y) / (float)ControlYSize * (TopLeftScaledCoord.Y - BottomLeftScaledCoord.Y) + BottomLeftScaledCoord.Y})";
        }

        private Color GetGreyscaleColor(double value)
        {
            // Scale the double value to the range of 0 to 255
            int intensity = (int)(value * 255);

            // Create a grayscale color with the same intensity for each RGB component
            return Color.FromArgb(intensity, intensity, intensity);
        }

        private void btnSaveJPG_Click(object sender, EventArgs e)
        {
            //to implement
        }

    }
}
