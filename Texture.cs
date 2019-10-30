using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioLum
{
    public abstract class Texture
    {
        public abstract RGB GetColor(HitData hd);
    }

    public class TextureSolidColor : Texture
    {
        RGB rgb;

        public static TextureSolidColor Red = new TextureSolidColor(RGB.Red);
        public static TextureSolidColor Green = new TextureSolidColor(RGB.Green);
        public static TextureSolidColor Blue = new TextureSolidColor(RGB.Blue);
        public static TextureSolidColor Yellow = new TextureSolidColor(RGB.Yellow);
        public static TextureSolidColor Magenta = new TextureSolidColor(RGB.Magenta);
        public static TextureSolidColor Cyan = new TextureSolidColor(RGB.Cyan);
        public static TextureSolidColor White = new TextureSolidColor(RGB.White);
        public static TextureSolidColor Gray = new TextureSolidColor(RGB.Gray);
        public static TextureSolidColor Black = new TextureSolidColor(RGB.Black);

        
        public TextureSolidColor(double r, double g, double b)
        {
            rgb=new RGB(r,g,b);
        }

        public TextureSolidColor(RGB rgb)
        {
            this.rgb = rgb;
        }

        public override RGB GetColor(HitData hd)
        {
            return rgb;
        }
    }
}
