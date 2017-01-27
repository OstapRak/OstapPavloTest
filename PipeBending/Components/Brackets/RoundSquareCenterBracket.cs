using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundSquareCenterBracket : Component
    {
        private double DEFAULT_RADIUS = 10;
        private double DEFAULT_WIDTH = 7;
        private double DEFAULT_HEIGHT = 7;
        public double Width
        {
            get;
            set;
        }
        public double Height
        {
            get;
            set;
        }
        public double Radius
        {
            get;
            set;
        }
        public RoundSquareCenterBracket() : this("", 7, 7,10) { }
        public RoundSquareCenterBracket(string name) : this(name, 7, 7,10) { }
        public RoundSquareCenterBracket(string name, double width, double height, double radius)
        {
            Width = width;
            Height = height;
            Radius = radius;
            Name = name;

            double l = Radius - Math.Sqrt(Radius * Radius - Height * Height / 4);
            LinearPath lp = new LinearPath(5);
            lp.Vertices[0] = new Point3D(0, Width / 2, -Height / 2);
            lp.Vertices[1] = new Point3D(0, Width / 2, Height / 2);
            lp.Vertices[2] = new Point3D(l, Width / 2, Height / 2);
            lp.Vertices[3] = new Point3D(l, Width / 2, -Height / 2);
            lp.Vertices[4] = new Point3D(0, Width / 2, -Height / 2);
            Circle circle = new Arc(Plane.ZX, new Point3D(Radius, Width / 2, 0), Radius, -Math.Asin(Height/2 / Radius) - Math.PI / 2, Math.Asin(Height / 2 / Radius) - Math.PI / 2); //Math.PI, 3/2*Math.PI - Math.Acos(l/Radius));
            Region profile = new Region(lp, circle);
            Surface surf = profile.ConvertToSurface();
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
            lp.Vertices[0] = new Point3D(0, -Width / 2, -Height / 2);
            lp.Vertices[1] = new Point3D(l, -Width / 2, -Height / 2); 
            lp.Vertices[2] = new Point3D(l, -Width / 2, Height / 2);
            lp.Vertices[3] = new Point3D(0, -Width / 2, Height / 2);
            lp.Vertices[4] = new Point3D(0, -Width / 2, -Height / 2);
            circle = new Arc(Plane.ZX, new Point3D(Radius, -Width / 2, 0), Radius, Math.Asin(Height / 2 / Radius) - Math.PI / 2, -Math.Asin(Height / 2 / Radius) - Math.PI / 2); //Math.PI, 3/2*Math.PI - Math.Acos(l/Radius));
            profile = new Region(lp, circle);
            surf = profile.ConvertToSurface();
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
            lp = new LinearPath(5);
            lp.Vertices[0] = new Point3D(0, -Width / 2, Height / 2);
            lp.Vertices[1] = new Point3D(l, -Width / 2, Height / 2);
            lp.Vertices[2] = new Point3D(l, Width / 2, Height / 2);
            lp.Vertices[3] = new Point3D(0, Width / 2, Height / 2);
            lp.Vertices[4] = new Point3D(0, -Width / 2, Height / 2);
            profile = new Region(lp);
            surf = profile.ConvertToSurface();
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
            lp = new LinearPath(5);
            lp.Vertices[0] = new Point3D(0, -Width / 2, -Height / 2);
            lp.Vertices[1] = new Point3D(0, Width / 2, -Height / 2);
            lp.Vertices[2] = new Point3D(l, Width / 2, -Height / 2);
            lp.Vertices[3] = new Point3D(l, -Width / 2, -Height / 2); 
            lp.Vertices[4] = new Point3D(0, -Width / 2, -Height / 2);
            profile = new Region(lp);
            surf = profile.ConvertToSurface();
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
        }
    }
}
