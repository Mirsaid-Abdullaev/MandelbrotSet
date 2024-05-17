# Mandelbrot Set Visualiser

Welcome to the Mandelbrot Set Visualiser! 

## Introduction

The Mandelbrot Set is a fascinating mathematical object that showcases the beauty of fractal geometry. It is derived from a simple iterative equation involving complex numbers. The set is defined as the collection of complex numbers \( c \) for which the iteration:
![Mandelbrot equation](https://latex.codecogs.com/png.image?\dpi{110}&space;\bg_white&space;z_{n&plus;1}&space;=&space;z_{n}^2&space;&plus;&space;c)
remains bounded as `n` approaches infinity, starting with the initial point of: ![Starting condition](https://latex.codecogs.com/png.image?\dpi{110}&space;\bg_white&space;z_{0}&space;=&space;0&space;&space;+0i) , which is the origin.

This set is explored iteratively using the above equation, along with the escape criterion:

![Escape criterion](https://latex.codecogs.com/png.image?\dpi{110}&space;\bg_white&space;|z_n|&space;\leq&space;2) , where ![Bounds on N](https://latex.codecogs.com/png.image?\dpi{100}&space;\bg_white&space;0&space;<&space;n\leq&space;MaxIterations)
## How to Setup

To use this Mandelbrot Set Visualiser, simply [download](link_to_download) or clone the repository, compile the code in a Visual Studio project or similar, and run the application. The code contains support for .NET 6.0, .NET 7.0, .NET 8.0 and .NET Core 3.1.

You'll be able to explore the Mandelbrot Set by zooming in and out, adjusting parameters, panning and saving images of views to local storage.

## Controls

**Toggle Zoom Mode** - this button when clicked enters the program's zooming mode, where the user can use their mouse to drag and select an interactive area of the visualiser area to zoom into. To exit the zoom mode, click the **Toggle Zoom Mode** button again

**Toggle Pan Mode** - this button similarly enters the program's panning mode, where the user can drag their mouse across the screen to move the image of the Mandelbrot set at that time by the magnitude of the drag and into the direction of the user drag. To exit this mode, similarly click the button again.

**Undo/Go Back** - this button takes the user back to previous view and settings, with the previous value of MaxIterations (if it was different to current) as well as the previous selection of Colour Palette if different. This only works if at least one action has been performed from the initial loaded screen view

**Clear Screen** - this button clears the visualiser area when clicked, simple as that

**Save to JPG** - this button allows the user to save the current view of the visualiser as a JPG image into their local storage, with a chosen name

**Back to Default View** - this button resets the parameters to be the same values at startup, this is essentially a reset button if the user zooms in too far or wants to get back to the original point of the Mandelbrot Set

**Palette 1, 2, 3** - these are checkboxes which select the colour palette to colour the Mandelbrot Set in. Once a new colour palette is selected, an operation of some sort must be performed to see the effect of the change, e.g. a Pan or a Zoom

**Max Iterations** - this textbox takes in optional user input for the number of iterations to use as the upper bound to check if a point is in the Mandelbrot Set or not. The higher the value, the larger the RAM usage and render time


## Features

- Interactive visualisation of the Mandelbrot Set
- Zoom functionality to explore different regions in detail
- Pan functionality to move around at a current zoom setting
- Save to Bitmap feature for the current view
- Adjustable Maximum Iterations count to alter the detail of the set
- Modular code allowing for custom colour algorithms to be added
- Render times shown on screen for each new rendering operation
- Complex coordinates of the mouse position dynamically updated/printed
## Examples

### Initialiser Loading Form
<img src="/UI/Initialiser.png"></img>

### Visualiser Initial Loaded View
<img src="/UI/Visualiser_UI.png"></img>

### Visualiser Panning Mode
<img src="/UI/Visualiser_UI_Panning.png"></img>

### Visualiser Zooming Mode
<img src="/UI/Visualiser_UI_Zooming.png"></img>

### Visualiser Zoom Result
<img src="/UI/Visualiser_UI_Zoomed.png"></img>

### Visualiser Zoom Result - Colour Palette Switch
<img src="/UI/Visualiser_UI_Zooming_ColourPalette.png"></img>

### Visualiser Saving to JPG
<img src="/UI/Visualiser_UI_Saving.png"></img>

## Contributing

Contributions are welcome! Whether you want to add new features, fix bugs, or improve documentation, feel free to submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
