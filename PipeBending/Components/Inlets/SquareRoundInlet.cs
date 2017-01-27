using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class SquareRoundInlet : Component
    {
        private double DEFAULT_HEIGHT = 15;
        private double DEFAULT_WIDTH = 15;
        private double DEFAULT_RADIUS = 6;
        private double DEFAULT_LENGTH = 7;
        private double DEFAULT_ZIPPER_LENGTH = 1;
        private int SLICES = 24;
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
        public SquareRoundInlet() : this("", 6,7,15,15,1) { }
        public SquareRoundInlet(string name) : this(name, 6, 7, 15, 15, 1) { }
        public SquareRoundInlet(string name, double radius, double length, double height, double width, double zipperLength)
        {
            Radius = radius;
            Length = length;
            Height = height;
            Width = width;
            ZipperLength = zipperLength;
            Name = name;
            List<Point3D> points = new List<Point3D>();
            //SmoothTriangle
            List<IndexTriangle> triangles = new List<IndexTriangle>();
            Mesh m = new Mesh(SLICES*3+2, SLICES*4 , Mesh.natureType.Smooth);
            
            m.Vertices[0] = new Point3D(0, Radius * Math.Sin(-Math.PI / 4), Radius * Math.Cos(-Math.PI / 4));
            m.Vertices[1] = new Point3D(Length - ZipperLength, -Width / 2, Height / 2);
            for (int i = 1; i <= SLICES; ++i)
            {
                
                m.Vertices[i*3] = new Point3D(0, Radius * Math.Sin(-Math.PI / 4 + 2 * Math.PI / SLICES * i), Radius * Math.Cos(-Math.PI / 4 + 2 * Math.PI / SLICES * i));
                if (i <= SLICES / 4)
                {
                    m.Vertices[i * 3+1] = (new Point3D(Length - ZipperLength, Height / 2 * Math.Tan(-Math.PI / 4 + 2 * Math.PI / SLICES * i), Height / 2));
                }
                else if (i <= SLICES / 2)
                {
                    m.Vertices[i * 3+1] = (new Point3D(Length - ZipperLength, Width / 2, -Width / 2 * Math.Tan(-Math.PI / 4 + (2 * Math.PI / SLICES * i - Math.PI / 2))));
                }
                else if (i < 3 * SLICES / 4)
                {
                    m.Vertices[i * 3+1] = (new Point3D(Length - ZipperLength, -Height / 2 * Math.Tan(-Math.PI / 4 + (-Math.PI + 2 * Math.PI / SLICES * i)), -Height / 2));
                }
                else
                {
                    m.Vertices[i * 3+1] = (new Point3D(Length - ZipperLength, -Width / 2, Width / 2 * Math.Tan(-Math.PI / 4 + (2 * Math.PI / SLICES * i - 3 * Math.PI / 2))));
                }
                //central point
                m.Vertices[i * 3 - 1] = new Point3D((Length - ZipperLength) / 2, (m.Vertices[i * 3 - 3].Y + m.Vertices[i * 3 - 2].Y + m.Vertices[i * 3 + 1].Y + m.Vertices[i * 3].Y) / 4, (m.Vertices[i * 3 - 3].Z + m.Vertices[i * 3 - 2].Z + m.Vertices[i * 3 + 1].Z + m.Vertices[i * 3].Z) / 4);
                m.Triangles[i * 4 - 4] = (new SmoothTriangle(i * 3 - 2, i * 3 -3, i * 3-1));
                m.Triangles[i * 4 - 3] = (new SmoothTriangle(i * 3 - 2, i * 3-1, i * 3+1));
                m.Triangles[i * 4 - 2] = (new SmoothTriangle(i * 3 + 1, i * 3 - 1, i * 3));
                m.Triangles[i * 4 - 1] = (new SmoothTriangle(i * 3 - 3, i * 3, i * 3 - 1));
            }
           // Mesh m = new Mesh(points, triangles, Mesh.natureType.Smooth); //
            m.ColorMethod = colorMethodType.byEntity;
            m.EntityData = Name;
            m.Color = System.Drawing.Color.White;
            m.EdgeStyle = 0;
            

            Entities.Add(m);
            LinearPath lp = new LinearPath(5);
            lp.Vertices[0] = new Point3D(Length - ZipperLength, -Width / 2, -Height / 2);
            lp.Vertices[1] = new Point3D(Length - ZipperLength, Width / 2, -Height / 2);
            lp.Vertices[2] = new Point3D(Length - ZipperLength, Width / 2, Height / 2);
            lp.Vertices[3] = new Point3D(Length - ZipperLength, -Width / 2, Height / 2);
            lp.Vertices[4] = new Point3D(Length - ZipperLength, -Width / 2, -Height / 2);

            for (int i = 0; i < 4; ++i)
            {
                Surface surf = lp.ExtrudeAsSurface(ZipperLength, 0, 0)[i];
                surf.EntityData = Name;
                surf.ColorMethod = colorMethodType.byEntity;
                surf.Color = System.Drawing.Color.White;
                surf.ShowCurvature = false;
                Entities.Add(surf);
            }
        }
    }
}