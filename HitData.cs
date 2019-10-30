using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class HitData
    {
        //Computed by tracer before a final hit is found:
        public bool HasHit=false;
        public Point3 Position;
        public double HitT=double.MaxValue;
        public TracerObject HitObject;
        public Ray3 Ray;    //ray used to find this hit

        //filled in by tracer object when a final hit is found:
        public Vector3 Normal;

        
        public HitData(Ray3 ray)
        {
            this.Ray = ray;
        }

        public void Reset()
        {
            HasHit = false;
            Position = null;
            Normal = null;
            HitObject = null;
            HitT = double.MaxValue;
        }

        public bool TryBetterHit(TracerObject obj)
        {
            double t;
            if (obj.HitTest(Ray, out t))
            {
                if (t > 1e-4 && t < HitT)
                {
                    HasHit = true;
                    HitT = t;
                    HitObject = obj;
                    return true;
                }
            }
            
            return false;
        }
    }
}
