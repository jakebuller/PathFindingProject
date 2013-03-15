using System.Collections.Generic;

namespace PathFindingProject.Search.Framework {
	internal class NodeComparer : IComparer<Node> {

		private readonly IEvaluationFunction m_evalFunc;

		public NodeComparer( IEvaluationFunction evalFunc ) {
			m_evalFunc = evalFunc;
		}

		public int Compare( Node one, Node two ) {
			var fOne = m_evalFunc.Evaluate( one );
			var fTwo = m_evalFunc.Evaluate( two );

			return fOne.CompareTo( fTwo );
		}
	}
}
