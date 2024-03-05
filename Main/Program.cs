// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Linq;

Node nodeA = new Node("A");
Node nodeB = new Node("B");
Node nodeC = new Node("C");
Node nodeD = new Node("D");
Node none = new Node("_NONE");

List<Node> nodeList = new List<Node> { 
	nodeA,
	nodeB,
	nodeC,
	nodeD
};

List<Edge> edgeList = new EdgeBuilder().AddBidirectionalEdge(nodeA, nodeB, 10)
		   								   .AddBidirectionalEdge(nodeA, nodeC, 15)
										   .AddBidirectionalEdge(nodeB, nodeD, 12)
										   .AddBidirectionalEdge(nodeC, nodeD, 1)
										   .Edges;

// List<Edge> edgeList = new List<Edge> {
// 	new Edge(nodeA, nodeB, 10),
// 	new Edge(nodeA, nodeC, 15),
// 	new Edge(nodeB, nodeD, 12),
// 	new Edge(nodeC, nodeD, 1)
// };


ShortestPathData ShortestPath(Node from, Node to, List<Node> nodes, List<Edge> edges)
{
	Dictionary<Node, DjikstraMap> mapping = nodes.Select(node => 
		(Node: node, Mapping: node == from ? new DjikstraMap(0, null) : new DjikstraMap(int.MaxValue, null)))
		.ToDictionary(pair => pair.Node, pair => pair.Mapping);

	List<Node> finalized = new List<Node>();
	List<Node> notFinalized = new List<Node>{from};

	while(notFinalized.Count > 0)
	{
		Node minimalDistanceNode = mapping.Where(mapping => notFinalized.Contains(mapping.Key))
										  .MinBy(x => x.Value.Distance).Key;
		notFinalized.Remove(minimalDistanceNode);
		finalized.Add(minimalDistanceNode);

		edges.Where(edge => edge.HasNode(minimalDistanceNode) && !finalized.Contains(edge.PairOf(minimalDistanceNode)))
			 .ToList()
			 .ForEach(edge => {
				Node pairNode = edge.PairOf(minimalDistanceNode);
				int currentDistance = mapping[pairNode].Distance;
				int edgeDistance = edge.Distance;
				int newDistance = mapping[minimalDistanceNode].Distance + edgeDistance;
				if(newDistance < currentDistance)
				{
					mapping[pairNode] = mapping[pairNode] with { Distance = newDistance, Parent = minimalDistanceNode };
					notFinalized.Add(pairNode);
				}
			 });
	}

	mapping.Select(x => (Node: x.Key, Distance: x.Value)).ToList().ForEach(x => Console.WriteLine(string.Format("{0}: {1}", x.Node, x.Distance)));


	return null;
}

ShortestPath(nodeA, nodeB, nodeList, edgeList);

public record Edge(Node Start, Node End, int Distance)
{
	public bool HasNode(Node node) => Start == node || End == node;
	public Node PairOf(Node basis) => Start == basis ? End : End == basis ? Start : null;
}

public record Node(String NodeName);

public record DjikstraMap(int Distance, Node Parent);

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