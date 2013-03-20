using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Environment.Map {
	public class MapStepCostFunction : IStepCostFunction {
		private readonly IMap m_map;

		//
		// Used by Uniform-cost search to ensure every step is greater than or equal
		// to some small positive constant
		private static double constantCost = 1.0;

		public MapStepCostFunction( IMap map ) {
			m_map = map;
		}

		//
		// START-StepCostFunction
		public double Cost( object fromCurrentState, IAction action, object toNextState ) {

			String fromLoc = fromCurrentState.ToString();
			String toLoc = toNextState.ToString();

			Double distance = m_map.GetDistance( fromLoc, toLoc );

			if( distance == null || distance <= 0 ) {
				return constantCost;
			}

			Double r = new Double();

			r = distance;

			return r;
		}

		// END-StepCostFunction
		//
	}
}
