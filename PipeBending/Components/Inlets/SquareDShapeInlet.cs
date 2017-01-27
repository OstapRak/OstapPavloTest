using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class SquareDShapeInlet : Component
    {
        private double DEFAULT_HEIGHT = 15;
        private double DEFAULT_WIDTH = 15;
        private double DEFAULT_RADIUS = 3;
        private double DEFAULT_LENGTH = 16;
        private double DEFAULT_ZIPPER_LENGTH = 1;
        private int SLICES = 120;
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
        public SquareDShapeInlet() : this("", 3,16,15,15,1) { }
        public SquareDShapeInlet(string name) : this(name, 3, 16, 15, 15, 1) { }
        public SquareDShapeInlet(string name, double radius, double length, double height, double width, double zipperLength)
        {
            Radius = radius;
            Length = length;
            Height = height;
            Width = width;
            ZipperLength = zipperLength;
            Name = name;
            List<Point3D> points = new List<Point3D>();
            List<IndexTriangle> triangles = new List<IndexTriangle>();
            points.Add(new Point3D(0, -Radius, 0));
            points.Add(new Point3D(Length, -Width / 2, 0));
            Length -= ZipperLength;
            for (int i = 1; i <= SLICES; ++i)
            {
                if (i <= SLICES / 4)
                {
                    points.Add(new Point3D(0, -Radius + i * Radius / SLICES * 8, 0));
                    points.Add(new Point3D(Length, -Width / 2 + i * Width / SLICES * 4, 0));

                }
                else if (i <= SLICES / 2)
                {
                    points.Add(new Point3D(0, Radius * Math.Sin(+(2 * Math.PI / SLICES * i / 2 + Math.PI / 4)), Radius * Math.Cos(+(2 * Math.PI / SLICES * i / 2 + Math.PI / 4))));
                    points.Add(new Point3D(Length, Width / 2, -Height / 2 - Width / 2 * Math.Tan(-Math.PI / 4 + (2 * Math.PI / SLICES * i - Math.PI / 2))));
                }
                else if (i < 3 * SLICES / 4)
                {
                    points.Add(new Point3D(0, Radius * Math.Sin(-Math.PI / 4 + 2 * Math.PI / SLICES * i), Radius * Math.Cos(-Math.PI / 4 + 2 * Math.PI / SLICES * i)));
                    points.Add(new Point3D(Length, -Height / 2 * Math.Tan(-Math.PI / 4 + (-Math.PI + 2 * Math.PI / SLICES * i)), -Height));
                }
                else
                {
                    points.Add(new Point3D(0, Radius * Math.Sin(+(2 * Math.PI / SLICES * i / 2 - 3 * Math.PI / 2)), Radius * Math.Cos(+(2 * Math.PI / SLICES * i / 2 - 3 * Math.PI / 2))));
                    points.Add(new Point3D(Length, -Width / 2, -Height / 2 + Width / 2 * Math.Tan(-Math.PI / 4 + (2 * Math.PI / SLICES * i - 3 * Math.PI / 2))));
                }
                triangles.Add(new IndexTriangle(i * 2 - 2, i * 2 - 1, i * 2));
                triangles.Add(new IndexTriangle(i * 2 - 1, i * 2 + 1, i * 2));
            }
            Mesh m = new Mesh(points, triangles);
            m.ColorMethod = colorMethodType.byEntity;
            m.Color = System.Drawing.Color.White;
            m.EntityData = Name;
            Entities.Add(m);
            Length += ZipperLength;
            LinearPath lp = new LinearPath(5);
            lp.Vertices[0] = new Point3D(Length - ZipperLength, -Width / 2, -Height);
            lp.Vertices[1] = new Point3D(Length - ZipperLength, Width / 2, -Height);
            lp.Vertices[2] = new Point3D(Length - ZipperLength, Width / 2, 0);
            lp.Vertices[3] = new Point3D(Length - ZipperLength, -Width / 2, 0);
            lp.Vertices[4] = new Point3D(Length - ZipperLength, -Width / 2, -Height);

            for (int i = 0; i < 4; ++i)
            {
                Surface surf = lp.ExtrudeAsSurface(ZipperLength, 0, 0)[i];
                surf.EntityData = Name;
                surf.ColorMethod = colorMethodType.byEntity;
                surf.Color = System.Drawing.Color.White;
                Entities.Add(surf);
            }
        }
    }
}