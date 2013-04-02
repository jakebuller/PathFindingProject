using System.Collections.Generic;

using PathFindingProject.Search.Domain;

namespace PathFindingProject.Environment.Map {
	public interface IMap {

		List<string> GetLocations();

		List<string> GetVerticesLinkedTo( string fromLocation );

		double GetDistance( string fromLocation, string toLocation );

		Point GetPosition( string loc );
	}
}
