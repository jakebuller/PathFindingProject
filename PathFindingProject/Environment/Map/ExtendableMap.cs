using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Domain;

namespace PathFindingProject.Environment.Map {
	public class ExtendableMap : IMap {

		private readonly Dictionary<
				string,
				Dictionary<string, double>
			> m_edgeLookup;

		private readonly Dictionary< string, Point> m_vertexLookup;

		public ExtendableMap() {
			m_edgeLookup = new Dictionary<string, Dictionary<string, double>>();
			m_vertexLookup = new Dictionary<string, Point>();
		}

		public List<string> GetLocations() {
			return m_edgeLookup.Keys.ToList();
		}

		public bool IsVertexLabel( string str ) {
			return m_edgeLookup.ContainsKey( str );
		}

		public List<string> GetVerticesLinkedTo( string fromLocation ) {
			List<string> result = new List<string>();
			if( m_edgeLookup.ContainsKey( fromLocation ) ) {
				result.AddRange( m_edgeLookup[fromLocation].Keys );
			}

			return result;
		}

		public double GetDistance( string fromLocation, string toLocation ) {
			var pOne = m_vertexLookup[fromLocation];
			var pTwo = m_vertexLookup[toLocation];
			return pOne.DistanceTo( pTwo );
		}

		public void AddUnidirectionalLink( 
			string fromLocation, 
			string toLocation,
			double distance
		) {
			m_edgeLookup[fromLocation][toLocation] = distance;
		}

		public void AddBidirectionalLink( 
			string fromLocation, 
			string toLocation,
			Double distance 
		) {
			m_edgeLookup[fromLocation][toLocation] = distance;
			m_edgeLookup[toLocation][fromLocation] = distance;
		}

		public void RemoveUnidirectionalLink( string fromLocation, string toLocation ) {
			m_edgeLookup[fromLocation].Remove( toLocation );
		}

		public void RemoveBidirectionalLink( string fromLocation, string toLocation ) {
			m_edgeLookup[fromLocation].Remove( toLocation );
			m_edgeLookup[toLocation].Remove( fromLocation );
		}

		public void SetPosition( string loc, int x, int y ) {
			m_vertexLookup[loc] = new Point( x, y );
		}

		public Point GetPosition( string loc ) {
			return m_vertexLookup[loc];
		}

		public void AddVertex( string label, int x, int y ) {
			AddVertex( label, new Point( x, y ) );
		}

		public void AddVertex( string label, Point p ) {
			m_vertexLookup.Add( label, p );
			m_edgeLookup.Add( label, new Dictionary<string, double>() );
		}
	}
}
