﻿using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    public class RoundInlet : Component
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
        public RoundInlet() : this("", 5,2) { }
        public RoundInlet(string name) : this(name, 5,2) { }
        public RoundInlet(string name, double radius, double length)
        {
            Radius = radius;
            Length = length;
            Name = name;
            Circle circle = new Circle(Plane.YZ, Radius);
            Surface surf = circle.ExtrudeAsSurface(Length, 0, 0)[0];
            surf.EntityData = Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            Entities.Add(surf);
        }
    }
}
