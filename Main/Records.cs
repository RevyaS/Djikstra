public record Edge(Node Start, Node End, int Distance);
public record Node(string NodeName);
public record DijkstraMap(int Distance, Node Parent);
public record ShortestPathData(List<string> NodeNames, int Distance);

public class EdgeBuilder
{
	private List<Edge> _edges = new List<Edge>();
	public List<Edge> Edges {get => _edges; }
	public EdgeBuilder AddBidirectionalEdge(Node from, Node to, int distance)
	{
        addEdgeWhenNotExist(new Edge(from, to, distance));
		addEdgeWhenNotExist(new Edge(to, from, distance));
		return this;
	}

	public EdgeBuilder AddEdge(Node from, Node to, int distance)
	{
        addEdgeWhenNotExist(new Edge(from, to, distance));
		return this;
	}

    private void addEdgeWhenNotExist(Edge newEdge)
    {
        if(!_edges.Contains(newEdge))
        {
		    _edges.Add(newEdge);
        }
    }
}