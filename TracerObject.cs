using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public abstract class TracerObject
    {
        public Transform3 Position;
        public Transform3 InvPosition;
        public Material Material;
        
        public TracerObject(Transform3 pos, Material mat)
        {
            this.Position = pos;
            this.Material = mat;
            this.InvPosition = Position.Inverse;
        }


        public abstract bool HitTest(Ray3 r,out double t);
        public abstract void ComputeHitData(HitData h);

    }
}
