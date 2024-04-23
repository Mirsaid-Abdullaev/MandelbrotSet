using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using MandelbrotSet.Utils;

namespace MandelbrotSet
{
    public partial class Mandelbrot : Form
    {
        private Bitmap MandelBitmap;
        private ScaledToPixelTranslator? CoordsTranslator;
        private int MaxIter;
        private ComplexNumber TopRight;
        private ComplexNumber BottomLeft;
        private Rectangle VisualiserArea;
        private Graphics BitmapG;
        private ColourLookupTable ColourTable;
        private bool IsComputing;
        private bool InPanMode;
        private bool InZoomMode;
        private bool IsPanning;
        private bool IsZooming;
        private Point StartedPanPoint;
        private Point StoppedPanPoint;
        private int panx;
        private int pany;
        private Utils.Stack<CurrentState> ViewStack;


        public Mandelbrot(int MaxIter)
        {
            InitializeComponent();
            this.MaxIter = MaxIter;
            VisualiserArea = new Rectangle(10, 10, 800, 600);

            MandelBitmap = new Bitmap(VisualiserArea.Width, VisualiserArea.Height, PixelFormat.Format24bppRgb);
            BitmapG = Graphics.FromImage(MandelBitmap);
            BitmapG.Clear(Color.White);

            TopRight = new ComplexNumber(2, 1.5);
            BottomLeft = new ComplexNumber(-2, -1.5);
            CoordsTranslator = null;
            ColourTable = new ColourLookupTable(MaxIter);
            ViewStack = new Utils.Stack<CurrentState>();
        }


        private void Mandelbrot_Load(object sender, EventArgs e)
        {
        }

        private void GenerateNewImage()
        {
            IsComputing = true;
            BitmapG.Clear(Color.White);

            double ModSquared = 0;
            Color CurrColor;
            CoordsTranslator = new ScaledToPixelTranslator(BitmapG, BottomLeft, TopRight);
            ComplexNumber PixelStep = new ComplexNumber(1, 1);
            ComplexNumber ScaledStep = CoordsTranslator.GetScaledCoordChange(PixelStep);

            int yPixel = MandelBitmap.Height - 1;

            for (double y = BottomLeft.y; y < TopRight.y; y += ScaledStep.y)
            {
                int xPixel = 0;
                for (double x = BottomLeft.x; x < TopRight.x; x += ScaledStep.x)
                {
                    ComplexNumber candidate = new ComplexNumber(x, y);
                    ComplexNumber newPoint = new ComplexNumber(0, 0);
                    int i = 0;
                    while (ModSquared <= 4 && i < MaxIter)
                    {
                        newPoint = newPoint * newPoint + candidate;
                        ModSquared = newPoint.ModulusSquared;
                        i++;
                    }
                    ModSquared = 0;
                    if (i < MaxIter)
                    {
                        CurrColor = ColourTable.GetColor(i);
                        if (xPixel < MandelBitmap.Width && yPixel >= 0)
                        {
                            MandelBitmap.SetPixel(xPixel, yPixel, CurrColor);
                        }
                    }
                    xPixel += 1;
                }
                yPixel -= 1;
            }
            using Graphics g = this.CreateGraphics();
            g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);

            CurrentState currentState = new CurrentState((Bitmap)MandelBitmap.Clone(), TopRight, BottomLeft);
            ViewStack.Push(currentState);
            IsComputing = false;
        }


        private void Mandelbrot_MouseMove(object sender, MouseEventArgs e)
        {
            if (VisualiserArea.Contains(e.Location))
            {
                int X = e.X - 10;
                int Y = e.Y - 10;
                lblMousePos.Text = $"Mouse Position: ({(float)X / (float)this.VisualiserArea.Width * (TopRight.x - BottomLeft.x) + BottomLeft.x}, {(float)(this.VisualiserArea.Height - Y) / (float)this.VisualiserArea.Height * (TopRight.y - BottomLeft.y) + BottomLeft.y})";
            }
        }

        //private void Mandelbrot_Click(object sender, EventArgs e)
        //{
        //    if (!IsComputing)
        //    {

        //    }
        //    //click event
        //}

