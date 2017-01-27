using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.ComponentsData
{
    public class RoundDuctData: ComponentData
    {
        public double Length
        {
            get;
            set;
        }
        public double Radius
        {
            get;
            set;
        }
        public RoundDuctData() : this(20, 3, "") { }
        public RoundDuctData(string name) : this(20, 3, name) { }
        public RoundDuctData(double length, double radius, string name): base(name)
        {
            Length = length;
            Radius = radius;
        }
    }
}
