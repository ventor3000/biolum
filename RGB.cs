using Coral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class RGB
    {
        public readonly double R, G, B;


       /* public static RGB Red = new RGB(0.5, 0.0, 0.0);
        public static RGB Green = new RGB(0.0, 0.5, 0.0);
        public static RGB Blue = new RGB(0.0, 0.0, 0.5);
        public static RGB Yellow = new RGB(0.5, 0.5, 0.0);
        public static RGB Magenta = new RGB(0.5, 0.0, 0.5);
        public static RGB Cyan = new RGB(0.0, 0.5, 0.5);
        public static RGB White = new RGB(1.0, 1.0, 1.0);
        public static RGB Black = new RGB(0.0, 0.0, 0.0);
        public static RGB Gray = new RGB(0.5, 0.5, 0.5);*/


        public static RGB Red = new RGB(1.0, 0.0, 0.0);
        public static RGB Green = new RGB(0.0, 1.0, 0.0);
        public static RGB Blue = new RGB(0.0, 0.0, 1.0);
        public static RGB Yellow = new RGB(1.0, 1.0, 0.0);
        public static RGB Magenta = new RGB(1.0, 0.0, 1.0);
        public static RGB Cyan = new RGB(0.0, 1.0, 1.0);
        public static RGB White = new RGB(1.0, 1.0, 1.0);
        public static RGB Black = new RGB(0.0, 0.0, 0.0);
        public static RGB Gray = new RGB(0.5, 0.5, 0.5);

        public RGB(double r,double g,double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static RGB operator *(RGB rgb,double s) {
            return new RGB(rgb.R*s,rgb.G*s,rgb.B*s);
        }

        public static RGB operator +(RGB rgb, double s)
        {
            return new RGB(
                rgb.R + s,
                rgb.G + s,
                rgb.B + s
            );
        }

        public static RGB operator +(RGB rgb1, RGB rgb2)
        {
            return new RGB(
                rgb1.R + rgb2.R,
                rgb1.G + rgb2.G,
                rgb1.B + rgb2.B
                );
        }

        public int ToInt()
        {
            //apply exposure to avoid out of limit colors
            double exposure = -1.00f;
            double blue = 1.0f - Math.Exp(B * exposure);
            double red = 1.0f - Math.Exp(R * exposure);
            double green = 1.0f - Math.Exp(G * exposure);

            int ir = (int)(red * 255.0);
            int ig = (int)(green * 255.0);
            int ib = (int)(blue * 255.0);
            return (ir << 16) | (ig << 8) | ib;
        }

        
    }
}
