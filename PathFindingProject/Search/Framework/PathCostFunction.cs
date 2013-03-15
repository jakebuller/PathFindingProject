
namespace PathFindingProject.Search.Framework {
    public class PathCostFunction {
        public PathCostFunction() { }

        public double Calculate( Node node ) {
			return node.PathCost;
        }

    }
}
