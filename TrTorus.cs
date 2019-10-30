using Coral;
using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class TrTorus:TracerObject
    {
        Point3 center;
        private double R; //central radius
        private double r; //tube radius


        public TrTorus(Point3 center, double majrad, double tuberad,Material mat):base(Transform3.Scale(majrad),mat)
        {
            this.R = 1.0;
            this.r = tuberad/majrad;
            this.center = center;
        }

        public override bool HitTest(Ray3 ray, out double t)
        {
            ray=ray.Transform(InvPosition);

            double x0 = ray.Position.X;
            double y0 = ray.Position.Y;
            double z0 = ray.Position.Z;
            double dx = ray.Direction.X;
            double dy = ray.Direction.Y;
            double dz = ray.Direction.Z;


            double A = x0 * x0 + y0 * y0 + z0 * z0;
            double B = 2 * (x0 * dx + y0 * dy + z0 * dz);
            double R2 = R * R;
            double r2 = r * r;

            double t4 = A * A;
            double t3 = 2 * A * B;
            double t2 = 2 * A * R2 + B * B + 2 * A * A - 2 * r2 * A;
            double t1 = 2 * B * (R2 + A - r2);
            double t0 = (2 * A - 2 * r2) * R2 + A * A - 2 * r2 * A + r2 * r2;


            t2 -= 4 * R2 * (dx * dx + dy * dy);
            t1 -= 8 * R2 * (dx * x0 + dy * y0);
            t0 -= 4 * R2 * (x0 * x0 + y0 * y0);

            

            RealPolynomial rp = new RealPolynomial(t4, t3, 2, t1, t0);
            //rp.Normalize();
            double[] roots = rp.FindRoots();

            /*

            Vector3 p = ray.Position.ToVector();
            Vector3 d = ray.Direction;

            double alpha = d.Dot(d);
            double beta = 2.0 * p.Dot(d);
            double gamma = p.Dot(p) - tubeRadius*tubeRadius - centralRadius*centralRadius;

            // quatric coefficients
            double a4 = alpha*alpha;
            double a3 = 2.0 * alpha * beta;
            double a2 = beta*beta + (2.0 * alpha * gamma) + (4.0 * centralRadius*centralRadius * d.Z*d.Z);
            double a1 = (2.0 * beta * gamma) + (8 * centralRadius*centralRadius * p.Z * d.Z);
            double a0 = gamma*gamma + (4.0 * centralRadius*centralRadius * p.Z*p.Z) - (4.0 * centralRadius*centralRadius * tubeRadius*tubeRadius);

            // solve polynomial
            double[] roots = RealPolynomial.SolveQuartic(a4, a3, a2, a1, a0);
            */
            bool found = false;
            t = double.MaxValue;
            foreach (var tt in roots)
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

            Point3 p = h.Position;

            double innerComponent = p.X * p.X + p.Y * p.Y + p.Z * p.Z - r * r - R * R;

            
            Vector3 norm=new Vector3(
                4.0 * p.X * innerComponent,
                4.0 * p.Y * innerComponent,
                4.0 * p.Z * innerComponent + (8.0 * R*R * p.Z*p.Z));

            h.Normal = norm.Normalized;
        }
    }
}
