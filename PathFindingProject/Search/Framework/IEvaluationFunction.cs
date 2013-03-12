using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Search.Framework {
	public interface IEvaluationFunction {
		double Evaluate( Node node );
	}
}
