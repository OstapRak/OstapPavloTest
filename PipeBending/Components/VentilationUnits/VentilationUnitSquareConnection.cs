using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class VentilationUnitSquareConnection : Component
    {
        private double DEFAULT_LENGTH = 10;
        private double DEFAULT_WIDTH = 10;
        private double DEFAULT_HEIGHT = 10;
        private double DEFAULT_INL_LENGTH = 1;
        private double DEFAULT_INL_WIDTH = 5;
        private double DEFAULT_INL_HEIGHT = 5;
        private double DEFAULT_CONE_LENGTH = 2;
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
        public double InlWidth
        {
            get;
            set;
        }
        public double InlHeight
        {
            get;
            set;
        }
        public double InlLength
        {
            get;
            set;
        }
        public double ConeLength
        {
            get;
            set;
        }
        public VentilationUnitSquareConnection() : this("", 10,10,10,1,5,5,2) { }
        public VentilationUnitSquareConnection(string name) : this(name, 10, 10, 10, 1, 5, 5, 2) { }
        public VentilationUnitSquareConnection(string name, double length, double height, double width, double inllength, double inlheight, double inlwidth, double conelength)
        {
            Width = DEFAULT_WIDTH;
            Height = DEFAULT_HEIGHT;
            Length = DEFAULT_LENGTH;
            InlWidth = DEFAULT_INL_WIDTH;
            InlHeight = DEFAULT_INL_HEIGHT;
            InlLength = DEFAULT_INL_LENGTH;
            ConeLength = DEFAULT_CONE_LENGTH;
            Name = "";
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
                surf = lp.ExtrudeAsSurface(Length - ConeLength, 0, 0)[i];
                surf.EntityData = Name;
                surf.ColorMethod = colorMethodType.byEntity;
                surf.Color = System.Drawing.Color.White;
                Entities.Add(surf);
            }
            lp.Translate(Length - ConeLength, 0, 0);
            LinearPath lp1 = new LinearPath(5);
            lp1.Vertices[0] = new Point3D(Length, -InlWidth / 2, -InlHeight / 2);
            lp1.Vertices[1] = new Point3D(Length, InlWidth / 2, -InlHeight / 2);
            lp1.Vertices[2] = new Point3D(Length, InlWidth / 2, InlHeight / 2);
            lp1.Vertices[3] = new Point3D(Length, -InlWidth / 2, InlHeight / 2);
            lp1.Vertices[4] = new Point3D(Length, -InlWidth / 2, -InlHeight / 2);
            for (int i = 0; i < 4; ++i)
            {
                surf = lp1.ExtrudeAsSurface(InlLength, 0, 0)[i];
                surf.EntityData = Name;
                surf.ColorMethod = colorMethodType.byEntity;
                surf.Color = System.Drawing.Color.White;
                Entities.Add(surf);
                LinearPath lp2 = new LinearPath(5);
                lp2.Vertices[0] = lp.Vertices[i];
                lp2.Vertices[1] = lp.Vertices[(i + 1) % 5];
                lp2.Vertices[2] = lp1.Vertices[(i + 1) % 5]; 
                lp2.Vertices[3] = lp1.Vertices[i]; 
                lp2.Vertices[4] = lp.Vertices[i];
                profile = new Region(lp2);
                surf = profile.ConvertToSurface();
                surf.EntityData = Name;
                surf.ColorMethod = colorMethodType.byEntity;
                surf.Color = System.Drawing.Color.White;
                Entities.Add(surf);
            }

        }
        //public SquareRoundInlet(string name)
        //{
        //    Width = DEFAULT_WIDTH;
        //    Height = DEFAULT_HEIGHT;
        //    Length = DEFAULT_LENGTH;
        //    Name = name;
        //    LinearPath lp = new LinearPath(7);
        //    lp.Vertices[0] = new Point3D(0, -Height / 2, -Width / 2);
        //    lp.Vertices[1] = new Point3D(0, -Height / 2, Width / 2);
        //    lp.Vertices[2] = new Point3D(0, Height / 2, Width / 2);
        //    lp.Vertices[3] = new Point3D(0, Height / 2, -Width / 2);
        //    lp.Vertices[4] = new Point3D(0, -Height / 2, -Width / 2);
        //    for (int i = 0; i < 4; ++i)
        //    {
        //        Surface surf = lp.ExtrudeAsSurface(Length, 0, 0)[i];
        //        surf.EntityData = Name;
        //        surf.ColorMethod = colorMethodType.byEntity;
        //        surf.Color = System.Drawing.Color.White;
        //        Entities.Add(surf);
        //    }
        //}
        //public SquareRoundInlet(string name, double length, double height, double width)
        //{
        //    Width = width;
        //    Height = height;
        //    Length = length;
        //    Name = name;
        //    LinearPath lp = new LinearPath(7);
        //    lp.Vertices[0] = new Point3D(0, -Height / 2, -Width / 2);
        //    lp.Vertices[1] = new Point3D(0, -Height / 2, Width / 2);
        //    lp.Vertices[2] = new Point3D(0, Height / 2, Width / 2);
        //    lp.Vertices[3] = new Point3D(0, Height / 2, -Width / 2);
        //    lp.Vertices[4] = new Point3D(0, -Height / 2, -Width / 2);
        //    for (int i = 0; i < 4; ++i)
        //    {
        //        Surface surf = lp.ExtrudeAsSurface(Length, 0, 0)[i];
        //        surf.EntityData = Name;
        //        surf.ColorMethod = colorMethodType.byEntity;
        //        surf.Color = System.Drawing.Color.White;
        //        Entities.Add(surf);
        //    }
        //}

    }
}