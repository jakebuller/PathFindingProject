
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
    public interface IResultFunction {
        string Result( string state, IAction action );
    }
}
