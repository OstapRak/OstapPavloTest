﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot.Entities;
using devDept.Geometry;


namespace WpfApplication1.Commponents
{
    class DShapeInlet : Component
    {
        private double DEFAULT_LENGTH = 2;
        private double DEFAULT_RADIUS = 5;
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
        public DShapeInlet() : this("", 5,2) { }
        public DShapeInlet(string name) : this(name, 5,2) { }
        public DShapeInlet(string name, double radius, double length)
        {
            Radius = radius;
            Length = length;
            Name = name;
            Circle circle = new Arc(Plane.YZ, new Point3D(0, 0, 0), Radius, Math.PI);
            Surface surf = circle.ExtrudeAsSurface(Length, 0, 0)[0];
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
            devDept.Eyeshot.Entities.Line line = new devDept.Eyeshot.Entities.Line(0, -Radius, 0, 0, Radius, 0);
            Surface surf1 = line.ExtrudeAsSurface(Length, 0, 0)[0];
            surf1.EntityData = Name;
            surf1.ColorMethod = colorMethodType.byEntity;
            surf1.Color = System.Drawing.Color.White;
            Entities.Add(surf1);
        }
    }
}
