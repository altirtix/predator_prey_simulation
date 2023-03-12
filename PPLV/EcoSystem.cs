using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPLV
{
    class EcoSystem
    {
        public double x; // prey
        public double y; // predators
        public double t; // time
        public double a; // growth rate of prey
        public double b; // death of prey
        public double c; // growth of predators
        public double d; // death of predators

        public EcoSystem(double x, double y, double t, double a, double b, double c, double d)
        {
            this.x = x;
            this.y = y;
            this.t = t;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public void doEcoSystem()
        {
            double dx = (a * x) - (b * x * y);
            double dy = (c * x * y) - (d * y);
            int dt = 1;

            x = x + dx;
            y = y + dy;
            t = t + dt;
        }

        public double getPrey()
        {
            return x;
        }

        public double getPredators()
        {
            return y;
        }

        public double getTime()
        {
            return t;
        }

    }
}
