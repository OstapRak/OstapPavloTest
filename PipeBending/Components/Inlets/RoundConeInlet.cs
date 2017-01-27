using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundConeInlet : Component
    {
        private double DEFAULT_LENGTH = 5;
        private double DEFAULT_WIDTH = 2;
        private double DEFAULT_RADIUS = 5;
        private double DEFAULT_LESS_RADIUS = 3;
        public double Radius
        {
            get;
            set;
        }
        public double Width
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
        public RoundConeInlet() : this("", 5,3,5,2) { }
        public RoundConeInlet(string name) : this(name, 5,3,5,2) { }
        public RoundConeInlet(string name, double radius, double lessradius, double length, double width)
        {
            Radius = radius;
            LessRadius = lessradius;
            Length = length;
            Width = width;
            Name = name;
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(0, Radius, 0, Width, Radius, 0);
            Surface surf1 = line.RevolveAsSurface(0, 2 * Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
            line = new devDept.Eyeshot.Entities.Line(Width, Radius, 0, Length, LessRadius, 0);
            surf1 = line.RevolveAsSurface(0, 2 * Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
        }

    }
}