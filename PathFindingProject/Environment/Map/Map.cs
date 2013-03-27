using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Domain;

namespace PathFindingProject.Environment.Map {
	public interface IMap {

		List<string> GetLocations();

		List<string> GetVerticesLinkedTo( string fromLocation );

		double GetDistance( string fromLocation, string toLocation );

		Point GetPosition( string loc );
	}
}
