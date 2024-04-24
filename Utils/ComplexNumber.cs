using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace MandelbrotSet.Utils
{
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    internal class ComplexNumber
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    {
        public double x;
        public double y;

        public ComplexNumber()
        {
            this.x = 0;
            this.y = 0;
        }

        public ComplexNumber(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double Modulus
        {
            get
            {
                return Math.Sqrt(x * x + y * y);
            }
        }
        public double ModulusSquared
        {
            get
            {
                return x * x + y * y;
            }
        }

        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber z3 = new ComplexNumber()
            {
                x = z1.x + z2.x,
                y = z1.y + z2.y
            };
            return z3;
        }

        public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber z3 = new ComplexNumber()
            {
                x = z1.x - z2.x,
                y = z1.y - z2.y
            };
            return z3;
        }

        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber z3 = new ComplexNumber()
            {
                x = z1.x * z2.x - z1.y * z2.y,
                y = z1.x * z2.y + z1.y * z2.x
            };
            return z3;
        }

        public static ComplexNumber operator /(ComplexNumber z1, ComplexNumber z2)
        {
            ComplexNumber z3 = new ComplexNumber()
            {
                x = (z1.x * z2.x + z1.y * z2.y) / (z2.x * z2.x + z2.y * z2.y),
                y = (z1.y * z2.x - z1.x * z2.y) / (z2.x * z2.x + z2.y * z2.y)
            };
            return z3;
        }
        public static bool operator ==(ComplexNumber z1, ComplexNumber z2)
        {
            return z1.x == z2.x && z1.y == z2.y;
        }
        public static bool operator !=(ComplexNumber z1, ComplexNumber z2)
        {
            return !(z1.x == z2.x && z1.y == z2.y);
        }

    }
}
