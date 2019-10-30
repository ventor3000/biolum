using Coral;
using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class TrTangleCube:TracerObject
    {
        //x^4+5x^2+y^4-5y^2+z^4-5z^2+11.8=0

        public TrTangleCube(Point3 center, double size,Material mat):base(Transform3.Scale(100.0),mat)
        {
            
        }


        public override bool HitTest(Ray3 ray, out double t)
        {

            ray = ray.Transform(InvPosition);

            double dx = ray.Direction.X;
            double dy = ray.Direction.Y;
            double dz = ray.Direction.Z;
            double x0 = ray.Position.X;
            double y0 = ray.Position.Y;
            double z0 = ray.Position.Z;

            double a = dx * dx * dx * dx + dy * dy * dy * dy + dz * dz * dz * dz;
            double b = 4.0 * dx * dx * dx * x0 + 4.0 * dy * dy * dy * y0 + 4.0 * dz * dz * dz;
            double c = 6.0 * dx * dx * x0 * x0 - 5.0 * dx * dx + 6.0 * dy * dy * y0 * y0 - 5.0 * dy * dy + 6.0 * dz * dz * z0 * z0 - 5.0 * dz * dz;
            double d = 4.0 * dx * x0 * x0 * x0 - 10.0 * dx * x0 + 4.0 * dy * y0 * y0 * y0 - 10.0 * dy * y0 + 4.0 * dz * z0 * z0 * z0;
            double e = x0 * x0 * x0 * x0 - 5.0 * x0 * x0 + y0 * y0 * y0 * y0 - 5.0 * y0 * y0 + z0 * z0 * z0 * z0 - 5.0 * z0 * z0 + 11.8;

            RealPolynomial poly = new RealPolynomial(a, b, c, d, e);
            //poly /= new RealPolynomial(100000);
            //poly.Normalize();

            double ev=poly.Eval(1.0);

            double[] cof=poly.FindRoots(MathUtil.Epsilon,false);
            //double[] cof=RealPolynomial.SolveQuartic(a, b, c, d, e);

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
            h.Normal = new Vector3(1, 1, -1).Normalized;
        }
    }
}
