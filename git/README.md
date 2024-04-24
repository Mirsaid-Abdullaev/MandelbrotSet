# Mandelbrot Set Visualiser

Welcome to the Mandelbrot Set Visualiser! 

## Introduction

The Mandelbrot Set is a fascinating mathematical object that showcases the beauty of fractal geometry. It is derived from a simple iterative equation involving complex numbers. The set is defined as the collection of complex numbers \( c \) for which the iteration \( z_{n+1} = z_{n}^2 + c \) remains bounded as \( n \) approaches infinity, starting with \( z_0 = 0 \).

In mathematical terms, this can be represented as:

![Mandelbrot equation](https://latex.codecogs.com/png.image?\dpi{120}&space;\bg_white&space;z_{n&plus;1}&space;=&space;z_{n}^2&space;&plus;&space;c)

This set is explored iteratively using the above equation, along with the escape criterion:

![Escape criterion](https://latex.codecogs.com/png.image?\dpi{120}&space;\bg_white&space;|z_n|&space;\leq&space;2)

## How to Use

To use this Mandelbrot Set Visualiser, simply [download](link_to_download) or clone the repository, compile the code, and run the application. The code contains support for .NET 6.0, .NET 7.0, .NET 8.0 and .NET Core 3.1.

You'll be able to explore the Mandelbrot Set by zooming in and out, adjusting parameters, panning and saving images of views to local storage.

## Features

- Interactive visualisation of the Mandelbrot Set
- Zoom functionality to explore different regions in detail
- Pan functionality to move around at a current zoom setting
- Save to Bitmap feature for the current view
- Adjustable Maximum Iterations count to alter the detail of the colours
- Modular code allowing for custom colour algorithms to be added

## Examples

(to follow)

## Contributing

Contributions are welcome! Whether you want to add new features, fix bugs, or improve documentation, feel free to submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).