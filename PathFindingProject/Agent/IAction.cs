using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Agent {
	public interface IAction {
		bool IsNoOp();
	}
}
