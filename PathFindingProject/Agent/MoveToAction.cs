
namespace PathFindingProject.Agent {
    public class MoveToAction {

        public string TargetLocation { get; private set; }

        public MoveToAction( string tarLoc ) {
            TargetLocation = tarLoc;
        }
    }
}
