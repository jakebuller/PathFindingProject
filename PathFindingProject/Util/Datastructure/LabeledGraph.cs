using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Util.Datastructure {
public class LabeledGraph<VertexLabelType, EdgeLabelType> {

	/**
	 * Lookup for edge label information. Contains an entry for every vertex
	 * label.
	 */
	private readonly Dictionary<
            VertexLabelType, 
            Dictionary<VertexLabelType, EdgeLabelType>
        > m_globalEdgeLookup;
	/** List of the labels of all vertices within the graph. */
	private readonly List<VertexLabelType> m_vertexLabels;

	/** Creates a new empty graph. */
	public LabeledGraph() {
        m_globalEdgeLookup = new Dictionary<VertexLabelType, Dictionary<VertexLabelType, EdgeLabelType>>();
        m_vertexLabels = new List<VertexLabelType>();
	}

	/**
	 * Adds a new vertex to the graph if it is not already present.
	 * 
	 * @param v
	 *            the vertex to add
	 */
	public void AddVertex(VertexLabelType v) {
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
	public void Set(VertexLabelType from, VertexLabelType to, EdgeLabelType el) {
        Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup = CheckForNewVertex(from);
		localEdgeLookup[to] = el;
		CheckForNewVertex(to);
	}

	/** Handles new vertices. */
	private Dictionary<VertexLabelType, EdgeLabelType> CheckForNewVertex(
			VertexLabelType v
    ) {
        Dictionary<VertexLabelType, EdgeLabelType> result;
        if (!m_globalEdgeLookup.ContainsKey(v)) {
            result = new Dictionary<VertexLabelType, EdgeLabelType>();
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
	public void Remove(VertexLabelType from, VertexLabelType to) {
        if (m_globalEdgeLookup.ContainsKey(from)) {
            Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup =
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
	public EdgeLabelType Get(VertexLabelType from, VertexLabelType to) {
        EdgeLabelType ret = default(EdgeLabelType);

        if (m_globalEdgeLookup.ContainsKey(from)) {
            Dictionary<VertexLabelType, EdgeLabelType> localEdgeLookup =
                m_globalEdgeLookup[from];

            ret = localEdgeLookup[to];
        }

        return ret;
	}

	/**
	 * Returns the labels of those vertices which can be obtained by following
	 * the edges starting at the specified vertex.
	 */
	public List<VertexLabelType> GetSuccessors(VertexLabelType v) {
		List<VertexLabelType> result = new List<VertexLabelType>();
        if (m_globalEdgeLookup.ContainsKey(v)) {
            var localEdgeLookup = m_globalEdgeLookup[v];
            result.AddRange(localEdgeLookup.Keys);
        }
		
		return result;
	}

	/** Returns the labels of all vertices within the graph. */
	public List<VertexLabelType> GetVertexLabels() {
		return m_vertexLabels;
	}

	/** Checks whether the given label is the label of one of the vertices. */
	public bool IsVertexLabel(VertexLabelType v) {
        return m_globalEdgeLookup.ContainsKey(v);
	}

	/** Removes all vertices and all edges from the graph. */
	public void Clear() {
        m_vertexLabels.Clear();
		m_globalEdgeLookup.Clear();
	}
}
}
