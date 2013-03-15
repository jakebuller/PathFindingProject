using System.Collections.Generic;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public class Node {
		
		private readonly object m_state;
		private readonly Node m_parent;
		private readonly IAction m_action;
		private readonly double m_pathCost;

		public Node( object state ) {
			m_state = state;
			m_pathCost = 0;
		}

		public Node( 
			object state,
			Node parent,
			IAction action,
			double stepCost
		) {
			m_state = state;
			m_parent = parent;
			m_action = action;
			m_pathCost = parent.PathCost + stepCost;
		}

		public object State {
			get {
				return m_state;
			}
		}

		public Node Parent {
			get {
				return m_parent;
			}
		}

		public IAction Action {
			get {
				return m_action;
			}
		}

		public double PathCost {
			get {
				return m_pathCost;
			}
		}

		public bool IsRootNode() {
			return m_parent == null;
		}

		public IEnumerable<Node> GetPathFromRoot() {
			var path = new Stack<Node>();
			var current = this;
			while (!current.IsRootNode()) {
				path.Push( current );
				current = current.Parent;
			}

			path.Push( current );
			return path;
		}

		public override string ToString() {
			return "[parent=" + m_parent + ", action=" + m_action + ", state="
					+ m_state + ", pathCost=" + m_pathCost + "]";
		}
	}
}
