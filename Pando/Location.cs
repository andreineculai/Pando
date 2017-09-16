using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pando
{
    class Location
    {
        public int x;
        public int y;
        public double p;
        public String name;

        public Location(int _x, int _y, double _p, String _name)
        {
            x = _x;
            y = _y;
            p = _p;
            name = _name;
        }
        public Location(int _x, int _y, double _p)
        {
            x = _x;
            y = _y;
            p = _p;
            name = "undefined";
        }
        public String GetInfo(){
            String info = "Locatia"+"\r\nCoordonate: [" + x + ", " + y + "], pondere: " + p + ", nume: " + name;
            return info;
        }
    }
}
