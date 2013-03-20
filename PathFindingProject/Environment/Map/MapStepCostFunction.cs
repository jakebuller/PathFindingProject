using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent.IAction;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Environment.Map
{
    public class MapStepCostFunction : IStepCostFunction {
	    private Map map = null;

	    //
	    // Used by Uniform-cost search to ensure every step is greater than or equal
	    // to some small positive constant
	    private static double constantCost = 1.0;

	    public MapStepCostFunction(Map map) {
		    this.map = map;
	    }

	    //
	    // START-StepCostFunction
	    public double c(Object fromCurrentState, Action action, Object toNextState) {

		    String fromLoc = fromCurrentState.ToString();
		    String toLoc = toNextState.ToString();

		    Double distance = map.getDistance(fromLoc, toLoc);

		    if (distance == null || distance <= 0) {
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
