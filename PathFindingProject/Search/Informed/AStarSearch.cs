﻿using System.Collections.Generic;
using System.Linq;
using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

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
			List<Node> frontier = new List<Node>();
			HashSet<Node> explored = new HashSet<Node>();

            Node root = new Node( problem.InitialState );
            frontier.Add( root );
            while( frontier.Count > 0 ) {
				Node nodeToExpand = frontier.First();
				frontier.Remove( nodeToExpand );

				var newNodes = ExpandNode(
					nodeToExpand,
					explored,
					problem
				);

                foreach( Node fn in newNodes ) {
					var test = fn.Equals( newNodes.First() );
					if( IsGoalState( fn.State, problem.GoalTest ) ) {

						return ActionsFromNodes( fn.GetPathFromRoot() );
					}

                    frontier.Add( fn );
                }

				// This could be costly.  Find better alternative later
				frontier = frontier
					.OrderBy( n => n.PathCost + n.EstimateCost )
					.Distinct()
					.ToList();
            }

            return new List<IAction>();
        }

		private IEnumerable<Node> ExpandNode( 
			Node node, 
			HashSet<Node> explored, 
			Problem problem 
		) {
			explored.Add( node );

			var childNodes = new List<Node>();
			var actionsFunction = problem.ActionsFunction;
			var resultFunction = problem.ResultFunction;
			var stepCostFunction = problem.StepCostFunction;

			foreach( var action in actionsFunction.GetActions( node.State ) ) {
				string successorState = resultFunction.Result(
					node.State,
					action
				);

				int stepCost = stepCostFunction.Cost(
					node.State,
					action,
					successorState
				);
				int estimateCost = m_heuristic.Calculate( successorState );
				var child = new Node( 
					successorState,
					node,
					action,
					stepCost,
					estimateCost
				);
				if( !explored.Contains( child ) ) {
					childNodes.Add( child );
				}
			}

			return childNodes;
		}

		private bool IsGoalState( string state, IGoalTest goalTest ) {
			return goalTest.IsGoalState( state );
		}

		private List<IAction> ActionsFromNodes( IEnumerable<Node> nodeList ) {
			var actions = new List<IAction>();
			if( 0 == nodeList.Count() ) {
				// I'm at the root node, this indicates I started at the
				// Goal node, therefore just return a NoOp
				actions.Add( null );
			} else {
				// ignore the root node this has no action
				// hence index starts from 1 not zero
				foreach( var node in nodeList ) {
					if( node.Action == null ) {
						continue;
					}
					actions.Add( node.Action );
				}
			}
			return actions;
		}        
    }

}