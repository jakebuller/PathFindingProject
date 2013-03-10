﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface IStepCostFunction {
		double Cost( object state, IAction action, object stateDelta );
	}
}