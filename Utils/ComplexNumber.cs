using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace MandelbrotSet.Utils
{
    internal class ComplexNumber //a utility class containing common operations with complex numbers
    {
        // z = x + iy
        public double x; //real component
        public double y; //imaginary component

        public ComplexNumber() //initialises a complex number as the real origin
        {
            this.x = 0;
            this.y = 0;
        }

        public ComplexNumber(double x, double y) //initialises a complex number with passed parameters
        {
            this.x = x;
            this.y = y;
        }

        public double Modulus 
        {
            get
            {
                return Math.Sqrt(x * x + y * y); // abs(z) = sqrt(x^2 + y^2), distance of complex number from origin
            }
        }
        public double ModulusSquared
        {
            get
            {
                return x * x + y * y; //same as above but not sqrt'd
            }
        }

        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2) //overload for addition of two complex numbers
        {
            ComplexNumber z3 = new ComplexNumber()
            {
                x = z1.x + z2.x,
                y = z1.y + z2.y
            };
            return z3; //sum of z1 and z2
        }

        public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2) //overload for subtraction of two complex numbers
        {
            ComplexNumber z3 = new ComplexNumber()
            {
                x = z1.x - z2.x,
                y = z1.y - z2.y
            };
            return z3; //z1 - z2
        }

        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2) //multiplication of two complex numbers
        {
            //z3 = (x + iy)(a + ib) - performs the necessary expansion and multiplication of complex numbers
            ComplexNumber z3 = new ComplexNumber()
            {
                x = z1.x * z2.x - z1.y * z2.y,
                y = z1.x * z2.y + z1.y * z2.x
            };
            return z3;
        }

        public static ComplexNumber operator /(ComplexNumber z1, ComplexNumber z2) //complex number division
        {
            // z3 = (x + iy) / (a + ib)
            ComplexNumber z3 = new ComplexNumber()
            {
                x = (z1.x * z2.x + z1.y * z2.y) / (z2.x * z2.x + z2.y * z2.y),
                y = (z1.y * z2.x - z1.x * z2.y) / (z2.x * z2.x + z2.y * z2.y)
            };
            return z3;
        }
        public static bool operator ==(ComplexNumber z1, ComplexNumber z2) //checks if two complex numbers are equal
        {
            return z1.x == z2.x && z1.y == z2.y;
        }
        public static bool operator !=(ComplexNumber z1, ComplexNumber z2) //check if two complex numbers arent equal
        {
            return !(z1.x == z2.x && z1.y == z2.y);
        }

    }
}
