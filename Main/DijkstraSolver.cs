using System.Linq;

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
        int sum = 0;
        for(Node currentNode = to; currentNode != from; currentNode = mapping[currentNode].Parent)
        {
            if(mapping[currentNode].Parent is null) break;
            sum +=  mapping[currentNode].Distance;
            nodeOrder.Add(currentNode);
        } 
        nodeOrder.Add(from);

        nodeOrder.Reverse();

        ShortestPathData output = new ShortestPathData(nodeOrder.Select(x => x.NodeName).ToList(), sum);
        
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
            Node minimalDistanceNode = mapping.Where(mapping => notFinalized.Contains(mapping.Key))
                                            .MinBy(x => x.Value.Distance).Key;
            notFinalized.Remove(minimalDistanceNode);
            finalized.Add(minimalDistanceNode);

            edges.Where(edge => edge.Start == minimalDistanceNode && !finalized.Contains(edge.End))
                .ToList()
                .ForEach(edge => {
                    Node pairNode = edge.End;
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

        return mapping;
    }
}