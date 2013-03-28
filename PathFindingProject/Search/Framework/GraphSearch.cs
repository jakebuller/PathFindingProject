//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using PathFindingProject.Agent;

//namespace PathFindingProject.Search.Framework {
//	public class GraphSearch : QueueSearch {

//	private HashSet<object> m_explored = new HashSet<object>();
//	private Dictionary<object, Node> m_frontierState = new Dictionary<object, Node>();
//	private IComparer<Node> m_replaceFrontierNodeAtStateCostFunction = null;
//	private List<Node> m_addToFrontier = new List<Node>();

//	// Need to override search() method so that I can re-initialize
//	// the m_explored set should multiple calls to search be made.
//	public override IEnumerable<IAction> Search( 
//		Problem problem, 
//		List<Node> frontier
//	) {
//		// initialize the m_explored set to be empty
//		m_explored.Clear();
//		m_frontierState.Clear();
//		return base.Search( problem, frontier );
//	}

//	public override Node PeekAtFrontier() {
//		Node toRemove = base.PeekAtFrontier();
//		m_frontierState.Remove( toRemove.State );
//		return toRemove;
//	}

//	public override bool RemoveFromFrontier( Node toRemove ) {
//		bool removed = base.RemoveFromFrontier(toRemove);
//		if( removed ) {
//			m_frontierState.Remove( toRemove.State );
//		}
//		return removed;
//	}

	
//	public override IEnumerable<Node> GetResultingNodesToAddToFrontier(
//		Node nodeToExpand,
//		Problem problem
//	) {

//		m_addToFrontier.Clear();
//		m_explored.Add( nodeToExpand.State );
//		// THIS SHIT DOESN'T WORK
//		foreach( Node cfn in new List<Node>() ) {
//			Node frontierNode = m_frontierState[cfn.State];
//			bool yesAddToFrontier = false;
//			if( frontierNode == null ) {
//				yesAddToFrontier = !m_explored.Contains( cfn.State );
//			} else if( m_replaceFrontierNodeAtStateCostFunction != null
//				&& m_replaceFrontierNodeAtStateCostFunction.Compare(cfn, frontierNode) < 0
//			) {
//				yesAddToFrontier = true;
//				RemoveFromFrontier( frontierNode );
//				m_addToFrontier.Remove( frontierNode );
//			}

//			if( yesAddToFrontier ) {
//				m_addToFrontier.Add( cfn );
//				m_frontierState[cfn.State] = cfn;
//			}
//		}

//		return m_addToFrontier;
//	}
//}
//}
