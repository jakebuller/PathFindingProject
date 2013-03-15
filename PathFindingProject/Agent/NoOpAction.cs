
namespace PathFindingProject.Agent {
	public class NoOpAction : DynamicAction {

	public static NoOpAction NoOp = new NoOpAction();

	public bool IsNoOp() {
		return true;
	}

	private NoOpAction() : base( "NoOp" ){}
}
}
