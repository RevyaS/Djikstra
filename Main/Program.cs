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
										//    .AddBidirectionalEdge(nodeB, nodeD, 12)
										   .AddEdge(nodeD, nodeB, 12)

										//    .AddBidirectionalEdge(nodeC, nodeD, 1)
										   .AddEdge(nodeD, nodeC, 1)
										   .Edges;


DijkstraComputer solver = new DijkstraComputer(edgeList);

solver.ShortestPath(nodeA, nodeD, nodeList);