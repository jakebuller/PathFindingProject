using System.Collections.Generic;
using System.Linq;

using PathFindingProject.Search.Framework;
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Informed {
    public class AStarSearch : ISearch {
        private readonly Problem m_problem;
        private IHeuristicFunction m_heuristic;

        public static string QueueSizeMetric = "queueSize";
        public static string MaxQueueSizeMetric = "maxQueueSize";
        public static string PathCostMetric = "pathCost";

        private List<Node> m_frontier; 
        private List<Node> m_addToFrontier = new List<Node>();
        private HashSet<object> m_explored = new HashSet<object>();
        private Dictionary<object, Node> m_frontierState = new Dictionary<object, Node>();
        private IComparer<Node> m_replaceFrontierNodeAtStateCostFunction = null;

        public bool CheckGoalBeforeAddingToFrontier { get; set; }

        public AStarSearch( Problem problem, IHeuristicFunction heuristic ) {
            this.m_problem = problem;
            this.m_heuristic = heuristic;
        }

        private IEnumerable<Node> ExpandNode( Node node, Problem problem ) {
            var childNodes = new List<Node>();
            var actionsFunction = problem.ActionsFunction;
            var resultFunction = problem.ResultFunction;
            var stepCostFunction = problem.StepCostFunction;

            foreach( var action in actionsFunction.GetActions( node.State ) ) {
                object successorState = resultFunction.Result(
                    node.State,
                    action
                );

                double stepCost = stepCostFunction.Cost(
                    node.State,
                    action,
                    successorState
                );

                childNodes.Add(
                    new Node( successorState, node, action, stepCost )
                );
            }

            return childNodes;
        }

        public IEnumerable<Node> GetResultingNodesToAddToFrontier(
                Node nodeToExpand,
                Problem problem
            ) {

            m_addToFrontier.Clear();
            m_explored.Add( nodeToExpand.State );
            // THIS SHIT DOESN'T WORK
            foreach( Node cfn in new List<Node>() ) {
                Node frontierNode = m_frontierState[cfn.State];
                bool yesAddToFrontier = false;
                if( frontierNode == null ) {
                    yesAddToFrontier = !m_explored.Contains( cfn.State );
                } else if( m_replaceFrontierNodeAtStateCostFunction != null
                    && m_replaceFrontierNodeAtStateCostFunction.Compare( cfn, frontierNode ) < 0
                ) {
                    yesAddToFrontier = true;
                    RemoveFromFrontier( frontierNode );
                    m_addToFrontier.Remove( frontierNode );
                }

                if( yesAddToFrontier ) {
                    m_addToFrontier.Add( cfn );
                    m_frontierState[cfn.State] = cfn;
                }
            }

            return m_addToFrontier;
        }

        public bool IsFailure( List<IAction> result ) {
            return 0 == result.Count;
        }

        public virtual Node PeekAtFrontier() {
            return m_frontier.First();
        }

        public virtual bool RemoveFromFrontier( Node node ) {
            return m_frontier.Remove( node );
        }

        public virtual IEnumerable<IAction> Search( Problem problem ) {
            List<Node> frontier = new List<Node>();
            m_frontier = frontier;

            Node root = new Node( problem.InitialState );
            if( CheckGoalBeforeAddingToFrontier ) {
                if( SearchUtils.IsGoalState( problem, root ) ) {
                    return SearchUtils.ActionsFromNodes( root.GetPathFromRoot() );
                }
            }
            frontier.Add( root );
            while( 0 != frontier.Count ) {
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