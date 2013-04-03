using System.Collections.Generic;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public class Node {
		
		private readonly string m_state;
		private readonly Node m_parent;
		private readonly MoveToAction m_action;
		private readonly int m_pathCost;
		private readonly int m_estimateCost;

		public Node( string state ) {
			m_state = state;
			m_pathCost = 0;
		}

		public Node( 
			string state,
			Node parent,
			MoveToAction action,
			int stepCost,
			int estimate
		) {
			m_state = state;
			m_parent = parent;
			m_action = action;
			m_pathCost = parent.PathCost + stepCost;
			m_estimateCost = estimate;
		}

		public string State {
			get {
				return m_state;
			}
		}

		public Node Parent {
			get {
				return m_parent;
			}
		}

		public MoveToAction Action {
			get {
				return m_action;
			}
		}

		public int PathCost {
			get {
				return m_pathCost;
			}
		}

		public int EstimateCost {
			get {
				return m_estimateCost;
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

		public override bool Equals( object obj ) {
			if( obj == null ) {
				return false;
			}

			Node n = obj as Node;
			return Equals( n );
		}

		public bool Equals( Node n ) {
			if( ( object )n == null ) {
				return false;
			}

			return m_state == n.State;
		}

		public override int GetHashCode() {
			return m_state.GetHashCode();
		}

		public override string ToString() {
			return "[parent=" + m_parent + ", action=" + m_action + ", state="
					+ m_state + ", pathCost=" + m_pathCost + "]";
		}
	}
}
