using System.Collections.Generic;
using System.Linq;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public abstract class QueueSearch : NodeExpander{

		public static string QueueSizeMetric = "queueSize";
		public static string MaxQueueSizeMetric = "maxQueueSize";
		public static string PathCostMetric = "pathCost";

		private List<Node> m_frontier;

		public bool CheckGoalBeforeAddingToFrontier { get; set; }

		public abstract IEnumerable<Node> GetResultingNodesToAddToFrontier(
			Node nodeToExpand,
			Problem p
		);

		public bool IsFailure( List<IAction> result ) {
			return 0 == result.Count;
		}

		public virtual Node PeekAtFrontier() {
			return m_frontier.First();
		}

		public virtual bool RemoveFromFrontier( Node node ) {
			return m_frontier.Remove( node );
		}

		public virtual IEnumerable<IAction> Search( 
			Problem problem,
			List<Node> frontier
		) {
			m_frontier = frontier;

			Node root = new Node( problem.InitialState );
			if ( CheckGoalBeforeAddingToFrontier ) {
				if ( SearchUtils.IsGoalState( problem, root ) ) {
					return SearchUtils.ActionsFromNodes( root.GetPathFromRoot() );
				}
			}
			frontier.Add( root );
			while( 0 != frontier.Count ) {//&& !CancelableThread.currIsCanceled() ) {
                Node nodeToExpand = m_frontier.First();
                m_frontier.RemoveAt( 0 );
				if( !CheckGoalBeforeAddingToFrontier ) {

					if( SearchUtils.IsGoalState( problem, nodeToExpand ) ) {
						
						return SearchUtils.ActionsFromNodes(
							nodeToExpand.GetPathFromRoot() 
						);
					}
				}

				foreach( Node fn in GetResultingNodesToAddToFrontier(
						nodeToExpand,
						problem 
					)
				) {
					if( CheckGoalBeforeAddingToFrontier ) {
						if( SearchUtils.IsGoalState( problem, fn ) ) {
							return SearchUtils.ActionsFromNodes(
								fn.GetPathFromRoot()
							);
						}
					}
					frontier.Add( fn );
				}
			}

			return new List<IAction>();
		}
	}
}
