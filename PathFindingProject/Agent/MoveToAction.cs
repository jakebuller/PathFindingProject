using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Agent {
	public class MoveToAction : DynamicAction {
		public static string MoveToLocationAttribute = "location";

		public MoveToAction( string location ) : base( "moveTo" ) {
			SetAttribute( MoveToLocationAttribute, location );
		}

		public string GetToLocation() {
			return ( string )GetAttribute( MoveToLocationAttribute );
		}
	}
}
