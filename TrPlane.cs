using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class TrPlane:TracerObject
    {
        Plane3 plane;

        public TrPlane(Point3 pos,Vector3 normal,Material mat)
            : base(Transform3.Identity, mat)
        {
            plane = new Plane3(pos, normal);
        }

        public override bool HitTest(Ray3 r, out double t)
        {
            double[] ints = Intersect3.RayPlaneParametric(r, plane);

            bool found = false;
            t = double.MaxValue;
            foreach (var tt in ints)
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

            h.Normal = plane.Normal;
        }
    }
}
