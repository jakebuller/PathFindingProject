
namespace PathFindingProject.Search.Framework {
    public interface IHeuristicFunction {
        int Calculate( string state );
    }
}