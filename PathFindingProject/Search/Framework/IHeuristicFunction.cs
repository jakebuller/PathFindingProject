
namespace PathFindingProject.Search.Framework {
    public interface IHeuristicFunction {
        double Calculate( object state );
    }
}