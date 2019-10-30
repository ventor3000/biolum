using Coral;
using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class TrSphere:TracerObject
    {

        double rad;
        
        public TrSphere(Point3 center, double rad,Material mat):base(Transform3.Translate(center.X, center.Y, center.Z),mat)
        {
            this.rad = rad;
        }

        public override bool HitTest(Ray3 ray,out double t)
        {

            ray = ray.Transform(InvPosition);

            double dx = ray.Direction.X;
            double dy = ray.Direction.Y;
            double dz = ray.Direction.Z;
            double x0 = ray.Position.X;
            double y0 = ray.Position.Y;
            double z0 = ray.Position.Z;

            double a = 1.0; //assume ray direction is normalized   dx * dx + dy * dy + dz * dz;
            double b = 2.0 * (x0 * dx + y0 * dy + z0 * dz);
            double c = x0 * x0 + y0 * y0 + z0 * z0-rad*rad;

            bool found = false;
            t = double.MaxValue;
            foreach (var tt in RealPolynomial.SolveQuadric(a, b, c))
            {
                if (tt > 1e-4 && tt < t)
                {
                    t = tt;
                    found = true;
                }
            }

            return found;
        }


        public override void ComputeHitData(HitData h)
        {
            //get hit position in local coordinate system
            Point3 pos = h.Position.Transform(InvPosition);
            h.Normal = pos.ToVector().Normalized;
        }
    }
}
