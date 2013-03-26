using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Util.Datastructure {
public class LabeledGraph<K, V> {

	/**
	 * Lookup for edge label information. Contains an entry for every vertex
	 * label.
	 */
	private readonly Dictionary<
            K, 
            Dictionary<K, V>
        > m_globalEdgeLookup;
	/** List of the labels of all vertices within the graph. */
	private readonly List<K> m_vertexLabels;

	/** Creates a new empty graph. */
	public LabeledGraph() {
        m_globalEdgeLookup = new Dictionary<K, Dictionary<K, V>>();
        m_vertexLabels = new List<K>();
	}

	/**
	 * Adds a new vertex to the graph if it is not already present.
	 * 
	 * @param v
	 *            the vertex to add
	 */
	public void AddVertex(K v) {
		CheckForNewVertex(v);
	}

	/**
	 * Adds a directed labeled edge to the graph. The end points of the edge are
	 * specified by vertex labels. New vertices are automatically identified and
	 * added to the graph.
	 * 
	 * @param from
	 *            the first vertex of the edge
	 * @param to
	 *            the second vertex of the edge
	 * @param el
	 *            an edge label
	 */
	public void Set(K from, K to, V el) {
        Dictionary<K, V> localEdgeLookup = CheckForNewVertex(from);
		localEdgeLookup[to] = el;
		CheckForNewVertex(to);
	}

	/** Handles new vertices. */
	private Dictionary<K, V> CheckForNewVertex(
			K v
    ) {
        Dictionary<K, V> result;
        if (!m_globalEdgeLookup.ContainsKey(v)) {
            result = new Dictionary<K, V>();
            m_globalEdgeLookup[v] = result;
            m_vertexLabels.Add(v);
        }

		result = m_globalEdgeLookup[v];
		
        return result;
	}

	/**
	 * Removes an edge from the graph.
	 * 
	 * @param from
	 *            the first vertex of the edge
	 * @param to
	 *            the second vertex of the edge
	 */
	public void Remove(K from, K to) {
        if (m_globalEdgeLookup.ContainsKey(from)) {
            Dictionary<K, V> localEdgeLookup =
                m_globalEdgeLookup[from];
            localEdgeLookup.Remove(to);
        }
	}

	/**
	 * Returns the label of the edge between the specified vertices, or null if
	 * there is no edge between them.
	 * 
	 * @param from
	 *            the first vertex of the ege
	 * @param to
	 *            the second vertex of the edge
	 * 
	 * @return the label of the edge between the specified vertices, or null if
	 *         there is no edge between them.
	 */
	public V Get(K from, K to) {
        V ret = default(V);

        if (m_globalEdgeLookup.ContainsKey(from)) {
            Dictionary<K, V> localEdgeLookup =
                m_globalEdgeLookup[from];

            ret = localEdgeLookup[to];
        }

        return ret;
	}

	/**
	 * Returns the labels of those vertices which can be obtained by following
	 * the edges starting at the specified vertex.
	 */
	public List<K> GetSuccessors(K v) {
		List<K> result = new List<K>();
        if (m_globalEdgeLookup.ContainsKey(v)) {
            var localEdgeLookup = m_globalEdgeLookup[v];
            result.AddRange(localEdgeLookup.Keys);
        }
		
		return result;
	}

	/** Returns the labels of all vertices within the graph. */
	public List<K> GetVertexLabels() {
		return m_vertexLabels;
	}

	/** Checks whether the given label is the label of one of the vertices. */
	public bool IsVertexLabel(K v) {
        return m_globalEdgeLookup.ContainsKey(v);
	}

	/** Removes all vertices and all edges from the graph. */
	public void Clear() {
        m_vertexLabels.Clear();
		m_globalEdgeLookup.Clear();
	}
}
}
