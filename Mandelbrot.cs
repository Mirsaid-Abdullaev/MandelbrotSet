using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using MandelbrotSet.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static MandelbrotSet.Utils.Design;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
        private Point StartedZoomPoint;
        private Rectangle ZoomRect;
        private Rectangle ZoomRectScreen;
        private int panx;
        private int pany;
        private Utils.Stack<CurrentState> ViewStack;
        private readonly Stopwatch stopwatch;
        private Thread? bkgDrawThread;

        public Mandelbrot(int MaxIter)
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            this.MaxIter = MaxIter;
            VisualiserArea = new Rectangle(10, 10, 800, 600);
            bkgDrawThread = new Thread(GenerateNewImage);
            MandelBitmap = new Bitmap(VisualiserArea.Width, VisualiserArea.Height, PixelFormat.Format24bppRgb);
            BitmapG = Graphics.FromImage(MandelBitmap);
            BitmapG.Clear(Color.White);

            TopRight = new ComplexNumber(2, 1.5);
            BottomLeft = new ComplexNumber(-2, -1.5);
            ColourTable = new ColourLookupTable(MaxIter);
            ViewStack = new Utils.Stack<CurrentState>();
            SetControlGradient(this, Colours6);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void GenerateNewImage()
        {
            IsComputing = true;
            this.Cursor = Cursors.WaitCursor;
            stopwatch.Restart();
            BitmapG.Clear(Color.White);

            if (ColourTable.MaxIterations != MaxIter)
            {
                ColourTable = new ColourLookupTable(MaxIter);
            } //recalculating the colour table if the max iterations variable changes from user input

            double ModSquared = 0;
            Color CurrColor;
            CoordsTranslator = new ScaledToPixelTranslator(BitmapG, BottomLeft, TopRight);
            ComplexNumber PixelStep = new ComplexNumber(1, 1);
            ComplexNumber ScaledStep = CoordsTranslator.GetScaledCoordChange(PixelStep);

            using Graphics g = this.CreateGraphics();

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
                if (yPixel % 200 == 0)
                {
                    g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);
                }
            }
            g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);
            CurrentState currentState = new CurrentState(TopRight, BottomLeft, MaxIter);
            ViewStack.Push(currentState);
            stopwatch.Stop();
            lblShowTime.Text = Math.Round(stopwatch.Elapsed.TotalSeconds, 5).ToString();
            this.Cursor = Cursors.Default;
            IsComputing = false;
        }

        private void BtnSaveJPG_Click(object sender, EventArgs e)
        {
            if (!IsComputing && ! InPanMode && !InZoomMode)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
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
        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (!IsComputing && !InPanMode && !InZoomMode)
            {
                BitmapG.Clear(Color.White);
                using Graphics g = this.CreateGraphics();
                g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);
            }
        }
        private void BtnUndoView_Click(object sender, EventArgs e)
        {
            if (ViewStack.CurrentSize > 1 && !IsComputing)
            {
                CurrentState previousState = ViewStack.Pop();
                previousState = ViewStack.Pop();
                TopRight = previousState.TopRightScaled;
                BottomLeft = previousState.BottomLeftScaled;
                MaxIter = previousState.MaxIter;
                txtIterCount.Text = MaxIter.ToString();
                bkgDrawThread = new Thread(GenerateNewImage);
                bkgDrawThread.Start();
                bkgDrawThread = null;
            }
        }
        private void BtnPanMode_Click(object sender, EventArgs e)
        {
            if (!IsComputing && InPanMode) //stop the pan mode
            {
                InPanMode = false;
                btnPanMode.BackColor = Control.DefaultBackColor;
                this.Cursor = Cursors.Default;
            }
            else if (!InZoomMode)
            {
                InPanMode = true;
                btnPanMode.BackColor = Color.Goldenrod;
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void BtnZoomMode_Click(object sender, EventArgs e)
        {

            if (!IsComputing && InZoomMode)
            {
                InZoomMode = false;
                btnZoomMode.BackColor = Control.DefaultBackColor;
            }
            else if (!InPanMode)
            {
                InZoomMode = true;
                btnZoomMode.BackColor = Color.Goldenrod;
            }
        }

        private void BtnDefaultView_Click(object sender, EventArgs e)
        {
            if (!IsComputing)
            {
                TopRight = new ComplexNumber(2, 1.5);
                BottomLeft = new ComplexNumber(-2, -1.5);

                if (!IsValidIterCount())
                {
                    txtIterCount.Text = "250";
                    MaxIter = 250;
                }
                else
                {
                    MaxIter = Convert.ToInt16(txtIterCount.Text);
                }

                if (IsComputing && ViewStack.CurrentSize > 0 && ViewStack.Peek().TopRightScaled == TopRight && ViewStack.Peek().BottomLeftScaled == BottomLeft)
                {
                    return;
                }
                bkgDrawThread = new Thread(GenerateNewImage);
                bkgDrawThread.Start();
                bkgDrawThread = null;
            }
        }

        private void Mandelbrot_MouseMove(object sender, MouseEventArgs e)
        {
            if (VisualiserArea.Contains(e.Location))
            {
                int X = e.X - 10;
                int Y = e.Y - 10;
                lblMousePos.Text = $"Mouse Position: ({(float)X / (float)this.VisualiserArea.Width * (TopRight.x - BottomLeft.x) + BottomLeft.x}, {(float)(this.VisualiserArea.Height - Y) / (float)this.VisualiserArea.Height * (TopRight.y - BottomLeft.y) + BottomLeft.y}), Aspect Ratio: {(TopRight.x - BottomLeft.x) / (TopRight.y - BottomLeft.y)}";

                if (IsPanning)
                {
                    using Graphics g = this.CreateGraphics();
                    g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);
                    g.DrawLine(new Pen(Color.PaleGoldenrod, 2F) { CustomEndCap = new AdjustableArrowCap(8, 8) { Filled = false } }, StartedPanPoint, e.Location);
                }
                if (IsZooming)
                {
                    int zoomarea_x = StartedZoomPoint.X - e.X;
                    int zoomarea_y = StartedZoomPoint.Y - e.Y;

                    using Graphics g = this.CreateGraphics();

                    if (zoomarea_x > 0 && zoomarea_y > 0) //NW direction drag
                    {
                        ZoomRect = new Rectangle(e.X - 10, StartedZoomPoint.Y - Math.Abs(zoomarea_x) * 3 / 4 - 10, Math.Abs(zoomarea_x), Math.Abs(zoomarea_x) * 3 / 4);
                    }
                    else if (zoomarea_x > 0 && zoomarea_y < 0) //SW direction drag
                    {
                        ZoomRect = new Rectangle(e.X - 10, StartedZoomPoint.Y - 10, Math.Abs(zoomarea_x), Math.Abs(zoomarea_x) * 3 / 4);
                    }
                    else if (zoomarea_x < 0 && zoomarea_y > 0) //NE direction drag
                    {
                        ZoomRect = new Rectangle(StartedZoomPoint.X - 10, StartedZoomPoint.Y - Math.Abs(zoomarea_x) * 3 / 4 - 10, Math.Abs(zoomarea_x), Math.Abs(zoomarea_x) * 3 / 4);
                    }
                    else if (zoomarea_x < 0 && zoomarea_y < 0) //SE direction drag
                    {
                        ZoomRect = new Rectangle(StartedZoomPoint.X - 10, StartedZoomPoint.Y - 10, Math.Abs(zoomarea_x), Math.Abs(zoomarea_x) * 3 / 4);
                    }
                    else
                    {
                        ZoomRect = new Rectangle();
                        return; //dodgy rectangle by user
                    }
                    ZoomRectScreen = new Rectangle(ZoomRect.X + 10, ZoomRect.Y + 10, ZoomRect.Width, ZoomRect.Height);
                    if (VisualiserArea.Contains(ZoomRectScreen))
                    {
                        g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);
                        g.DrawRectangle(new Pen(Color.AntiqueWhite, 1F), ZoomRectScreen);
                    }
                    else
                    {
                        return; //zoomrect outof bounds
                    }
                }
            }
            else
            {
                lblMousePos.Text = "Mouse Position: Out of visualiser area bounds.";
            }

        }

        private void Mandelbrot_MouseDown(object sender, MouseEventArgs e)
        {
            if (VisualiserArea.Contains(e.Location))
            {
                if (!IsComputing && e.Button == MouseButtons.Left && InPanMode)
                {
                    IsPanning = true;
                    StartedPanPoint = e.Location;
                }
                if (!IsComputing && e.Button == MouseButtons.Left && InZoomMode)
                {
                    IsZooming = true;
                    StartedZoomPoint = e.Location;
                }
            }
        }

        private void Mandelbrot_MouseUp(object sender, MouseEventArgs e)
        {
            if (VisualiserArea.Contains(e.Location))
            {
                if (e.Button == MouseButtons.Left && IsPanning && InPanMode && ViewStack.CurrentSize > 0) //checking if initial view loaded at least
                {
                    IsPanning = false;
                    IsComputing = true;

                    StoppedPanPoint = e.Location;
                    panx = Math.Min(StoppedPanPoint.X - StartedPanPoint.X, VisualiserArea.Width - 1);
                    pany = Math.Min(StoppedPanPoint.Y - StartedPanPoint.Y, VisualiserArea.Height - 1);

                    if (panx == 0 && pany == 0) //both are 0
                    {
                        IsComputing = false;
                        this.Cursor = Cursors.SizeAll;
                        return;
                    }

                    if (!IsValidIterCount())
                    {
                        txtIterCount.Text = "250";
                        MaxIter = 250;
                    }
                    else
                    {
                        MaxIter = Convert.ToInt16(txtIterCount.Text);
                    }

                    double xdif = TopRight.x - BottomLeft.x;
                    double ydif = TopRight.y - BottomLeft.y;

                    TopRight = new ComplexNumber(TopRight.x - (TopRight.x - BottomLeft.x) * (float)panx / VisualiserArea.Width,
                                                     TopRight.y + (TopRight.y - BottomLeft.y) * (float)pany / VisualiserArea.Height);
                    BottomLeft = new ComplexNumber((float)(TopRight.x - xdif), (float)(TopRight.y - ydif));

                    bkgDrawThread = new Thread(GenerateNewImage);
                    bkgDrawThread.Start();
                    bkgDrawThread = null;

                    this.Cursor = Cursors.SizeAll;
                    return;
                }
                if (e.Button == MouseButtons.Left && IsZooming && InZoomMode && ViewStack.CurrentSize > 0)
                {
                    IsZooming = false;
                    IsComputing = true;

                    if (!IsValidIterCount())
                    {
                        txtIterCount.Text = "250";
                        MaxIter = 250;
                    }
                    else
                    {
                        MaxIter = Convert.ToInt16(txtIterCount.Text);
                    }

                    Point tempTopRightPixelCoord = new Point(ZoomRect.X + ZoomRect.Width, ZoomRect.Y);
                    Point tempBottomLeftPixelCoord = new Point(ZoomRect.X, ZoomRect.Y + ZoomRect.Height);
                    ComplexNumber tempTopRight = new ComplexNumber(TopRight.x, TopRight.y);
                    TopRight = new ComplexNumber((float)tempTopRightPixelCoord.X / (float)this.VisualiserArea.Width * (TopRight.x - BottomLeft.x) + BottomLeft.x, (float)(this.VisualiserArea.Height - tempTopRightPixelCoord.Y) / (float)this.VisualiserArea.Height * (TopRight.y - BottomLeft.y) + BottomLeft.y);
                    BottomLeft = new ComplexNumber((float)tempBottomLeftPixelCoord.X / (float)this.VisualiserArea.Width * (tempTopRight.x - BottomLeft.x) + BottomLeft.x, (float)(this.VisualiserArea.Height - tempBottomLeftPixelCoord.Y) / (float)this.VisualiserArea.Height * (tempTopRight.y - BottomLeft.y) + BottomLeft.y);

                    bkgDrawThread = new Thread(GenerateNewImage);
                    bkgDrawThread.Start();
                    bkgDrawThread = null;
                }
            }

        }

        private bool IsValidIterCount()
        {
            int IterCount;
            Regex regex = new Regex("[1-9]+[0-9]*");
            if (!regex.IsMatch(txtIterCount.Text))
            {
                return false;
            }
            IterCount = Convert.ToInt16(txtIterCount.Text);
            if (IterCount > 1000 || IterCount <= 0)
            {
                return false;
            }
            return true;
        }


        private void Mandelbrot_Shown(object sender, EventArgs e)
        {
            bkgDrawThread = new Thread(GenerateNewImage);
            bkgDrawThread.Start();
            bkgDrawThread = null;
            txtIterCount.Text = MaxIter.ToString();
        }
    }
}