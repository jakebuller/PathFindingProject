using System.Collections.Generic;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Informed {
	public class BestFirstSearch : PrioritySearch {

		public BestFirstSearch( 
			QueueSearch search,
			IEvaluationFunction evalFunc
		) : base (search, CreateComparer(evalFunc) ) {
		}

		private static IComparer<Node> CreateComparer( IEvaluationFunction evalFunc ) {
			return new NodeComparer( evalFunc );
		}
	}
}
