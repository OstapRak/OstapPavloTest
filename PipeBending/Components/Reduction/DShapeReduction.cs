using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class DShapeReduction : Component
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
        public DShapeReduction() : this("", 5,3,5) { }
        public DShapeReduction(string name) : this(name, 5, 3, 5) { }
        public DShapeReduction(string name, double radius, double lessradius, double length)
        {
            Radius = radius;
            LessRadius = lessradius;
            Length = length;
            Name = name;
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(0, Radius, 0, Length, LessRadius, 0);
            Surface surf1 = line.RevolveAsSurface(0, Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
            LinearPath lp = new LinearPath(5);
            lp.Vertices[0] = new Point3D(0, -Radius, 0);
            lp.Vertices[1] = new Point3D(0, Radius, 0);
            lp.Vertices[2] = new Point3D(Length, LessRadius, 0);
            lp.Vertices[3] = new Point3D(Length, -LessRadius, 0);
            lp.Vertices[4] = new Point3D(0, -Radius, 0);
            Region profile = new Region(lp);
            surf1 = profile.ConvertToSurface();
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
        }
    }
}