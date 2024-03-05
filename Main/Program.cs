Node nodeA = new Node("A");
Node nodeB = new Node("B");
Node nodeC = new Node("C");
Node nodeD = new Node("D");
Node nodeE = new Node("E");
Node nodeF = new Node("F");
Node nodeG = new Node("G");
Node nodeH = new Node("H");
Node nodeI = new Node("I");

List<Node> nodeList = new List<Node> {
	nodeA,
	nodeB,
	nodeC,
	nodeD,
	nodeE,
	nodeF,
	nodeG,
	nodeH,
	nodeI
};

List<Edge> edgeList = new EdgeBuilder().AddBidirectionalEdge(nodeA, nodeB, 4)
									.AddBidirectionalEdge(nodeA, nodeC, 6)
									.AddBidirectionalEdge(nodeB, nodeF, 2)
									.AddBidirectionalEdge(nodeC, nodeD, 8)
									.AddBidirectionalEdge(nodeD, nodeE, 4)
									.AddBidirectionalEdge(nodeD, nodeG, 1)
									.AddBidirectionalEdge(nodeE, nodeF, 3)
									.AddBidirectionalEdge(nodeE, nodeI, 8)
									.AddBidirectionalEdge(nodeF, nodeH, 6)
									.AddBidirectionalEdge(nodeF, nodeG, 4)
									.AddBidirectionalEdge(nodeG, nodeH, 5)
									.AddBidirectionalEdge(nodeG, nodeI, 5)
									.AddEdge(nodeE, nodeB, 2)
									.Edges;

DijkstraComputer solver = new DijkstraComputer(edgeList);

void getOutput(Node from, Node to)
{
	ShortestPathData output = solver.ShortestPath(from, to, nodeList);
	Console.WriteLine(string.Format("From {0} to {1}:\nDistance: {2}\nPath: {3}", from.NodeName, to.NodeName, output.Distance, string.Join(", ", output.NodeNames)));
}


// Get Output here
getOutput(nodeB, nodeD);
