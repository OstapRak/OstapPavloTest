﻿using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    class RoundBending : Component
    {
        private double DEFAULT_ANGLE = Math.PI/2;
        private double DEFAULT_RADIUS = 5;
        public double Radius
        {
            get;
            set;
        }
        public double Angle
        {
            get;
            set;
        }
        public RoundBending() : this("", 5, Math.PI / 2) { }
        public RoundBending(string name) : this(name, 5, Math.PI / 2) { }
        public RoundBending(string name, double radius, double angle)
        {
            Radius = radius;
            Angle = angle;
            Name = name;
            Circle circle = new Circle(Plane.YZ, Radius);
            Vector3D axis = Vector3D.AxisZ;
            Point3D center = new Point3D(0, Radius, 0);
            Surface s1 = circle.RevolveAsSurface(0, Angle, axis, center)[0];
            s1.ColorMethod = colorMethodType.byEntity;
            s1.Color = System.Drawing.Color.Green;
            s1.EntityData = Name;
            Entities.Add(s1);
        }
    }
}
