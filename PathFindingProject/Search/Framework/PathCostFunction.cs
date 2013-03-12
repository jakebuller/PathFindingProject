using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Search.Framework
{
    public class PathCostFunction
    {
        public PathCostFunction() { }

        public double g(Node n)
        {
            return n.pathCost;
        }

    }
}
