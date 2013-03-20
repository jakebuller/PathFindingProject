using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PathFindingProject.Util;
using PathFindingProject.Util.Datastructure;

namespace PathFindingProject.Environment.Map
{
public class ExtendableMap:Map {

	/**
	 * Stores map data. Locations are represented as vertices and connections
	 * (m_links) as directed edges labeled with corresponding travel distances.
	 */
	private readonly LabeledGraph<string, Double> m_links;

	/** Stores xy-coordinates for each location. */
    private readonly Dictionary<string, Point2D> m_locationPositions;

	/** Creates an empty map. */
	public ExtendableMap() {
		m_links = new LabeledGraph<string, Double>();
		m_locationPositions = new Dictionary<string, Point2D>();
	}

	/** Removes everything. */
	public void Clear() {
		m_links.Clear();
		m_locationPositions.Clear();
	}

	/** Clears all connections but keeps location position informations. */
	public void ClearLinks() {
		m_links.Clear();
	}

	/** Returns a list of all locations. */
	public List<string> GetLocations() {
		return m_links.GetVertexLabels();
	}

	/** Checks whether the given string is the name of a location. */
	public bool IsLocation(string str) {
		return m_links.IsVertexLabel(str);
	}

	/**
	 * Answers to the question: Where can I get, following one of the
	 * connections starting at the specified location?
	 */
	public List<string> GetLocationsLinkedTo(string fromLocation) {
		List<string> result = m_links.GetSuccessors(fromLocation);
        // FIX
		//Collections.sort(result);
		return result;
	}

	/**
	 * Returns the travel distance between the two specified locations if they
	 * are linked by a connection and null otherwise.
	 */
	public Double getDistance(string fromLocation, string toLocation) {
		//FIX THIS
        return m_links.Get(fromLocation, toLocation);
	}

	/** Adds a one-way connection to the map. */
	public void addUnidirectionalLink(string fromLocation, string toLocation,
			Double distance) {
		m_links.Set(fromLocation, toLocation, distance);
	}

	/**
	 * Adds a connection which can be traveled in both direction. Internally,
	 * such a connection is represented as two one-way connections.
	 */
	public void addBidirectionalLink(string fromLocation, string toLocation,
			Double distance) {
		m_links.Set(fromLocation, toLocation, distance);
		m_links.Set(toLocation, fromLocation, distance);
	}

	/**
	 * Returns a location which is selected by random.
	 */
	public string randomlyGenerateDestination() {
		return Util.selectRandomlyFromList(getLocations());
	}

	/** Removes a one-way connection. */
	public void removeUnidirectionalLink(string fromLocation, string toLocation) {
		m_links.Remove(fromLocation, toLocation);
	}

	/** Removes the two corresponding one-way connections. */
	public void removeBidirectionalLink(string fromLocation, string toLocation) {
		m_links.Remove(fromLocation, toLocation);
		m_links.Remove(toLocation, fromLocation);
	}

	/**
	 * Defines the position of a location as with respect to an orthogonal
	 * coordinate system.
	 */
	public void setPosition(string loc, double x, double y) {
		locationPositions.put(loc, new Point2D(x, y));
	}

	/**
	 * Defines the position of a location within the map. Using this method, one
	 * location should be selected as reference position (<code>dist=0</code>
	 * and <code>dir=0</code>) and all the other location should be placed
	 * relative to it.
	 * 
	 * @param loc
	 *            location name
	 * @param dist
	 *            distance to a reference position
	 * @param dir
	 *            bearing (compass direction) in which the location is seen from
	 *            the reference position
	 */
	public void setDistAndDirToRefLocation(string loc, double dist, int dir) {
		Point2D coords = new Point2D(-Math.sin(dir * Math.PI / 180.0) * dist,
				Math.cos(dir * Math.PI / 180.0) * dist);
		m_links.addVertex(loc);
		locationPositions.put(loc, coords);
	}

	/**
	 * Returns the position of the specified location as with respect to an
	 * orthogonal coordinate system.
	 */
	public Point2D getPosition(string loc) {
		return locationPositions.get(loc);
	}
}
}
