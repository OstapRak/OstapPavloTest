using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class VentilationUnitDShapeConnection : Component
    {
        private double DEFAULT_HEIGHT = 15;
        private double DEFAULT_WIDTH = 15;
        private double DEFAULT_RADIUS = 3;
        private double DEFAULT_LENGTH = 16;
        private double DEFAULT_CONE_LENGTH = 2;
        private double DEFAULT_ZIPPER_LENGTH = 1;
        private int SLICES = 600;
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
        public double ZipperLength
        {
            get;
            set;
        }
        public double Height
        {
            get;
            set;
        }
        public double Width
        {
            get;
            set;
        }
        public double ConeLength
        {
            get;
            set;
        }
        public VentilationUnitDShapeConnection() : this("", 3,16,15,15,1,2) { }
        public VentilationUnitDShapeConnection(string name) : this(name, 3, 16, 15, 15, 1, 2) { }
        public VentilationUnitDShapeConnection(string name, double radius, double length, double height, double width, double zipperLength, double coneLength)
        {
            Radius = radius;
            Length = length;
            Height = height;
            Width = width;
            ConeLength = coneLength;
            ZipperLength = zipperLength;
            Name = name;
            List<Point3D> points = new List<Point3D>();
            List<IndexTriangle> triangles = new List<IndexTriangle>();
            points.Add(new Point3D(ConeLength, -Radius, 0));
            points.Add(new Point3D(0, -Width / 2, Height / 2));
            for (int i = 1; i <= SLICES; ++i)
            {
                if (i <= SLICES / 4)
                {
                    points.Add(new Point3D(ConeLength, -Radius + i * Radius / SLICES * 8, 0));
                    points.Add(new Point3D(0, Height / 2 * Math.Tan(-Math.PI / 4 + 2 * Math.PI / SLICES * i), Height / 2));

                }
                else if (i <= SLICES / 2)
                {
                    points.Add(new Point3D(ConeLength, Radius * Math.Sin(+(2 * Math.PI / SLICES * i / 2 + Math.PI / 4)), Radius * Math.Cos(+(2 * Math.PI / SLICES * i / 2 + Math.PI / 4))));
                    points.Add(new Point3D(0, Width / 2, -Width / 2 * Math.Tan(-Math.PI / 4 + (2 * Math.PI / SLICES * i - Math.PI / 2))));
                }
                else if (i < 3 * SLICES / 4)
                {
                    points.Add(new Point3D(ConeLength, Radius * Math.Sin(-Math.PI / 4 + 2 * Math.PI / SLICES * i), Radius * Math.Cos(-Math.PI / 4 + 2 * Math.PI / SLICES * i)));
                    points.Add(new Point3D(0, -Height / 2 * Math.Tan(-Math.PI / 4 + (-Math.PI + 2 * Math.PI / SLICES * i)), -Height / 2));
                }
                else
                {
                    points.Add(new Point3D(ConeLength, Radius * Math.Sin(+(2 * Math.PI / SLICES * i / 2 - 3 * Math.PI / 2)), Radius * Math.Cos(+(2 * Math.PI / SLICES * i / 2 - 3 * Math.PI / 2))));
                    points.Add(new Point3D(0, -Width / 2, Width / 2 * Math.Tan(-Math.PI / 4 + (2 * Math.PI / SLICES * i - 3 * Math.PI / 2))));
                }
                triangles.Add(new IndexTriangle(i * 2 - 2, i * 2, i * 2 - 1));
                triangles.Add(new IndexTriangle(i * 2 - 1, i * 2, i * 2 + 1));
            }
            Mesh m = new Mesh(points, triangles);
            m.ColorMethod = colorMethodType.byEntity;
            m.EntityData = Name;
            m.Color = System.Drawing.Color.White;
            m.Translate(Length - ConeLength - ZipperLength, 0, 0);
            Entities.Add(m);
            LinearPath lp = new LinearPath(5);
            lp.Vertices[0] = new Point3D(0, -Width / 2, -Height / 2);
            lp.Vertices[1] = new Point3D(0, -Width / 2, Height / 2);
            lp.Vertices[2] = new Point3D(0, Width / 2, Height / 2);
            lp.Vertices[3] = new Point3D(0, Width / 2, -Height / 2);
            lp.Vertices[4] = new Point3D(0, -Width / 2, -Height / 2);
            Region profile = new Region(lp);
            Surface surf = profile.ConvertToSurface();
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
            Point3D p = lp.Vertices[3];
            lp.Vertices[3] = lp.Vertices[1];
            lp.Vertices[1] = p;
            for (int i = 0; i < 4; ++i)
            {
                surf = lp.ExtrudeAsSurface(Length - ConeLength - ZipperLength, 0, 0)[i];
                surf.EntityData = Name;
                surf.ColorMethod = colorMethodType.byEntity;
                surf.Color = System.Drawing.Color.White;
                Entities.Add(surf);
            }
            Circle circle = new Arc(Plane.YZ, new Point3D(0, 0, 0), Radius, Math.PI, Math.PI * 2);
            circle.Translate(Length - ZipperLength, 0, 0);
            surf = circle.ExtrudeAsSurface(ZipperLength, 0, 0)[0];
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(Length - ZipperLength, Radius, 0, Length - ZipperLength, -Radius, 0);
            Surface surf1 = line.ExtrudeAsSurface(ZipperLength, 0, 0)[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
        }
    }
}