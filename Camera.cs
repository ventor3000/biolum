using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class Camera
    {

        Transform3 camtrans;
        Point3 campos;

        double tan_fovx;
        double tan_fovy;
        public readonly double Width;
        public readonly double Height;

        
        public Camera(Point3 pos,Point3 lookat,Vector3 up,int width, int height,double fov=45.0)
        {

            this.campos = pos;

            Vector3 lookdir = (lookat - pos).Normalized;
            Vector3 camside = up.Normalized.Cross(lookdir);
            Vector3 camup = camside.Cross(lookdir);
            camtrans = new Transform3(camside, camup, lookdir);

            this.Width = width;
            this.Height = height;
            double fovx = Coral.MathUtil.DegToRad(fov);
            double fovy = ((double)height / (double)width) * fovx;
            tan_fovx = Math.Tan(fovx);
            tan_fovy = -Math.Tan(fovy);
        }

        
        public Ray3 GetRay(double pixx,double pixy)
        {
            double x = ((2.0 * pixx - Width) / Width) * tan_fovx;
            double y = ((2.0 * pixy - Height) / Height) * tan_fovy;

            Vector3 raydir = new Vector3(x, y, 1);
            raydir = raydir.Transform(camtrans);
            return new Ray3(campos,raydir.Normalized);
        }

    }
}
