using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Environment.Map {
	public class MapFunctionFactory {
		private static IResultFunction m_resultFunction = null;
		private static IPerceptToStateFunction m_perceptToStateFunction = null;

		public static IActionsFunction getActionsFunction( IMap map ) {
			return new MapActionsFunction( map );
		}

		public static IResultFunction getResultFunction() {
			if( null == m_resultFunction ) {
				m_resultFunction = new MapResultFunction();
			}
			return m_resultFunction;
		}

		private class MapActionsFunction : IActionsFunction {
			private readonly IMap m_map;

			public MapActionsFunction( IMap map ) {
				m_map = map;
			}

			public HashSet<IAction> GetActions( Object state ) {
				HashSet<IAction> actions = new HashSet<IAction>();
				string location = state.ToString();

				List<string> linkedLocations = m_map.GetVerticesLinkedTo( location );
				foreach( string linkLoc in linkedLocations ) {
					actions.Add( new MoveToAction( linkLoc ) );
				}

				return actions;
			}
		}

		public static IPerceptToStateFunction getPerceptToStateFunction() {
			if( null == m_perceptToStateFunction ) {
				m_perceptToStateFunction = new MapPerceptToStateFunction();
			}
			return m_perceptToStateFunction;
		}

		private class MapResultFunction : IResultFunction {
			public MapResultFunction() {
			}

			public object Result( object s, IAction a ) {

				if( a.GetType() == typeof( MoveToAction ) ) {
					MoveToAction mta = ( MoveToAction )a;

					return mta.TargetLocation;
				}

				// The Action is not understood or is a NoOp
				// the result will be the current state.
				return s;
			}
		}

		private class MapPerceptToStateFunction :
				IPerceptToStateFunction {
			public object GetState( IPercept p ) {
				return ( ( DynamicPercept )p )
						.GetAttribute( "in" );
			}
		}
	}
}
