using System.Collections.Generic;

using PathFindingProject.Agent;
using PathFindingProject.Environment.Map;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class ActionsFunction : IActionsFunction  {
		private readonly ExtendableMap m_map;

		public ActionsFunction( ExtendableMap map ) {
			m_map = map;
		}

		public HashSet<IAction> GetActions( string state ) {
			var actions = new HashSet<IAction>();
			var neighbours = m_map.GetVerticesLinkedTo( state );

			foreach( var neighbour in neighbours ) {
				actions.Add( new MoveToAction( neighbour ) );
			}

			return actions;
		}
	}
}
