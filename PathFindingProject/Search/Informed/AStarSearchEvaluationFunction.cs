using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Informed {
    public class AStarSearchEvaluationFunction: IEvaluationFunction {
		private PathCostFunction m_pathCostFunc;
		private IHeuristicFunction m_heuristicFunc;

		public AStarSearchEvaluationFunction(
			PathCostFunction pathCostFunc,
			IHeuristicFunction heuristic
		) {
			m_pathCostFunc = pathCostFunc;
			m_heuristicFunc = heuristic;
		}

        public AStarSearchEvaluationFunction( IHeuristicFunction heuristic ) {
			m_pathCostFunc = new PathCostFunction();
			m_heuristicFunc = heuristic;
	    }

        public double Evaluate( Node node ){
			return m_pathCostFunc.Calculate( node ) + 
				m_heuristicFunc.Calculate( node.State );
        }


    }
}