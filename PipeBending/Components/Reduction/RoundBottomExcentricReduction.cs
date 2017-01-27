using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundBottomExcentricReduction : Component
    {
        private double DEFAULT_LES_RADIUS = 4;
        private double DEFAULT_RADIUS = 6;
        private double DEFAULT_LENGTH = 5;
        private int SLICES = 50;
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
        public double LesRadius
        {
            get;
            set;
        }
        public RoundBottomExcentricReduction() : this("", 6,4,5) { }
        public RoundBottomExcentricReduction(string name) : this(name, 6, 4, 5) { }
        public RoundBottomExcentricReduction(string name, double radius, double lessRadius, double length)
        {
            Radius = radius;
            Length = length;
            LesRadius = lessRadius;
            Name = name;
            List<Point3D> points = new List<Point3D>();
            List<IndexTriangle> triangles = new List<IndexTriangle>();
            points.Add(new Point3D(0, 0, LesRadius));
            points.Add(new Point3D(Length, 0, 2 * Radius - LesRadius));
            for (int i = 1; i <= SLICES; ++i)
            {
                points.Add(new Point3D(0, LesRadius * Math.Sin(2 * Math.PI / SLICES * i), LesRadius * Math.Cos(2 * Math.PI / SLICES * i)));
                points.Add(new Point3D(Length, Radius * Math.Sin(2 * Math.PI / SLICES * i), Radius - LesRadius + Radius * Math.Cos(2 * Math.PI / SLICES * i)));
                triangles.Add(new IndexTriangle(i * 2 - 2, i * 2 - 1, i * 2));
                triangles.Add(new IndexTriangle(i * 2 - 1, i * 2 + 1, i * 2));
            }
            Mesh m = new Mesh(points, triangles); //
            m.ColorMethod = colorMethodType.byEntity;
            m.EntityData = Name;
            m.Color = System.Drawing.Color.White;
            Entities.Add(m);
        }
    }
}