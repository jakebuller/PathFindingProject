using System.Collections.Generic;
using System.Linq;

using PathFindingProject.Search.Framework;
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Informed {
    public class AStarSearch : ISearch {
        private readonly Problem m_problem;
        private IHeuristicFunction m_heuristic;

        public AStarSearch( Problem problem, IHeuristicFunction heuristic ) {
            this.m_problem = problem;
            this.m_heuristic = heuristic;
        }

        public bool IsFailure( List<IAction> result ) {
            return 0 == result.Count;
        }

        public virtual IEnumerable<IAction> Search( Problem problem ) {
            Queue<Node> frontier = new Queue<Node>();
			HashSet<Node> explored = new HashSet<Node>();

            Node root = new Node( problem.InitialState );
            frontier.Enqueue( root );
            while( frontier.Count > 0 ) {
				Node nodeToExpand = frontier.Dequeue();

				var newNodes = ExpandNode(
					nodeToExpand,
					explored,
					problem
				);

                foreach( Node fn in newNodes ) {
					if( IsGoalState( nodeToExpand.State, problem.GoalTest ) ) {

						return SearchUtils.ActionsFromNodes(
							nodeToExpand.GetPathFromRoot()
						);
					}

                    frontier.Enqueue( fn );
                }
            }

            return new List<IAction>();
        }

		private IEnumerable<Node> ExpandNode( 
			Node node, 
			HashSet<Node> explored, 
			Problem problem 
		) {

			var childNodes = new List<Node>();
			var actionsFunction = problem.ActionsFunction;
			var resultFunction = problem.ResultFunction;
			var stepCostFunction = problem.StepCostFunction;

			foreach( var action in actionsFunction.GetActions( node.State ) ) {
				string successorState = resultFunction.Result(
					node.State,
					action
				);

				double stepCost = stepCostFunction.Cost(
					node.State,
					action,
					successorState
				);
				var child = new Node( successorState, node, action, stepCost );
				if( !explored.Contains( child ) ) {
					childNodes.Add( child );
				}
			}

			return childNodes;
		}

		private bool IsGoalState( string state, IGoalTest goalTest ) {
			return goalTest.IsGoalState( state );
		}
        
    }

}