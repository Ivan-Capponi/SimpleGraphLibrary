using GraphLibrary;
using NUnit.Framework;

namespace GraphLibraryTests
{
    [TestFixture]
    public class SimpleDirectedGraphTests
    {
        private IGraph<string, string> _simpleDirectedGraph;

        [SetUp]
        public void SetUp()
        {
            _simpleDirectedGraph = new SimpleDirectedGraph<string, string>();
            _simpleDirectedGraph.AddVertex(new[] {"A", "B", "C"});
        }

        [Test]
        public void GetWeigthReturnsOk()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "Label");
            Assert.That(_simpleDirectedGraph.GetWeigth("A", "B"), Is.EqualTo("Label"));
        }

        [Test]
        public void GetWeigthThrowsArgumentNullException()
        {
            Assert.That(() => _simpleDirectedGraph.GetWeigth(null, "B"), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.GetWeigth("A", null), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.GetWeigth(null, null), Throws.ArgumentNullException);
        }

        [Test]
        public void GetWeigthThrowsArgumentException()
        {
            Assert.That(() => _simpleDirectedGraph.GetWeigth("A", "B"), Throws.ArgumentException);
        }

        [Test]
        public void AddEdgeThrowsArgumentNullException()
        {
            Assert.That(() => _simpleDirectedGraph.AddEdge(null, "B", "Label"), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.AddEdge("A", null, "Label"), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.AddEdge("A", "B", null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddEdgeNonExistingVertexReturnsFalse()
        {
            Assert.That(_simpleDirectedGraph.AddEdge("A", "D", "Label"), Is.False);
            Assert.That(_simpleDirectedGraph.AddEdge("D", "A", "Label"), Is.False);
        }

        [Test]
        public void AddEdgeReturnsTrue()
        {
            Assert.That(_simpleDirectedGraph.AddEdge("A", "B", "Label"), Is.True);
            Assert.That(_simpleDirectedGraph.AddEdge("B", "A", "Label2"), Is.True);
            Assert.That(_simpleDirectedGraph.GetEdgeSet(), Contains.Item(new PairValue<string>("A", "B")));
            Assert.That(_simpleDirectedGraph.GetEdgeSet(), Contains.Item(new PairValue<string>("B", "A")));
            Assert.That(_simpleDirectedGraph.GetWeigth("A", "B"), Is.EqualTo("Label"));
            Assert.That(_simpleDirectedGraph.GetWeigth("B", "A"), Is.EqualTo("Label2"));
        }

        [Test]
        public void AddEdgeAlreadyExistingPairReturnsFalse()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "Label");
            Assert.That(_simpleDirectedGraph.AddEdge("A", "B", "Label"), Is.False);
            Assert.That(_simpleDirectedGraph.AddEdge("A", "B", "Label2"), Is.False);
        }

        [Test]
        public void DeleteEdgeThrowsArgumentNullException()
        {
            Assert.That(() => _simpleDirectedGraph.DeleteEdge("A", null), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.DeleteEdge(null, "B"), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.DeleteEdge(null, null), Throws.ArgumentNullException);
        }

        [Test]
        public void DeleteEdgeNonExistingPairReturnsFalse()
        {
            Assert.That(_simpleDirectedGraph.DeleteEdge("A", "B"), Is.False);
        }

        [Test]
        public void DeleteEdgeReturnsOkAndEdgeSetChanges()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "Label");
            Assert.That(_simpleDirectedGraph.DeleteEdge("A", "B"), Is.True);
            Assert.That(_simpleDirectedGraph.GetEdgeSet(), Is.Empty);
            Assert.That(() => _simpleDirectedGraph.GetWeigth("A", "B"), Throws.ArgumentException);
        }

        [Test]
        public void AreAdjacentReturnsTrue()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "L1");
            _simpleDirectedGraph.AddEdge("A", "C", "L2");
            Assert.That(_simpleDirectedGraph.AreAdjacent("A", "B"), Is.True);
            Assert.That(_simpleDirectedGraph.AreAdjacent("A", "C"), Is.True);
        }

        [Test]
        public void AreAdjacentReturnsFalse()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "L1");
            _simpleDirectedGraph.AddEdge("A", "C", "L2");
            Assert.That(_simpleDirectedGraph.AreAdjacent("B", "C"), Is.False);
            Assert.That(_simpleDirectedGraph.AreAdjacent("B", "A"), Is.False);
        }

        [Test]
        public void AreAdjacentThrowsArgumentNullException()
        {
            Assert.That(() => _simpleDirectedGraph.AreAdjacent("A", null), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.AreAdjacent(null, "B"), Throws.ArgumentNullException);
            Assert.That(() => _simpleDirectedGraph.AreAdjacent(null, null), Throws.ArgumentNullException);
        }

        [Test]
        public void AreAdjacentThrowsArgumentException()
        {
            Assert.That(() => _simpleDirectedGraph.AreAdjacent("C", "D"), Throws.ArgumentException);
            Assert.That(() => _simpleDirectedGraph.AreAdjacent("D", "A"), Throws.ArgumentException);
            Assert.That(() => _simpleDirectedGraph.AreAdjacent("E", "D"), Throws.ArgumentException);
        }

        [Test]
        public void DegreeReturnsCorrectInteger()
        {
            _simpleDirectedGraph.AddVertex("D");
            _simpleDirectedGraph.AddEdge("A", "B", "L1");
            _simpleDirectedGraph.AddEdge("A", "C", "L2");
            Assert.That(_simpleDirectedGraph.Degree("A"), Is.EqualTo(2));
            Assert.That(_simpleDirectedGraph.Degree("B"), Is.EqualTo(1));
            Assert.That(_simpleDirectedGraph.Degree("C"), Is.EqualTo(1));
            Assert.That(_simpleDirectedGraph.Degree("D"), Is.EqualTo(0));
        }

        [Test]
        public void DegreeThrowsArgumentNullException()
        {
            Assert.That(() => _simpleDirectedGraph.Degree(null), Throws.ArgumentNullException);
        }

        [Test]
        public void DegreeThrowsArgumentException()
        {
            Assert.That(() => _simpleDirectedGraph.Degree("D"), Throws.ArgumentException);
        }

        [Test]
        public void InDegreeReturnsCorrectResult()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "L1");
            _simpleDirectedGraph.AddEdge("A", "C", "L2");
            Assert.That(_simpleDirectedGraph.InDegree("A"), Is.EqualTo(0));
            Assert.That(_simpleDirectedGraph.InDegree("B"), Is.EqualTo(1));
            Assert.That(_simpleDirectedGraph.InDegree("C"), Is.EqualTo(1));
        }

        [Test]
        public void InDegreeThrowsArgumentException()
        {
            Assert.That(() => _simpleDirectedGraph.InDegree("D"), Throws.ArgumentException);
        }

        [Test]
        public void InDegreeThrowsArgumentNullException()
        {
            Assert.That(() => _simpleDirectedGraph.InDegree(null), Throws.ArgumentNullException);
        }

        [Test]
        public void OutDegreeReturnsCorrectResult()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "L1");
            _simpleDirectedGraph.AddEdge("A", "C", "L2");
            Assert.That(_simpleDirectedGraph.OutDegree("A"), Is.EqualTo(2));
            Assert.That(_simpleDirectedGraph.OutDegree("B"), Is.EqualTo(0));
            Assert.That(_simpleDirectedGraph.OutDegree("C"), Is.EqualTo(0));
        }

        [Test]
        public void OutDegreeThrowsArgumentException()
        {
            Assert.That(() => _simpleDirectedGraph.OutDegree("D"), Throws.ArgumentException);
        }

        [Test]
        public void OutDegreeThrowsArgumentNullException()
        {
            Assert.That(() => _simpleDirectedGraph.OutDegree(null), Throws.ArgumentNullException);
        }

        [Test]
        public void AdjacentVertexReturnsCorrectSet()
        {
            _simpleDirectedGraph.AddEdge("A", "B", "L1");
            _simpleDirectedGraph.AddEdge("A", "C", "L2");
            _simpleDirectedGraph.AddEdge("B", "A", "L3");
            _simpleDirectedGraph.AddEdge("B", "C", "L4");
            Assert.That(_simpleDirectedGraph.AdjacentVertex("A"), Is.EqualTo(new[] {"B", "C"}));
            Assert.That(_simpleDirectedGraph.AdjacentVertex("B"), Is.EqualTo(new[] { "A", "C" }));
            Assert.That(_simpleDirectedGraph.AdjacentVertex("C"), Is.EqualTo(new string[]{}));
        }
    }
}
