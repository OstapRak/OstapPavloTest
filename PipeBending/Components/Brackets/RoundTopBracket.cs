using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundTopBracketradius : Component
    {
        private double DEFAULT_EXT_RADIUS = 6;
        private double DEFAULT_RADIUS = 4;
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
        public RoundTopBracketradius() : this("", 4,6) { }
        public RoundTopBracketradius(string name) : this(name, 4, 6) { }
        public RoundTopBracketradius(string name, double radius, double extRadius)
        {
            Radius = radius;
            ExtRadius = extRadius;
            Name = name;
            List<Point3D> points = new List<Point3D>();
            List<IndexTriangle> triangles = new List<IndexTriangle>();
            double l = ExtRadius - Math.Sqrt(ExtRadius * ExtRadius - Math.Pow(Radius + ExtRadius - Radius, 2));
            points.Add(new Point3D(0, 0, Radius));
            points.Add(new Point3D(l, 0, Radius));
            for (int i = 1; i <= SLICES; ++i)
            {
                l = ExtRadius - Math.Sqrt(ExtRadius * ExtRadius - Math.Pow(Radius * Math.Cos(2 * Math.PI / SLICES * i) + ExtRadius - Radius, 2));
                points.Add(new Point3D(0, Radius * Math.Sin(2 * Math.PI / SLICES * i), Radius * Math.Cos(2 * Math.PI / SLICES * i)));
                points.Add(new Point3D(l, Radius * Math.Sin(2 * Math.PI / SLICES * i), Radius * Math.Cos(2 * Math.PI / SLICES * i)));
                triangles.Add(new IndexTriangle(i * 2 - 2, i * 2 - 1, i * 2));
                triangles.Add(new IndexTriangle(i * 2 - 1, i * 2 + 1, i * 2));
            }
            Mesh m = new Mesh(points, triangles);
            m.ColorMethod = colorMethodType.byEntity;
            m.Color = System.Drawing.Color.White;
            m.EntityData = Name;
            Entities.Add(m);
        }
    }
}