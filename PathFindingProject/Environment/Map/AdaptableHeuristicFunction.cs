using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Environment.Map
{
    public abstract class AdaptableHeuristicFunction : IHeuristicFunction {
	    /** The Current Goal. */
	    protected Object m_goal;
	    /** The map to be used for distance to goal estimates. */
	    protected IMap m_map;

	    /**
	     * Modifies goal and map information and returns the modified heuristic
	     * function.
	     */
	    public AdaptableHeuristicFunction AdaptToGoal(Object goal, IMap map) {
		    m_goal = goal;
		    m_map = map;

			return this;
	    }

		public abstract double Calculate( object state ); 
    }
}
