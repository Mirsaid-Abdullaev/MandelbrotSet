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
            ApplicationConfiguration.Initialize();

            InitialiseVisualiser initialiseVisualiser = new();
            Application.Run(initialiseVisualiser);
            if (initialiseVisualiser.UserClosed)
            {
                initialiseVisualiser.Dispose();
            }
            else
            {
                Mandelbrot mandelbrotViewer = new(250);
                Application.Run(mandelbrotViewer);
            }
        }
    }
}