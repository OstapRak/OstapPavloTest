using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.ComponentsData
{
    public static class ComponentFactory
    {
        public static List<Entity> GenerateRoundDuct(RoundDuctData data)
        {
            List<Entity> entities = new List<Entity>();
            Circle circle = new Circle(Plane.YZ, data.Radius);
            Surface surf = circle.ExtrudeAsSurface(data.Length, 0, 0)[0];
            surf.EntityData = data.Name;
            surf.ColorMethod = colorMethodType.byEntity;
            surf.Color = System.Drawing.Color.White;
            entities.Add(surf);
            Mesh cube = Mesh.CreateBox(data.Radius * 2, data.Radius * 2, data.Radius * 2);
            cube.EntityData = "cube" + data.Name;
            cube.ColorMethod = colorMethodType.byEntity;
            cube.Color = System.Drawing.Color.FromArgb(100, 0, 255, 0);
            cube.Translate( 0, -data.Radius, -data.Radius);
            cube.Visible = false;
            entities.Add(cube);
            cube = Mesh.CreateBox(data.Radius * 2, data.Radius * 2, data.Radius * 2);
            cube.EntityData = "cube" + data.Name;
            cube.ColorMethod = colorMethodType.byEntity;
            cube.Color = System.Drawing.Color.FromArgb(0, 0,255,0);
            cube.Visible = false;
            cube.Translate(data.Length - 2 * data.Radius, -data.Radius, -data.Radius);
            entities.Add(cube);
            return entities;
        }
        
    }
}
