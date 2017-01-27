using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundEndCaps : Component
    {
        private double COEF_FOR_BIGGER_RADIUS = 3;
        private double DEFAULT_RADIUS = 5;
        public double Radius
        {
            get;
            set;
        }
        public RoundEndCaps() : this("", 5) { }
        public RoundEndCaps(string name) : this(name, 5) { }
        public RoundEndCaps(string name, double radius)
        {
            Radius = radius;
            // Length = DEFAULT_LENGTH;
            Name = name;
            Circle circle = new Arc(Plane.ZX, new Point3D(-COEF_FOR_BIGGER_RADIUS * Radius * Math.Cos(Math.Asin(1 / COEF_FOR_BIGGER_RADIUS)), 0, 0), COEF_FOR_BIGGER_RADIUS * Radius, Math.PI / 2 - Math.Asin(1 / COEF_FOR_BIGGER_RADIUS), Math.PI / 2);
            Surface surf = circle.RevolveAsSurface(0, Math.PI * 2, Vector3D.AxisX, new Point3D(0, 0, 0))[0];
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
        }
    }
}