using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
    public interface IPerceptToStateFunction
    {

        /**
         * Get the problem state associated with a Percept.
         * 
         * @param p
         *            the percept to be transformed to a problem state.
         * @return a problem state derived from the Percept p.
         */
        Object GetState(IPercept p);
    }
}
