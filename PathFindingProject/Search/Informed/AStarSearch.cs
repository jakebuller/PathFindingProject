using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Informed {
    public class AStarSearch : BestFirstSearch {
        public AStarSearch( 
			QueueSearch search,
			IHeuristicFunction heuristic
		) : base(
			search, 
			new AStarSearchEvaluationFunction( heuristic )
		) {}
    }
}