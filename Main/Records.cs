public record Edge(Node Start, Node End, int Distance);
public record Node(String NodeName);
public record DijkstraMap(int Distance, Node Parent);
public record ShortestPathData(List<string> NodeNames, int Distance);

public class EdgeBuilder
{
	private List<Edge> _edges = new List<Edge>();
	public List<Edge> Edges {get => _edges; }
	public EdgeBuilder AddBidirectionalEdge(Node from, Node to, int distance)
	{
		_edges.Add(new Edge(from, to, distance));
		_edges.Add(new Edge(to, from, distance));
		return this;
	}

	public EdgeBuilder AddEdge(Node from, Node to, int distance)
	{
		_edges.Add(new Edge(from, to, distance));
		return this;
	}
}