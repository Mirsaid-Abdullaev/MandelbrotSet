using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace MandelbrotSet
{
    public partial class Mandelbrot : Form
    {
        private readonly int XRes;
        private readonly int YRes;

        private PointF TopLeftScaledCoord;
        private PointF BottomRightScaledCoord;
        private Rectangle VisualiserArea;

        private bool IsComputing;

        private bool InPanMode;
        private bool IsPanning;
        private Point StartedPanPoint;
        private Point StoppedPanPoint;
        private int panx;
        private int pany;

        private Thread? bkgDrawThread;


        private double[][] InMandelbrot;
        private PointF[][] ScaledCoords;
        private RectangleF[][] Pixels;

        private readonly int MaxIter;

        private Stack<CurrentState> ViewStack;

        public Mandelbrot(int Resolution, int MaxIter)
        {
            InitializeComponent();

            this.XRes = Resolution;
            this.YRes = (int)((double)XRes / (double)1.6); //to preserve the same 8:5 aspect ratio

            this.MaxIter = MaxIter;

            bkgDrawThread = new Thread(PaintArea) { IsBackground = true };

            InMandelbrot = new double[YRes][]; //will be an array of resolution x 5/8 resolution representing all the pixel areas of the screen

            ScaledCoords = new PointF[YRes][];

            TopLeftScaledCoord = new PointF((float)-2, (float)2);
            BottomRightScaledCoord = new PointF((float)2, (float)-2);
            VisualiserArea = new Rectangle(10, 10, 800, 600);

            Pixels = new RectangleF[YRes][];

            ViewStack = new Stack<CurrentState>();
            //initial coordinate map will be the area bounded by these coordinates, the centre of the mandelbrot set.
        }


        private void Mandelbrot_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < YRes; i++)
            {
                Pixels[i] = new RectangleF[XRes];
                for (int j = 0; j < XRes; j++)
                {
                    Pixels[i][j] = new RectangleF(new PointF((float)this.VisualiserArea.Width / (float)XRes * j, (float)this.VisualiserArea.Height / (float)YRes * i), new SizeF((float)this.VisualiserArea.Width / (float)XRes, (float)this.VisualiserArea.Width / (float)YRes));
                }
            }

            for (int i = YRes - 1; i >= 0; i--)
            {
                ScaledCoords[i] = new PointF[XRes];
                InMandelbrot[i] = new double[XRes];

                for (int j = 0; j < XRes; j++)
                {
                    ScaledCoords[i][j] = new PointF(TopLeftScaledCoord.X + (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)(1 + 2 * j) / (float)(2 * XRes),
                                                    BottomRightScaledCoord.Y + (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)(1 + 2 * i) / (float)(2 * YRes));
                    InMandelbrot[i][j] = GetSmoothEscapeCount(ScaledCoords[i][j]);
                }
            } //saves and calculates the centres of each rectangle "pixel" area's actual coordinates

            bkgDrawThread = new Thread(PaintArea) { IsBackground = true };
            bkgDrawThread.Start();
            bkgDrawThread = null;
        }

        private void PaintArea()
        {
            Bitmap bitmap = new(this.VisualiserArea.Width, this.VisualiserArea.Height);
            using Graphics formGraphics = this.CreateGraphics();
            using Graphics bmpGraphics = Graphics.FromImage(bitmap);
            using SolidBrush solidBrush = new(Color.Black);
            for (int i = 0; i < YRes; i++)
            {
                for (int j = 0; j < XRes; j++)
                {
                    solidBrush.Color = GetGreyscaleColor(InMandelbrot[i][j]);
                    bmpGraphics.FillRectangle(solidBrush, Pixels[i][j]);
                }
            }
            formGraphics.DrawImage(bitmap, this.VisualiserArea);
            if ((ViewStack.CurrentSize > 0 && bitmap != ViewStack.Peek().View) || ViewStack.CurrentSize == 0) //checking whether the drawn area is the same
            {
                AddCurrentViewToStack(bitmap, TopLeftScaledCoord, BottomRightScaledCoord);
            }
        }

        
        private double GetSmoothEscapeCount(PointF candidatePoint)
        {
            PointF newPoint = new(0, 0);

            for (int i = 1; i <= MaxIter; i++)
            {
                newPoint.X = newPoint.X * newPoint.X - newPoint.Y * newPoint.Y + candidatePoint.X; //X = real part of z(n) = z(n-1)*2 + c
                newPoint.Y = 2 * newPoint.X * newPoint.Y + candidatePoint.Y; //Y is imaginary part of z(n)
                double magnitude = Math.Sqrt(newPoint.X * newPoint.X + newPoint.Y * newPoint.Y);
                if (magnitude > 2)
                {
                    return Math.Log(i) / Math.Log(MaxIter);
                }
            }
            return 1;
        }


        private Color GetGreyscaleColor(double value)
        {
            // Scale the double value to the range of 0 to 255
            int intensity = (int)(value * 255);

            // Create a grayscale color with the same intensity for each RGB component
            return Color.FromArgb(intensity, intensity, intensity);
        }


        private void AddCurrentViewToStack(Bitmap bitmap, PointF TL, PointF BR)
        {
            CurrentState currentView = new(bitmap, TL, BR);
            ViewStack.Push(currentView);
        }










        private void Mandelbrot_MouseMove(object sender, MouseEventArgs e)
        {
            if (VisualiserArea.Contains(e.Location) && !IsComputing)
            {
                int X = e.X + 10;
                int Y = e.Y + 10;
                lblMousePos.Text = $"Mouse Position: ({(float)X / (float)this.VisualiserArea.Width * (BottomRightScaledCoord.X - TopLeftScaledCoord.X) + TopLeftScaledCoord.X}, {(float)(this.VisualiserArea.Height - Y) / (float)this.VisualiserArea.Height * (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) + BottomRightScaledCoord.Y})";
            }
        }

        private void Mandelbrot_Click(object sender, EventArgs e)
        {
            if (!IsComputing)
            {

            }
            //click event
        }

        private void btnSaveJPG_Click(object sender, EventArgs e)
        {
            if (!IsComputing && ViewStack.CurrentSize > 0)
            {
                Bitmap currentView = ViewStack.Peek().View;
                SaveFileDialog saveFileDialog = new()
                {
                    Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*",
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentView.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!IsComputing)
            {
                using Graphics g = this.CreateGraphics();
                using SolidBrush solidBrush = new(this.BackColor);
                g.FillRectangle(solidBrush, this.VisualiserArea);
            }
        }
        private void btnUndoView_Click(object sender, EventArgs e)
        {
            if (ViewStack.CurrentSize != 0)
            {
                CurrentState previousState = ViewStack.Pop();
                TopLeftScaledCoord = previousState.TopLeftScaledCoord;
                BottomRightScaledCoord = previousState.BottomRightScaledCoord;
                using Graphics g = this.CreateGraphics();
                g.DrawImage(previousState.View, this.ClientRectangle);
            }
        }
        private void btnPanMode_Click(object sender, EventArgs e)
        {
            if (InPanMode) //stop the pan mode
            {
                InPanMode = false;
                btnPanMode.BackColor = Control.DefaultBackColor;
                this.Cursor = Cursors.Default;
            }
            else
            {
                InPanMode = true;
                btnPanMode.BackColor = Color.LightYellow;
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void Mandelbrot_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsComputing && e.Button == MouseButtons.Left && InPanMode)
            {
                IsPanning = true;
                StartedPanPoint = e.Location;
            }
        }

        private void PaintSections(object parameters)
        {
            object[] paramsArray = (object[]) parameters;
            Bitmap Result = (Bitmap) paramsArray[0];
            int Direction = (int)paramsArray[1];
            Rectangle NewArea1 = (Rectangle)paramsArray[2];
            Rectangle NewArea2;
            if (Direction < 5)
            {
                NewArea2 = (Rectangle)paramsArray[3];
            }
            using Graphics g = Graphics.FromImage(Result);
            using Graphics form_g = this.CreateGraphics();
            using SolidBrush solidBrush = new(Color.Black);
            switch (Direction)
            {
                case 1: //SE
                    for (int i = YRes - (VisualiserArea.Height - pany) * YRes/VisualiserArea.Height + 1; i >= 0; i--)
                    {
                        for (int j =0; j < XRes; j++)
                        {
                            solidBrush.Color = GetGreyscaleColor(InMandelbrot[i][j]);
                            g.FillRectangle(solidBrush, Pixels[i][j]);
                        }
                    }
                    for (int i = YRes - 1; i >= YRes - (VisualiserArea.Height - pany) * YRes / VisualiserArea.Height + 1; i--)
                    {
                        for (int j = 0; j < panx/VisualiserArea.Width + 2; j++)
                        {
                            solidBrush.Color = GetGreyscaleColor(InMandelbrot[i][j]);
                            g.FillRectangle(solidBrush, Pixels[i][j]);
                        }
                    }
                    form_g.DrawImage(Result, this.VisualiserArea);
                    return;
                //case 2: //NE
                //case 3: //NW
                //case 4: //SW
                //case 5: //W
                //case 6: //E
                //case 7: //S
                //case 8: //N
            }
        }


        private void Mandelbrot_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsPanning && InPanMode && ViewStack.CurrentSize > 0) //checking if initial view loaded at least
            {
                IsPanning = false;
                IsComputing = true;
                this.Cursor = Cursors.WaitCursor;

                StoppedPanPoint = e.Location;
                panx = Math.Min(StoppedPanPoint.X - StartedPanPoint.X, VisualiserArea.Width - 1);
                pany = Math.Min(StoppedPanPoint.Y - StartedPanPoint.Y, VisualiserArea.Height - 1);

                if (panx == 0 && pany == 0) //both are 0
                {
                    IsComputing = false;
                    this.Cursor = Cursors.Default;
                    return;
                }

                double xdif = BottomRightScaledCoord.X - TopLeftScaledCoord.X;
                double ydif = TopLeftScaledCoord.Y - BottomRightScaledCoord.Y;

                if (panx > 0 && pany > 0) //SE direction
                {
                    //Rectangle NewArea1 = new Rectangle(0, 0, VisualiserArea.Width, pany);
                    //Rectangle NewArea2 = new Rectangle(0, pany, panx, VisualiserArea.Height - pany);

                    //Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    //Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X - (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)panx / VisualiserArea.Width, 
                                                    TopLeftScaledCoord.Y + (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)pany / VisualiserArea.Height);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    //Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    //new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one

                    for (int i = YRes - 1; i >= 0; i--)
                    {
                        ScaledCoords[i] = new PointF[XRes];
                        InMandelbrot[i] = new double[XRes];

                        for (int j = 0; j < XRes; j++)
                        {
                            ScaledCoords[i][j] = new PointF(TopLeftScaledCoord.X + (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)(1 + 2 * j) / (float)(2 * XRes),
                                                            BottomRightScaledCoord.Y + (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)(1 + 2 * i) / (float)(2 * YRes));
                            InMandelbrot[i][j] = GetSmoothEscapeCount(ScaledCoords[i][j]);
                        }
                    } //recalculating the centres of each "pixel" area's actual coordinates and the colours

                    bkgDrawThread = new Thread(PaintArea) { IsBackground = true };
                    bkgDrawThread.Start();

                    bkgDrawThread.Join();
                    IsComputing = false;
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (panx > 0 && pany < 0) //NE direction 
                {
                    pany = Math.Abs(pany);

                    Rectangle NewArea1 = new Rectangle(0, 0, panx, VisualiserArea.Height - pany);
                    Rectangle NewArea2 = new Rectangle(0, VisualiserArea.Height - pany, VisualiserArea.Width, pany);

                    Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X - (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)panx / VisualiserArea.Width,
                                                    TopLeftScaledCoord.Y - (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)pany / VisualiserArea.Height);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one

                }
                if (panx < 0 && pany < 0) //NW direction
                {
                    panx = Math.Abs(panx);
                    pany = Math.Abs(pany);

                    Rectangle NewArea1 = new Rectangle(VisualiserArea.X - panx, 0, panx, VisualiserArea.Height - pany);
                    Rectangle NewArea2 = new Rectangle(0, VisualiserArea.Height - pany, VisualiserArea.Width, pany);

                    Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X + (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)panx / VisualiserArea.Width,
                                                    TopLeftScaledCoord.Y - (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)pany / VisualiserArea.Height);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one


                }
                if (panx < 0 && pany > 0) //SW direction
                {
                    panx = Math.Abs(panx);

                    Rectangle NewArea1 = new Rectangle(0, 0, VisualiserArea.Width, pany);
                    Rectangle NewArea2 = new Rectangle(VisualiserArea.Width - panx, pany, panx, VisualiserArea.Height - pany);

                    Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X - (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)panx / VisualiserArea.Width,
                                                    TopLeftScaledCoord.Y - (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)pany / VisualiserArea.Height);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one

                }
                if (panx > 0 && pany == 0) //E direction
                {
                    Rectangle NewArea1 = new Rectangle(0, 0, panx, VisualiserArea.Height);

                    Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X - (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)panx / VisualiserArea.Width,
                                                    TopLeftScaledCoord.Y);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one

                }
                if (panx < 0 && pany == 0) //W direction
                {
                    Rectangle NewArea1 = new Rectangle(VisualiserArea.Width - panx, 0, panx, VisualiserArea.Height);

                    Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X + (BottomRightScaledCoord.X - TopLeftScaledCoord.X) * (float)panx / VisualiserArea.Width,
                                                    TopLeftScaledCoord.Y);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one

                }
                if (panx == 0 && pany < 0) //N direction
                {
                    Rectangle NewArea1 = new Rectangle(0, VisualiserArea.Width - pany, VisualiserArea.Width, pany);

                    Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X,
                                                    TopLeftScaledCoord.Y + (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)pany / VisualiserArea.Height);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one

                }
                if (panx == 0 && pany > 0) //S direction
                {
                    Rectangle NewArea1 = new Rectangle(0, 0, VisualiserArea.Width, pany);

                    Rectangle DestArea = new Rectangle(panx, pany, VisualiserArea.Width - panx, VisualiserArea.Height - pany);
                    Rectangle SrcArea = new Rectangle(0, 0, VisualiserArea.Width - panx, VisualiserArea.Height - pany);

                    TopLeftScaledCoord = new PointF(TopLeftScaledCoord.X,
                                                    TopLeftScaledCoord.Y - (TopLeftScaledCoord.Y - BottomRightScaledCoord.Y) * (float)pany / VisualiserArea.Height);
                    BottomRightScaledCoord = new PointF((float)(TopLeftScaledCoord.X + xdif), (float)(TopLeftScaledCoord.Y - ydif));

                    Bitmap new_view = new Bitmap(VisualiserArea.Width, VisualiserArea.Height);
                    Bitmap old_view = ViewStack.Peek().View;

                    using Graphics new_g = Graphics.FromImage(new_view);
                    new_g.DrawImage(old_view, DestArea, SrcArea, GraphicsUnit.Pixel); //copying the correct section of the old bitmap into the new one

                }
            }
        }
    }
}
