using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turtle;

namespace BioLum
{
    public class World
    {
        public double AmbientLight = 0.2;

        
        List<TracerObject> objects = new List<TracerObject>();


        public void Add(TracerObject obj)
        {
            objects.Add(obj);
        }

        public void Trace(Camera cam,Painter p)
        {
            

          /*  for (int y = 300; y < cam.Height; y++)
            {
                for (int x = 462; x < cam.Width; x++)
                {
*/
            for (int y = 0; y < cam.Height; y++)
            {
                for (int x = 0; x < cam.Width; x++)
                {

          
                    
                    Ray3 ray=cam.GetRay(x, y);
                    RGB col = GetColor(ray, 0);

                    p.SetPixel(462, 300, Colors.Wheat);

                    if (col!=null)
                    {
                        p.SetPixel(x, y, col.ToInt());
                    }
                    else
                    {
                        p.SetPixel(x, y, Colors.Black);
                    }
                }
            }
        }

        public RGB GetColor(Ray3 ray, int level)
        {
            HitData hd = ShootRay(ray);

            if (hd.HasHit)
            {
                return hd.HitObject.Material.GetColor(this, hd,level);
            }
            else
            {
                return null;
            }
        }



        public HitData ShootRay(Ray3 ray)
        {
            HitData h = new HitData(ray);

            foreach (TracerObject obj in objects)
                h.TryBetterHit(obj);
            

            if (h.HasHit)
            {
                h.Position = ray.Eval(h.HitT);
                h.HitObject.ComputeHitData(h);
            }

            return h;
        }
    }
}
