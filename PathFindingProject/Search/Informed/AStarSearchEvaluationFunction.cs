using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Informed
{
    public class AStarSearchEvaluationFunction
    {
        private PathCostFunction gf = new PathCostFunction();
        private HeuristicFunction hf = null;

        public AStarSearchEvaluationFunction(HeuristicFunction hf) {
		    this.hf = hf;
	    }

        public double f(Node n){
            return gf.g(n) + hf.h(n.State);
        }


    }
}