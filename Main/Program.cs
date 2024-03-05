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
.AddBidirectionalEdge(nodeD, nodeB, 12)
.AddBidirectionalEdge(nodeD, nodeC, 1)
.Edges;

DijkstraComputer solver = new DijkstraComputer(edgeList);

ShortestPathData output = solver.ShortestPath(nodeA, nodeD, nodeList);

Console.WriteLine(string.Format("From {0} to {1}:\nDistance: {2}\nPath: {3}", nodeA.NodeName, nodeD.NodeName, output.Distance, string.Join(", ", output.NodeNames)));