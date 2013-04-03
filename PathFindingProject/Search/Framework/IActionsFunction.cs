using System.Collections.Generic;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
    public interface MoveToActionsFunction {
        HashSet<MoveToAction> GetActions( string state );
    }
}