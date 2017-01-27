using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Commponents
{
    public class Component
    {
        public string Name
        {
            get;
            set;
        }
        public Transformation Transform
        {
            get;
            set;
        }

        public List<Entity> Entities
        {
            get;
            set;
        }

        public Component()
        {
            Transform = new Translation(0,0,0);
            Entities = new List<Entity>();
            Name = "";
        }
        public Component(string name)
        {
            Transform = new Translation(0, 0, 0);
            Entities = new List<Entity>();
            Name = name;
        }
    }
}
