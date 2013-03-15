using System.Collections.Generic;
using System.Linq;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public class SearchUtils {
		public static List<IAction> ActionsFromNodes( IEnumerable<Node> nodeList ) {
			var actions = new List<IAction>();
			if ( 0 == nodeList.Count() ) {
				// I'm at the root node, this indicates I started at the
				// Goal node, therefore just return a NoOp
				actions.Add( NoOpAction.NoOp );
			} else {
				// ignore the root node this has no action
				// hence index starts from 1 not zero
				foreach ( var node in nodeList ) {
					if ( node.Action == null ) {
						continue;
					}
					actions.Add( node.Action );
				}
			}
			return actions;
		}

		public static bool IsGoalState( Problem p, Node n ) {
			var isGoal = false;
			IGoalTest gt = p.GoalTest;
			if( gt.IsGoalState( n.State ) ) {
				ISoluctionChecker sc = gt as ISoluctionChecker;
				if( sc != null ) {
					isGoal = sc.IsAcceptableSolution(
						SearchUtils.ActionsFromNodes( n.GetPathFromRoot() ),
						n.State 
					);
				} else {
					isGoal = true;
				}
			}
			return isGoal;
		}
	}
}