        private void btnSaveJPG_Click(object sender, EventArgs e)
        {
            if (!IsComputing)
            {
                SaveFileDialog saveFileDialog = new()
                {
                    Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*",
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MandelBitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!IsComputing)
            {
                BitmapG.Clear(Color.White);
                using Graphics g = this.CreateGraphics();
                g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);
            }
        }
        private void btnUndoView_Click(object sender, EventArgs e)
        {
            if (ViewStack.CurrentSize != 0)
            {
                CurrentState previousState = ViewStack.Pop();
                TopRight = previousState.TopRightScaled;
                BottomLeft = previousState.BottomLeftScaled;
                using Graphics g = this.CreateGraphics();
                g.DrawImage(previousState.ViewBmp, this.VisualiserArea);
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
                btnPanMode.BackColor = Color.Goldenrod;
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void btnZoomMode_Click(object sender, EventArgs e)
        {

            if (!IsComputing && InZoomMode)
            {
                InZoomMode = false;
                btnZoomMode.BackColor = Control.DefaultBackColor;
            }
            else if (!IsComputing && !InZoomMode)
            {
                InZoomMode = true;
                btnZoomMode.BackColor = Color.Goldenrod;
            }
        }

        private void btnDefaultView_Click(object sender, EventArgs e)
        {
            if (ViewStack.CurrentSize > 0 && ViewStack.Peek().TopRightScaled == TopRight && ViewStack.Peek().BottomLeftScaled == BottomLeft)
            {
                return;
            }
            TopRight = new ComplexNumber(2, 1.5);
            BottomLeft = new ComplexNumber(-2, -1.5);
            GenerateNewImage();
        }

        private void Mandelbrot_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsComputing && e.Button == MouseButtons.Left && InPanMode)
            {
                IsPanning = true;
                StartedPanPoint = e.Location;
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
                    this.Cursor = Cursors.SizeAll;
                    return;
                }

                double xdif = TopRight.x - BottomLeft.x;
                double ydif = TopRight.y - BottomLeft.y;

                if (panx > 0 && pany > 0) //SE direction
                {
                    TopRight = new ComplexNumber(TopRight.x - (TopRight.x - BottomLeft.x) * (float)panx / VisualiserArea.Width,
                                                    TopRight.y + (TopRight.y - BottomLeft.y) * (float)pany / VisualiserArea.Height);
                }
                if (panx > 0 && pany < 0) //NE direction 
                {
                    pany = Math.Abs(pany);

                    TopRight = new ComplexNumber(TopRight.x - (TopRight.x - BottomLeft.x) * (float)panx / VisualiserArea.Width,
                                                   TopRight.y - (TopRight.y - BottomLeft.y) * (float)pany / VisualiserArea.Height);
                }
                if (panx < 0 && pany < 0) //NW direction
                {
                    panx = Math.Abs(panx);
                    pany = Math.Abs(pany);

                    TopRight = new ComplexNumber(TopRight.x + (TopRight.x - BottomLeft.x) * (float)panx / VisualiserArea.Width,
                                                    TopRight.y - (TopRight.y - BottomLeft.y) * (float)pany / VisualiserArea.Height);
                }
                if (panx < 0 && pany > 0) //SW direction
                {
                    panx = Math.Abs(panx);

                    TopRight = new ComplexNumber(TopRight.x + (TopRight.x - BottomLeft.x) * (float)panx / VisualiserArea.Width,
                                                   TopRight.y + (TopRight.y - BottomLeft.y) * (float)pany / VisualiserArea.Height);
                }
                if (panx > 0 && pany == 0) //E direction
                {
                    TopRight = new ComplexNumber(TopRight.x + (TopRight.x - BottomLeft.x) * (float)panx / VisualiserArea.Width,
                                                   TopRight.y);
                }
                if (panx < 0 && pany == 0) //W direction
                {
                    panx = Math.Abs(panx);

                    TopRight = new ComplexNumber(TopRight.x - (TopRight.x - BottomLeft.x) * (float)panx / VisualiserArea.Width,
                                                   TopRight.y);
                }
                if (panx == 0 && pany < 0) //N direction
                {
                    pany = Math.Abs(pany);

                    TopRight = new ComplexNumber(TopRight.x,
                                                   TopRight.y - (TopRight.y - BottomLeft.y) * (float)pany / VisualiserArea.Height);
                }
                if (panx == 0 && pany > 0) //S direction
                {
                    TopRight = new ComplexNumber(TopRight.x,
                                                 TopRight.y + (TopRight.y - BottomLeft.y) * (float)pany / VisualiserArea.Height);
                }

                BottomLeft = new ComplexNumber((float)(TopRight.x - xdif), (float)(TopRight.y - ydif));


                GenerateNewImage();
                //draw the bastard

                this.Cursor = Cursors.SizeAll;
                return;
            }
        }

    }
}
