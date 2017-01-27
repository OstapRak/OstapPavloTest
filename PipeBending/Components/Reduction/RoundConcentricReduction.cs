using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundConcentricReduction : Component
    {
        private double DEFAULT_LENGTH = 5;
        private double DEFAULT_RADIUS = 5;
        private double DEFAULT_LESS_RADIUS = 3;
        public double Radius
        {
            get;
            set;
        }
        public double LessRadius
        {
            get;
            set;
        }
        public double Length
        {
            get;
            set;
        }
        public RoundConcentricReduction() : this("", 5,3,5) { }
        public RoundConcentricReduction(string name) : this(name, 5, 3, 5) { }
        public RoundConcentricReduction(string name, double radius, double lessradius, double length)
        {
            Radius = radius;
            LessRadius = lessradius;
            Length = length;
            Name = name;
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(0, Radius, 0, Length, LessRadius, 0);
            Surface surf1 = line.RevolveAsSurface(0, 2 * Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
        }

    }
}