using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Environment.Map
{
    public abstract class AdaptableHeuristicFunction : IHeuristicFunction,
		    ICloneable {
	    /** The Current Goal. */
	    protected Object goal;
	    /** The map to be used for distance to goal estimates. */
	    protected Map map;

	    /**
	     * Modifies goal and map information and returns the modified heuristic
	     * function.
	     */
	    public AdaptableHeuristicFunction adaptToGoal(Object goal, Map map) {
		    this.goal = goal;
		    this.map = map;
		    return this;
	    }

	    // when subclassing: Don't forget to implement the most important method
	    // public double h(Object state)
    }
}
