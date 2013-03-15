
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