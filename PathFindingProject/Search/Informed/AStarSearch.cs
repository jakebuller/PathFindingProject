using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Informed
{

    /**
	 * Constructs an A* search from the specified search problem and heuristic
	 * function
	 * 
	 * @param search
	 *            a search problem
	 * @param hf
	 *            a heuristic function <em>h(n)</em>, which estimates the cost
	 *            of the cheapest path from the state at node <em>n</em> to a
	 *            goal state.
	 */

    public class AStarSearch: BestFirstSearch {
        public AStarSearch(QueueSearch search, HeuristicFunction hf) : base(search, new AStarSearchEvaluationFunction(hf)) {}
    }
}