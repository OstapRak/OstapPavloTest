using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundEndCapWithZipper: Component
    {
        private double COEF_FOR_BIGGER_RADIUS = 3;
        private double DEFAULT_RADIUS = 5;
        private double DEFAULT_WIDTH = 3;

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
        public RoundEndCapWithZipper() : this("", 5,1) { }
        public RoundEndCapWithZipper(string name) : this(name, 5,1) { }
        public RoundEndCapWithZipper(string name, double radius, double width)
        {
            Radius = radius;
            Width = width;
            // Length = DEFAULT_LENGTH;
            Name = name;
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(0, Radius, 0, Width, Radius, 0);
            Surface surf1 = line.RevolveAsSurface(0, 2 * Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
            Circle circle = new Arc(Plane.ZX, new Point3D(Width-COEF_FOR_BIGGER_RADIUS * Radius * Math.Cos(Math.Asin(1 / COEF_FOR_BIGGER_RADIUS)), 0, 0), COEF_FOR_BIGGER_RADIUS * Radius, Math.PI / 2 - Math.Asin(1 / COEF_FOR_BIGGER_RADIUS), Math.PI / 2);
            Surface surf = circle.RevolveAsSurface(0, Math.PI * 2, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
        }
    }
}