using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.ComponentsData
{
    public class ComponentData
    {

        public string Name
        {
            get;
            set;
        }
        public double X
        {
            get
            {
                return TransformationMatrix[0, 3];
            }

        }
        public double Y
        {
            get
            {
                return TransformationMatrix[1, 3];
            }

        }
        public double Z
        {
            get
            {
                return TransformationMatrix[2, 3];
            }
            
        }
        public double[,] TransformationMatrix = new double[4, 4];
        public ComponentData() : this("") { }
        public ComponentData(string name)
        {
            //X = 0;
            //Y = 0;
            //Z = 0;
            for(int i = 0; i < 4;++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (i == j && i != 4)
                    {
                        TransformationMatrix[i, j] = 1;
                    }
                    else
                    {
                        TransformationMatrix[i, j] = 0;
                    }
                }
            }
            Name = name;
        }
    }
}
