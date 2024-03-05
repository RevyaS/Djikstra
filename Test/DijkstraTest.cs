
namespace Test;

[TestClass]
public class DijkstraTest
{
    [TestMethod]
    public void TestBidirectional()
    {
        Node nodeA = new Node("A");
        Node nodeB = new Node("B");
        Node nodeC = new Node("C");
        Node nodeD = new Node("D");

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
        
        DijkstraComputer _computer = new DijkstraComputer(edgeList);
        
        ShortestPathData shortestPathData = _computer.ShortestPath(nodeA, nodeD, nodeList);

        CollectionAssert.AreEqual(shortestPathData.NodeNames, new List<string>{"A", "C", "D"});
        Assert.AreEqual(shortestPathData.Distance, 16);
    }

    [TestMethod]
    public void TestUnidirectional()
    {
        Node nodeA = new Node("A");
        Node nodeB = new Node("B");
        Node nodeC = new Node("C");
        Node nodeD = new Node("D");

        List<Node> nodeList = new List<Node> { 
            nodeA,
            nodeB,
            nodeC,
            nodeD
        };

        List<Edge> edgeList = new EdgeBuilder().AddBidirectionalEdge(nodeA, nodeB, 16)
                                               .AddBidirectionalEdge(nodeA, nodeC, 12)
                                               .AddBidirectionalEdge(nodeD, nodeB, 5)
                                               .AddEdge(nodeC, nodeB, 3)
                                               .AddEdge(nodeD, nodeC, 1)
                                               .Edges;
        
        DijkstraComputer _computer = new DijkstraComputer(edgeList);
        
        ShortestPathData shortestPathData = _computer.ShortestPath(nodeD, nodeA, nodeList);

        CollectionAssert.AreEqual(shortestPathData.NodeNames, new List<string>{"D", "C", "A"});
        Assert.AreEqual(shortestPathData.Distance, 13);
    }

    [TestMethod]
    public void TestUnidirectionalNoResult()
    {
        Node nodeA = new Node("A");
        Node nodeB = new Node("B");
        Node nodeC = new Node("C");
        Node nodeD = new Node("D");

        List<Node> nodeList = new List<Node> { 
            nodeA,
            nodeB,
            nodeC,
            nodeD
        };

        List<Edge> edgeList = new EdgeBuilder().AddBidirectionalEdge(nodeA, nodeB, 16)
                                               .AddBidirectionalEdge(nodeA, nodeC, 12)
                                               .AddEdge(nodeD, nodeB, 5)
                                               .AddEdge(nodeD, nodeC, 1)
                                               .Edges;
        
        DijkstraComputer _computer = new DijkstraComputer(edgeList);
        
        ShortestPathData shortestPathData = _computer.ShortestPath(nodeA, nodeD, nodeList);

        CollectionAssert.AreEqual(shortestPathData.NodeNames, new List<string>{"A"});
        Assert.AreEqual(shortestPathData.Distance, int.MaxValue);
    }
}