using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;
using PathFindingProject.Environment.Map;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class StringStateActionsFunction : IActionsFunction  {
		private readonly ExtendableMap m_map;

		public StringStateActionsFunction( ExtendableMap map ) {
			m_map = map;
		}

		public HashSet<IAction> GetActions( object state ) {
			var actions = new HashSet<IAction>();

			// states are strings of the form x,y
			var strState = ( string )state;
			var neighbours = m_map.GetLocationsLinkedTo( strState );

			foreach( var neighbour in neighbours ) {
				actions.Add( new MoveToAction( neighbour ) );
			}

			return actions;
		}
	}
}
