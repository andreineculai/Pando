using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pando
{
    class Particle
    {
        public double x;
        public double y;
        public double fitness;
        public double vx;
        public double vy;
        static public Random rand = new Random();
        public Particle()
        {
            x = rand.NextDouble() * 10;
            y = rand.NextDouble() * 10;
            vx = rand.NextDouble() * 5;
            vy = rand.NextDouble() * 5;
        }

        public Particle(double x, double y, double fitness)
        {
            this.x = x;
            this.y = y;
            this.fitness = fitness;
        }

        public void computeFitness(List<Location> locations) {
            fitness = 0;
            foreach (var location in locations)
            {
                fitness += location.p * distance(location);
            }
        }

        public void computeVelocity(Particle pBest, Particle gBest, double c1, double c2) {
            vx += c1 * rand.NextDouble() * (pBest.x - x) + c2 * rand.NextDouble() * (gBest.x - x);
            vy += c1 * rand.NextDouble() * (pBest.y - y) + c2 * rand.NextDouble() * (gBest.y - y);
        }

        public double distance(Location loc) {
            return Math.Sqrt((this.x - loc.x) * (this.x - loc.x) + (this.y - loc.y) * (this.y - loc.y));
        }



    }
}
