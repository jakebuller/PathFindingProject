using System.Collections.Generic;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
    public interface ISoluctionChecker : IGoalTest {
        bool IsAcceptableSolution( IEnumerable<MoveToAction> actions, object goal );
    }
}
