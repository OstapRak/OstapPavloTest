using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class DShapeBracket : Component
    {
        private double DEFAULT_EXT_RADIUS = 6;
        private double DEFAULT_RADIUS = 5;
        private int SLICES = 50;
        public double Radius
        {
            get;
            set;
        }
        public double ExtRadius
        {
            get;
            set;
        }
        public DShapeBracket() : this("", 5, 6) { }
        public DShapeBracket(string name) : this(name, 5, 6) { }
        public DShapeBracket(string name, double radius, double extRadius)
        {
            Radius = radius;
            ExtRadius = extRadius;
            Name = name;
            List<Point3D> points = new List<Point3D>();
            List<IndexTriangle> triangles = new List<IndexTriangle>();
            points.Add(new Point3D(0, Radius, 0));
            points.Add(new Point3D(0, Radius, 0));
            for (int i = 1; i <= SLICES; ++i)
            {
                double l = ExtRadius - Math.Sqrt(ExtRadius * ExtRadius - Math.Pow(Radius * Math.Sin(Math.PI / SLICES * i), 2));
                points.Add(new Point3D(0, Radius * Math.Cos(Math.PI / SLICES * i), Radius * Math.Sin(Math.PI / SLICES * i)));
                points.Add(new Point3D(l, Radius * Math.Cos(Math.PI / SLICES * i), Radius * Math.Sin(Math.PI / SLICES * i)));
                triangles.Add(new IndexTriangle(i * 2 - 2, i * 2, i * 2 - 1));
                triangles.Add(new IndexTriangle(i * 2 - 1, i * 2, i * 2 + 1));
            }
            triangles.Add(new IndexTriangle(0, 0, SLICES * 2 + 1));
            Mesh m = new Mesh(points, triangles); //
            m.ColorMethod = colorMethodType.byEntity;
            m.EntityData = Name;
            m.Color = System.Drawing.Color.White;
            Entities.Add(m);
        }
    }
}