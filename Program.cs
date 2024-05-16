using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace MandelbrotSet
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if NET6_0_OR_GREATER
            ApplicationConfiguration.Initialize();
#endif
            InitialiseVisualiser initialiseVisualiser = new InitialiseVisualiser();
            Application.Run(initialiseVisualiser);
            if (initialiseVisualiser.UserClosed)
            {
                initialiseVisualiser.Dispose();
                return;
            }
            else
            {
                Mandelbrot mandelbrotViewer = new Mandelbrot(250);
                Application.Run(mandelbrotViewer);
            }
            return;
        }
    }
}