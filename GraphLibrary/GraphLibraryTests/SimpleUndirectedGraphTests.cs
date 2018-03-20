using GraphLibrary;
using NUnit.Framework;

namespace GraphLibraryTests
{
    [TestFixture()]
    public class SimpleUndirectedGraphTests
    {
        private SimpleUndirectedGraph<string, string> _graph;

        [SetUp]
        public void SetUp()
        {
            _graph = new SimpleUndirectedGraph<string, string>();
            _graph.AddVertex(new[] {"A", "B", "C", "D"});
        }

        [Test]
        public void AddEdgeThrowsArumentNullException()
        {
            Assert.That(() => _graph.AddEdge(null, "B", "L1"), Throws.ArgumentNullException);
            Assert.That(() => _graph.AddEdge("A", null, "L1"), Throws.ArgumentNullException);
            Assert.That(() => _graph.AddEdge("A", "B", null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddEdgeReturnsFalse()
        {
            Assert.That(_graph.AddEdge("A", "E", "L1"), Is.False);
            Assert.That(_graph.AddEdge("E", "A", "L1"), Is.False);
        }

        [Test]
        public void AddEdgeReturnsTrue()
        {
            Assert.That(_graph.AddEdge("A", "B", "L1"), Is.True);
            Assert.That(_graph.AddEdge("A", "B", "L2"), Is.False);
            Assert.That(_graph.AddEdge("B", "A", "L3"), Is.False);
            Assert.That(_graph.GetEdgeSet(), Contains.Item(new PairValue<string>("A", "B")));
            Assert.That(_graph.GetEdgeSet(), Contains.Item(new PairValue<string>("B", "A")));
        }

        [Test]
        public void GetWeigthThrowsArgumentNullException()
        {
            Assert.That(() => _graph.GetWeigth(null, "B"), Throws.ArgumentNullException);
            Assert.That(() => _graph.GetWeigth("A", null), Throws.ArgumentNullException);
        }

        [Test]
        public void GetWeigthThrowsArgumentException()
        {
            Assert.That(() => _graph.GetWeigth("A", "E"), Throws.ArgumentException);
            Assert.That(() => _graph.GetWeigth("Z", "B"), Throws.ArgumentException);
            Assert.That(() => _graph.GetWeigth("A", "D"), Throws.ArgumentException);
        }

        [Test]
        public void GetWeigthReturnsOk()
        {
            _graph.AddEdge("A", "B", "W1");
            Assert.That(_graph.GetWeigth("A", "B"), Is.EqualTo("W1"));
            Assert.That(_graph.GetWeigth("B", "A"), Is.EqualTo("W1"));
        }

        [Test]
        public void DeleteEdgeThrowsArgumentNullException()
        {
            Assert.That(() => _graph.DeleteEdge(null, "B"), Throws.ArgumentNullException);
            Assert.That(() => _graph.DeleteEdge("A", null), Throws.ArgumentNullException);
            Assert.That(() => _graph.DeleteEdge(null, null), Throws.ArgumentNullException);
        }

        [Test]
        public void DeleteEdgeReturnsFalse()
        {
            Assert.That(_graph.DeleteEdge("A", "B"), Is.False);
            Assert.That(_graph.DeleteEdge("A", "E"), Is.False);
        }

        [Test]
        public void DeleteEdgeReturnsTrue()
        {
            _graph.AddEdge("A", "B", "W1");
            Assert.That(_graph.DeleteEdge("A", "B"), Is.True);
            Assert.That(_graph.GetEdgeSet(), !Contains.Item(new PairValue<string>("A", "B")));
            Assert.That(_graph.GetEdgeSet(), !Contains.Item(new PairValue<string>("B", "A")));
        }

        [Test]
        public void AreAdjacentThrowsArgumentNullException()
        {
            Assert.That(() => _graph.AreAdjacent(null, "B"), Throws.ArgumentNullException);
            Assert.That(() => _graph.AreAdjacent("A", null), Throws.ArgumentNullException);
            Assert.That(() => _graph.AreAdjacent(null, null), Throws.ArgumentNullException);
        }

        [Test]
        public void AreAdjacentThrowsArgumentException()
        {
            Assert.That(() => _graph.AreAdjacent("A", "E"), Throws.ArgumentException);
            Assert.That(() => _graph.AreAdjacent("Z", "B"), Throws.ArgumentException);
        }

        [Test]
        public void AreAdjacentReturnsFalse()
        {
            Assert.That(_graph.AreAdjacent("A", "B"), Is.False);
        }

        [Test]
        public void AreAdjacentReturnsTrue()
        {
            _graph.AddEdge("A", "B", "W1");
            Assert.That(_graph.AreAdjacent("A", "B"), Is.True);
            Assert.That(_graph.AreAdjacent("B", "A"), Is.True);
        }

        [Test]
        public void DegreeThrowsArgumentNullException()
        {
            Assert.That(() => _graph.Degree(null), Throws.ArgumentNullException);
        }

        [Test]
        public void DegreeThrowsArgumentException()
        {
            Assert.That(() => _graph.Degree("E"), Throws.ArgumentException);
        }

        [Test]
        public void DegreeReturnsCorrectResult()
        {
            Assert.That(_graph.Degree("A"), Is.EqualTo(0));
            Assert.That(_graph.Degree("B"), Is.EqualTo(0));
            Assert.That(_graph.Degree("C"), Is.EqualTo(0));
            Assert.That(_graph.Degree("D"), Is.EqualTo(0));
            _graph.AddEdge("A", "B", "W1");
            _graph.AddEdge("A", "C", "W2");
            Assert.That(_graph.Degree("A"), Is.EqualTo(2));
            Assert.That(_graph.Degree("B"), Is.EqualTo(1));
            Assert.That(_graph.Degree("C"), Is.EqualTo(1));
        }

        [Test]
        public void OutDegreeThrowsArgumentNullException()
        {
            Assert.That(() => _graph.OutDegree(null), Throws.ArgumentNullException);
        }

        [Test]
        public void InDegreeThrowsArgumentNullException()
        {
            Assert.That(() => _graph.InDegree(null), Throws.ArgumentNullException);
        }

        [Test]
        public void OutDegreeThrowsArgumentException()
        {
            Assert.That(() => _graph.OutDegree("E"), Throws.ArgumentException);
        }

        [Test]
        public void InDegreeThrowsArgumentException()
        {
            Assert.That(() => _graph.InDegree("E"), Throws.ArgumentException);
        }

        [Test]
        public void OutDegreeReturnsCorrectResult()
        {
            Assert.That(_graph.OutDegree("A"), Is.EqualTo(0));
            Assert.That(_graph.OutDegree("B"), Is.EqualTo(0));
            Assert.That(_graph.OutDegree("C"), Is.EqualTo(0));
            Assert.That(_graph.OutDegree("D"), Is.EqualTo(0));
            _graph.AddEdge("A", "B", "W1");
            _graph.AddEdge("A", "C", "W2");
            Assert.That(_graph.OutDegree("A"), Is.EqualTo(2));
            Assert.That(_graph.OutDegree("B"), Is.EqualTo(1));
            Assert.That(_graph.OutDegree("C"), Is.EqualTo(1));
        }

        [Test]
        public void InDegreeReturnsCorrectResult()
        {
            Assert.That(_graph.InDegree("A"), Is.EqualTo(0));
            Assert.That(_graph.InDegree("B"), Is.EqualTo(0));
            Assert.That(_graph.InDegree("C"), Is.EqualTo(0));
            Assert.That(_graph.InDegree("D"), Is.EqualTo(0));
            _graph.AddEdge("A", "B", "W1");
            _graph.AddEdge("A", "C", "W2");
            Assert.That(_graph.InDegree("A"), Is.EqualTo(2));
            Assert.That(_graph.InDegree("B"), Is.EqualTo(1));
            Assert.That(_graph.InDegree("C"), Is.EqualTo(1));
        }

        [Test]
        public void AdjacentVertexReturnsCorrectSet()
        {
            _graph.AddEdge("A", "B", "W1");
            _graph.AddEdge("A", "C", "W2");
            Assert.That(_graph.AdjacentVertex("A"), Is.EqualTo(new[] {"B", "C"}));
            Assert.That(_graph.AdjacentVertex("B"), Is.EqualTo(new[] {"A"}));
            Assert.That(_graph.AdjacentVertex("C"), Is.EqualTo(new[] { "A" }));
        }
    }
}
