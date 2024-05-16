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
        private readonly Bitmap MandelBitmap; //the bitmap holding the bitmap object of the current view
        private Rectangle VisualiserArea; //the screen rectangle which bounds the drawing area of the form
        private readonly Graphics BitmapG; //graphics object used to draw the mandelbrot bitmap MandelBitmap
        private ScaledToPixelTranslator CoordsTranslator; //the utility class that converts from screen pixel coordinates to complex coordinates
        private ColourLookupTable ColourTable; //the utility class that holds all the precomputed colours mapped to each possible iteration count
        private readonly Utils.Stack<CurrentState> ViewStack; //the stack that holds the parameters of the previous view
        private readonly Stopwatch stopwatch; //for timing each drawing operation
        private Thread bkgDrawThread; //the thread used to draw the mandelbrot set on to allow the main GUI to be responsive while drawing taking place

        private int MaxIter; //maximum iterations for the current view
        private int ColorPalette; //int for which colour palette is currently being used

        private ComplexNumber TopRight; // complex coordinate of the top right of the visualiser area for the current view
        private ComplexNumber BottomLeft; //same as above, but for the bottom left

        private bool IsComputing; //flag to check if currently in the process of zooming/panning/drawing
        private bool InPanMode; //flag to check if pan mode button active currently
        private bool InZoomMode; //flag to check if zoom mode button currently active
        private bool IsPanning; //flag that is true while the user has not released the mouse after clicking and holding within the visualiser area bounds
        private bool IsZooming; //same as above flag, but when in zoom mode and dragging the mouse without releasing the button

        private PointF StartedPanPoint; //point to store the initial starting position for the panning operation
        private PointF StoppedPanPoint; //same as above, but storing the point of release of the mouse in the panning mode
        private float PanX; //difference of the x coordinates of the start and stop pan screen pixel values
        private float PanY; //difference of the y coordinates of the start and stop pan screen pixel values

        private PointF StartedZoomPoint; //same as for the zoom points but storing where the zoom area selection started
        private Rectangle ZoomRect; //gets a rectangle with the correct aspect ratio size based on the user's drag selected area
        private Rectangle ZoomRectScreen; //uses the zoomrect to get the correct area to draw the zoom rectangle selection dynamically on the screen

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
            CoordsTranslator = new ScaledToPixelTranslator(BitmapG, BottomLeft, TopRight);
            ColourTable = new ColourLookupTable(MaxIter);
            ViewStack = new Utils.Stack<CurrentState>();
            SetControlGradient(this, Colours6);
            ColorPalette = 1;
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
            }

            double ModSquared = 0;
            Color CurrColor;
            CoordsTranslator = new ScaledToPixelTranslator(BitmapG, BottomLeft, TopRight);
            ComplexNumber PixelStep = new ComplexNumber(1, 1);
            ComplexNumber ScaledStep = CoordsTranslator.GetScaledCoordChange(PixelStep);

            using Graphics g = this.CreateGraphics();

            int RowPixel = MandelBitmap.Height - 1;
            for (double y = BottomLeft.y; y < TopRight.y; y += ScaledStep.y)
            {
                int ColumnPixel = 0;
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
                        CurrColor = ColourTable.GetColor(i, ColorPalette);
                        if (ColumnPixel < MandelBitmap.Width && RowPixel >= 0)
                        {
                            MandelBitmap.SetPixel(ColumnPixel, RowPixel, CurrColor);
                        }
                    }
                    ColumnPixel += 1;
                }
                RowPixel -= 1;
                if (RowPixel % 225 == 0)
                {
                    g.DrawImage(MandelBitmap, VisualiserArea.Location);
                }
            }
            g.DrawImage(MandelBitmap, VisualiserArea.Location);
            stopwatch.Stop();
            lblShowTime.Text = Math.Round(stopwatch.Elapsed.TotalSeconds, 5).ToString();
            this.Cursor = Cursors.Default;
            IsComputing = false;
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
        private void BtnSaveJPG_Click(object sender, EventArgs e)
        {
            if (!IsComputing && !InPanMode && !InZoomMode)
            {
                btnSaveJPG.BackColor = Color.Goldenrod;
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*",
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MandelBitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                }
                btnSaveJPG.BackColor = Color.DodgerBlue;
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (!IsComputing && !InPanMode && !InZoomMode)
            {
                btnClear.BackColor = Color.Goldenrod;
                BitmapG.Clear(Color.White);
                using Graphics g = this.CreateGraphics();
                g.DrawImage(MandelBitmap, 10, 10, MandelBitmap.Width, MandelBitmap.Height);
                btnClear.BackColor = Color.DodgerBlue;
            }
        }
        private void BtnUndoView_Click(object sender, EventArgs e)
        {
            if (ViewStack.CurrentSize > 1 && !IsComputing)
            {
                btnUndoView.BackColor = Color.Goldenrod;
                CurrentState previousState = ViewStack.Pop();
                TopRight = previousState.TopRightScaled;
                BottomLeft = previousState.BottomLeftScaled;
                MaxIter = previousState.MaxIter;
                txtIterCount.Text = MaxIter.ToString();
                bkgDrawThread = new Thread(GenerateNewImage);
                bkgDrawThread.Start();
                bkgDrawThread = null;
                lblAspectRatio.Text = $"Aspect Ratio: {(TopRight.x - BottomLeft.x) / (TopRight.y - BottomLeft.y)}";
                btnUndoView.BackColor = Color.DodgerBlue;
            }
        }
        private void BtnPanMode_Click(object sender, EventArgs e)
        {
            if (!IsComputing && InPanMode) //stop the pan mode
            {
                InPanMode = false;
                btnPanMode.BackColor = Color.DodgerBlue;
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
                btnZoomMode.BackColor = Color.DodgerBlue;
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
                btnDefaultView.BackColor = Color.Goldenrod;
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
                lblAspectRatio.Text = $"Aspect Ratio: {(TopRight.x - BottomLeft.x) / (TopRight.y - BottomLeft.y)}";
                btnDefaultView.BackColor = Color.DodgerBlue;
            }
        }
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsComputing)
            {
                if (checkBox1.Checked)
                {
                    ColorPalette = 1;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                }
            }
            else
            {
                checkBox1.Checked = false;
            }
        }
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsComputing)
            {
                if (checkBox2.Checked)
                {
                    ColorPalette = 2;
                    checkBox1.Checked = false;
                    checkBox3.Checked = false;
                }
            }
            else
            {
                checkBox2.Checked = false;
            }
        }
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsComputing)
            {
                if (checkBox3.Checked)
                {
                    ColorPalette = 3;
                    checkBox2.Checked = false;
                    checkBox1.Checked = false;
                }
            }
            else
            {
                checkBox3.Checked = false;
            }
        }
        private void Mandelbrot_MouseMove(object sender, MouseEventArgs e)
        {
            if (VisualiserArea.Contains(e.Location))
            {
                int X = e.X - 10;
                int Y = e.Y - 10;
                lblMousePos.Text = $"Mouse Position: ({(float)X / (float)this.VisualiserArea.Width * (TopRight.x - BottomLeft.x) + BottomLeft.x}, {(float)(this.VisualiserArea.Height - Y) / (float)this.VisualiserArea.Height * (TopRight.y - BottomLeft.y) + BottomLeft.y})";

                if (IsPanning)
                {
                    using Graphics g = this.CreateGraphics();
                    g.DrawImage(MandelBitmap, VisualiserArea.Location);
                    g.DrawLine(new Pen(Color.PaleGoldenrod, 2F) { CustomEndCap = new AdjustableArrowCap(8, 8) { Filled = false } }, StartedPanPoint, e.Location);
                }
                if (IsZooming)
                {
                    float zoomarea_x = StartedZoomPoint.X - e.X;
                    float zoomarea_y = StartedZoomPoint.Y - e.Y;

                    using Graphics g = this.CreateGraphics();

                    if (zoomarea_x > 0 && zoomarea_y > 0) //NW direction drag
                    {
                        ZoomRect = new Rectangle(e.X - 10, (int)(StartedZoomPoint.Y - Math.Abs(zoomarea_x) * 3 / 4 - 10), (int)Math.Abs(zoomarea_x), (int)(Math.Abs(zoomarea_x) * 3 / 4));
                    }
                    else if (zoomarea_x > 0 && zoomarea_y < 0) //SW direction drag
                    {
                        ZoomRect = new Rectangle(e.X - 10, (int)(StartedZoomPoint.Y - 10), (int)Math.Abs(zoomarea_x), (int)(Math.Abs(zoomarea_x) * 3 / 4));
                    }
                    else if (zoomarea_x < 0 && zoomarea_y > 0) //NE direction drag
                    {
                        ZoomRect = new Rectangle((int)(StartedZoomPoint.X - 10), (int)(StartedZoomPoint.Y - Math.Abs(zoomarea_x) * 3 / 4 - 10), (int)Math.Abs(zoomarea_x), (int)(Math.Abs(zoomarea_x) * 3 / 4));
                    }
                    else if (zoomarea_x < 0 && zoomarea_y < 0) //SE direction drag
                    {
                        ZoomRect = new Rectangle((int)(StartedZoomPoint.X - 10), (int)(StartedZoomPoint.Y - 10), (int)Math.Abs(zoomarea_x), (int)(Math.Abs(zoomarea_x) * 3 / 4));
                    }
                    else
                    {
                        ZoomRect = new Rectangle();
                        return; //dodgy rectangle by user
                    }
                    ZoomRectScreen = new Rectangle(ZoomRect.X + 10, ZoomRect.Y + 10, ZoomRect.Width, ZoomRect.Height);
                    if (VisualiserArea.Contains(ZoomRectScreen))
                    {
                        g.DrawImage(MandelBitmap, VisualiserArea.Location);
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
                    PanX = (int)Math.Min(StoppedPanPoint.X - StartedPanPoint.X, VisualiserArea.Width - 1);
                    PanY = Math.Min(StoppedPanPoint.Y - StartedPanPoint.Y, VisualiserArea.Height - 1);

                    if (PanX == 0 && PanY == 0) //both are 0
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

                    CurrentState currentState = new CurrentState(TopRight, BottomLeft, MaxIter);
                    ViewStack.Push(currentState);

                    TopRight = new ComplexNumber(TopRight.x - (TopRight.x - BottomLeft.x) * (float)PanX / VisualiserArea.Width,
                                                     TopRight.y + (TopRight.y - BottomLeft.y) * (float)PanY / VisualiserArea.Height);
                    BottomLeft = new ComplexNumber((float)(TopRight.x - xdif), (float)(TopRight.y - ydif));

                    bkgDrawThread = new Thread(GenerateNewImage);
                    bkgDrawThread.Start();
                    bkgDrawThread = null;
                    lblAspectRatio.Text = $"Aspect Ratio: {(TopRight.x - BottomLeft.x) / (TopRight.y - BottomLeft.y)}";
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

                    CurrentState currentState = new CurrentState(TopRight, BottomLeft, MaxIter);
                    ViewStack.Push(currentState);

                    TopRight = new ComplexNumber((float)tempTopRightPixelCoord.X / (float)this.VisualiserArea.Width * (TopRight.x - BottomLeft.x) + BottomLeft.x, (float)(this.VisualiserArea.Height - tempTopRightPixelCoord.Y) / (float)this.VisualiserArea.Height * (TopRight.y - BottomLeft.y) + BottomLeft.y);
                    BottomLeft = new ComplexNumber((float)tempBottomLeftPixelCoord.X / (float)this.VisualiserArea.Width * (tempTopRight.x - BottomLeft.x) + BottomLeft.x, (float)(this.VisualiserArea.Height - tempBottomLeftPixelCoord.Y) / (float)this.VisualiserArea.Height * (tempTopRight.y - BottomLeft.y) + BottomLeft.y);

                    bkgDrawThread = new Thread(GenerateNewImage);
                    bkgDrawThread.Start();
                    bkgDrawThread = null;
                    lblAspectRatio.Text = $"Aspect Ratio: {(TopRight.x - BottomLeft.x) / (TopRight.y - BottomLeft.y)}";
                }
            }
        }
        private void Mandelbrot_Paint(object sender, PaintEventArgs e)
        {
            if (!IsComputing & !IsPanning & !IsZooming)
            {
                using Graphics g = this.CreateGraphics();
                g.DrawImage(MandelBitmap, VisualiserArea.Location);
            }
        }
        private void Mandelbrot_Shown(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            CurrentState currentState = new CurrentState(TopRight, BottomLeft, MaxIter);
            ViewStack.Push(currentState);
            bkgDrawThread = new Thread(GenerateNewImage);
            bkgDrawThread.Start();
            bkgDrawThread = null;
            txtIterCount.Text = MaxIter.ToString();
            lblAspectRatio.Text = $"Aspect Ratio: {(TopRight.x - BottomLeft.x) / (TopRight.y - BottomLeft.y)}";
        }
    }
}