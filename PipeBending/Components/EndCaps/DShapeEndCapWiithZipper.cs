using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class DShapeEndCapWiithZipper: Component
    {
        private double COEF_FOR_BIGGER_RADIUS = 3;
        private double DEFAULT_RADIUS = 5;
        private double DEFAULT_WIDTH = 1;

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
        public DShapeEndCapWiithZipper() : this("", 5,1) { }
        public DShapeEndCapWiithZipper(string name) : this(name, 5,1) { }
        public DShapeEndCapWiithZipper(string name, double radius, double width)
        {
            Radius = radius;
            Width = width;
            Name = name;
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(0, Radius, 0, Width, Radius, 0);
            Surface surf1 = line.RevolveAsSurface(0, Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
            surf1 = line.ExtrudeAsSurface(0, -2 * Radius, 0)[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
            Circle circle = new Arc(Plane.XY, new Point3D(Width - COEF_FOR_BIGGER_RADIUS * Radius * Math.Cos(Math.Asin(1 / COEF_FOR_BIGGER_RADIUS)), 0, 0), COEF_FOR_BIGGER_RADIUS * Radius, -Math.Asin(1 / COEF_FOR_BIGGER_RADIUS), 0);
            Surface surf = circle.RevolveAsSurface(Math.PI, Math.PI, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
            circle = new Arc(Plane.XY, new Point3D(Width - COEF_FOR_BIGGER_RADIUS * Radius * Math.Cos(Math.Asin(1 / COEF_FOR_BIGGER_RADIUS)), 0, 0), COEF_FOR_BIGGER_RADIUS * Radius, Math.Asin(1 / COEF_FOR_BIGGER_RADIUS), -Math.Asin(1 / COEF_FOR_BIGGER_RADIUS));
            Region profile = new Region(circle);
            surf = profile.ConvertToSurface();
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
        }
    }
}
