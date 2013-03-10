using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public class SearchUtils {
		public static List<IAction> actionsFromNodes( List<Node> nodeList ) {
			List<IAction> actions = new List<IAction>();
			if ( 0 == nodeList.Count ) {
				// I'm at the root node, this indicates I started at the
				// Goal node, therefore just return a NoOp
				actions.Add( NoOpAction.NO_OP );
			} else {
				// ignore the root node this has no action
				// hence index starts from 1 not zero
				for ( int i = 1; i < nodeList.size(); i++ ) {
					Node node = nodeList.get( i );
					actions.add( node.getAction() );
				}
			}
			return actions;
		}

		public static bool isGoalState( Problem p, Node n ) {
		bool isGoal = false;
		IGoalTest gt = p.GoalTest;
		if( gt.IsGoalState( n.State ) ) {
			if( gt instanceof SolutionChecker ) {
				isGoal = ((SolutionChecker) gt).isAcceptableSolution(
						SearchUtils.actionsFromNodes(n.getPathFromRoot()),
						n.getState());
			} else {
				isGoal = true;
			}
		}
		return isGoal;
	}
	}
}
