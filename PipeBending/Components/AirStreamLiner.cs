using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class AirStreamLiner:Component
    {
        private double DEFAULT_LENGTH = 15;
        private double DEFAULT_RADIUS = 5;
        public double Radius
        {
            get;
            set;
        }
        public double Length
        {
            get;
            set;
        }
        public AirStreamLiner() : this("", 5,15) { }
        public AirStreamLiner(string name) : this(name, 5, 15) { }
        public AirStreamLiner(string name, double radius, double length)
        {
            Radius = radius;
            Length = length;
            Name = name;
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(0, Radius, 0, Length, 0, 0);
            Surface surf1 = line.RevolveAsSurface(0, 2 * Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
        }

    }
}