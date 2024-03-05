public class DijkstraComputer
{
    private List<Edge> _edges;

    public DijkstraComputer(List<Edge> edges)
    {
        _edges = edges;
    }

    public ShortestPathData ShortestPath(Node from, Node to, List<Node> nodes)
    {
        Dictionary<Node, DijkstraMap> mapping = createMapping(from, to, nodes, _edges);

        List<Node> nodeOrder = new List<Node>();
        for(Node currentNode = to; currentNode != from; currentNode = mapping[currentNode].Parent)
        {
            if(mapping[currentNode].Parent is null) break;
            nodeOrder.Add(currentNode);
        } 
        nodeOrder.Add(from);
        nodeOrder.Reverse();

        ShortestPathData output = new ShortestPathData(nodeOrder.Select(x => x.NodeName).ToList(), mapping[to].Distance);
        
        return output;
    }

    private Dictionary<Node, DijkstraMap> createMapping(Node from, Node to, List<Node> nodes, List<Edge> edges)
    {
        Dictionary<Node, DijkstraMap> mapping = nodes.Select(node => 
            (Node: node, Mapping: node == from ? new DijkstraMap(0, null) : new DijkstraMap(int.MaxValue, null)))
            .ToDictionary(pair => pair.Node, pair => pair.Mapping);

        List<Node> finalized = new List<Node>();
        List<Node> notFinalized = new List<Node>{from};

        while(notFinalized.Count > 0)
        {
            Node minimalDistanceNode = notFinalized.Select(node => (Node: node, Mapping: mapping[node]))
                                                .MinBy(x => x.Mapping.Distance).Node;
            notFinalized.Remove(minimalDistanceNode);
            finalized.Add(minimalDistanceNode);

            edges.Where(edge => edge.Start == minimalDistanceNode && !finalized.Contains(edge.End))
                .ToList()
                .ForEach(edge => {
                    Node end = edge.End;
                    int currentDistance = mapping[end].Distance;
                    int edgeDistance = edge.Distance;
                    int newDistance = mapping[minimalDistanceNode].Distance + edgeDistance;
                    if(newDistance < currentDistance)
                    {
                        mapping[end] = mapping[end] with { Distance = newDistance, Parent = minimalDistanceNode };
                        notFinalized.Add(end);
                    }
                });
        }

        return mapping;
    }
}