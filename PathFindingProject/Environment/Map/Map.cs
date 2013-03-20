using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Domain;

namespace PathFindingProject.Environment.Map {
	public interface IMap {

		/** Returns a list of all locations. */
		List<string> GetLocations();

		/**
		 * Answers to the question: Where can I get, following one of the
		 * connections starting at the specified location?
		 */
		List<string> GetLocationsLinkedTo( string fromLocation );

		/**
		 * Returns the travel distance between the two specified locations if they
		 * are linked by a connection and null otherwise.
		 */
		double GetDistance( string fromLocation, string toLocation );

		/**
		 * Returns the position of the specified location. The position is
		 * represented by two coordinates, e.g. latitude and longitude values.
		 */
		Point GetPosition( string loc );

		/**
		 * Returns a location which is selected by random.
		 */
		//string RandomlyGenerateDestination();
	}
}
