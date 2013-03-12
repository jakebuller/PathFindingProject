using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Search.Framework
{
    public interface HeuristicFunction
    {
        double h(Object state);
    }
}