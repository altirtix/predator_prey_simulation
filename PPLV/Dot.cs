using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPLV
{
    public class Dot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Widthsize { get; set; }
        public int Heightsize { get; set; }

        public Graphics g;

        Random random = new Random();

        public Dot(Graphics graphics)
        {
            g = graphics;
        }
        public void Plot(Color color)
        {
            g.FillEllipse(new SolidBrush(Color.White), X, Y, Widthsize, Heightsize);
            X += random.Next(-5, 5);
            Y += random.Next(-5, 5);
            g.FillEllipse(new SolidBrush(color), X, Y, Widthsize, Heightsize);
        }

        public void Clear()
        {
            g.FillEllipse(new SolidBrush(Color.White), X, Y, Widthsize, Heightsize);
        }
    }
}
