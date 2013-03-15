using System;

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

	public virtual bool IsNoOp() {
		return false;
	}
	
	public override string DescribeType() {
		return typeof( IAction ).Name;
	}
}
}
