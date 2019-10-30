using Coral.Geom3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public class Material
    {
        public Texture Ambient=TextureSolidColor.Red;
        public Texture Diffuse = TextureSolidColor.Red;
        public Texture Specular = TextureSolidColor.White;
        public double Reflection = 0.2;
        public double SpecularFactor = 50.0;


        Vector3 testlight = new Vector3(1.5, 1.5, -1).Normalized;


        public Material(Texture ambient,Texture diffuse,Texture specular,double reflection=0.0)
        {
            this.Ambient = ambient;
            this.Diffuse = diffuse;
            this.Specular = specular;
            this.Reflection=reflection;
        }


        public static int maxdepth = 0;


        public RGB GetColor(World world,HitData hd,int level)
        {

            maxdepth = Math.Max(level, maxdepth);

            if (level > 10)
                return null;

            RGB finalcolor = RGB.Black;

            if (Ambient != null)
                finalcolor += Ambient.GetColor(hd) * world.AmbientLight;

            if (Diffuse != null)
            {
                double dot = hd.Normal.Dot(testlight);
                if (dot > 0.0)
                    finalcolor += Diffuse.GetColor(hd) * dot;
            }


            
            if (Specular!=null)
            {
                Vector3 norm = hd.Normal;
                Vector3 incident = hd.Ray.Direction;
                Vector3 reflected = 2.0 * (-incident.Dot(hd.Normal)) * hd.Normal + incident;

                double specularatt = reflected.Dot(testlight);
                if (specularatt >= 0)
                    finalcolor += Specular.GetColor(hd) * Math.Pow(specularatt, SpecularFactor);
            }

            if (Reflection > 0.000)
            {
                Vector3 norm = hd.Normal;
                Vector3 incident = hd.Ray.Direction;
                Vector3 reflected = 2.0 * (-incident.Dot(hd.Normal)) * hd.Normal + incident;

                RGB col = world.GetColor(new Ray3(hd.Position, reflected),level+1);

                

                if (col != null)
                {
                    finalcolor += col*Reflection;

                    
                }

            }

            /*if (Diffuse > 0.0)
            {
                double dot = hd.Normal.Dot(testlight);
                if (dot > 0.0)
                    finalcolor += Color * dot;
            }

            if (Specular > 0.0)
            {
                
                double reflet = 2.0 * (testlight.Dot(hd.Normal));
                Vector3 phongDir = testlight - reflet * hd.Normal;
                double phongterm = Math.Max(phongDir.Dot(hd.Ray.Direction.Normalized), 0.0f);
                phongterm = Specular * Math.Pow(phongterm, 100);
                if (phongterm > 1.0)
                    phongterm = 1.0;

                finalcolor += new RGB(1, 1, 1) * phongterm;
                

              
            }*/

            return finalcolor;
        }
    }
}
