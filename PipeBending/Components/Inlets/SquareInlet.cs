using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class SquareInlet : Component
    {
        private double DEFAULT_LENGTH = 3;
        private double DEFAULT_WIDTH = 10;
        private double DEFAULT_HEIGHT = 10;
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
        public double Length
        {
            get;
            set;
        }
        public SquareInlet() : this("", 3,10,10) { }
        public SquareInlet(string name) : this(name, 3,10,10) { }
        public SquareInlet(string name, double length, double height, double width)
        {
            Width = width;
            Height = height;
            Length = length;
            Name = name;
            LinearPath lp = new LinearPath(7);
            lp.Vertices[0] = new Point3D(0, -Height / 2, -Width / 2);
            lp.Vertices[1] = new Point3D(0, -Height / 2, Width / 2);
            lp.Vertices[2] = new Point3D(0, Height / 2, Width / 2);
            lp.Vertices[3] = new Point3D(0, Height / 2, -Width / 2);
            lp.Vertices[4] = new Point3D(0, -Height / 2, -Width / 2);
            for (int i = 0; i < 4; ++i)
            {
                Surface surf = lp.ExtrudeAsSurface(Length, 0, 0)[i];
                surf.EntityData = Name;
                surf.ColorMethod = colorMethodType.byEntity;
                surf.Color = System.Drawing.Color.White;
                Entities.Add(surf);
            }
        }
        
    }
}