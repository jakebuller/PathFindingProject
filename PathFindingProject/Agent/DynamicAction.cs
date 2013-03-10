using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Agent {
	public class DynamicAction : ObjectWithDynamicAttributes, IAction {
	public static string AttributeName = "name";

	//

	public DynamicAction(String name) {
		this.SetAttribute( AttributeName, name );
	}

	/**
	 * Returns the value of the name attribute.
	 * 
	 * @return the value of the name attribute.
	 */
	public string getName() {
		return (string) GetAttribute( AttributeName );
	}

	public bool IsNoOp() {
		return false;
	}
	
	public override string DescribeType() {
		return typeof( IAction ).Name;
	}
}
}
