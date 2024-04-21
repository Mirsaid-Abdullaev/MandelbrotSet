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
            //int Resolution;
            //int MaxIter;
            ApplicationConfiguration.Initialize();

            //InitialiseVisualiser initialiseVisualiser = new InitialiseVisualiser();
            //Application.Run(initialiseVisualiser);

            //try
            //{
            //    Resolution = initialiseVisualiser.Resolution;
            //    MaxIter = initialiseVisualiser.MaxIter;
            //}
            //catch
            //{
            //    Resolution = 200;
            //    MaxIter = 250
            //}
            //initialiseVisualiser.Dispose();

            Mandelbrot mandelbrotViewer = new Mandelbrot(1000, 200);
            Application.Run(mandelbrotViewer);

          
        }
    }
}