//using System.Collections.Generic;

//using PathFindingProject.Agent;

//namespace PathFindingProject.Search.Framework {
//	public class PrioritySearch : ISearch {

//		private readonly QueueSearch m_search;
//		private readonly IComparer<Node> m_comparer;

//		public PrioritySearch( QueueSearch search, IComparer<Node> comparer ) {
//			m_search = search;
//		}

//		public IEnumerable<IAction> Search( Problem problem ) {
//			return m_search.Search( problem, new List<Node>() );
//		}
//	}
//}
