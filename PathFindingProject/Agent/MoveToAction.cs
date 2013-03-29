using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Agent {
    public class MoveToAction : IAction {

        public string TargetLocation { get; private set; }

        public MoveToAction( string tarLoc ) {
            TargetLocation = tarLoc;
        }

        public bool IsNoOp() {
            return false;
        }
    }
}
