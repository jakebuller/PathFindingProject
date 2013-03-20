using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class SimpleStepCostFunction : IStepCostFunction {
		public double Cost( object state, IAction action, object stateDelta ) {
			return 1;
		}
	}
}
