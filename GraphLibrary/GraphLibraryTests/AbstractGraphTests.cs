using System.Linq;
using GraphLibrary;
using NUnit.Framework;

namespace GraphLibraryTests
{
    [TestFixture]
    public class AbstractGraphTests
    {
        private IGraph<string, int> _abstractGraph;

        [SetUp]
        public void SetUp()
        {
            _abstractGraph = new SimpleDirectedGraph<string, int>();
        }

        [Test]
        public void AddVertexReturnsOk()
        {
            Assert.That(_abstractGraph.AddVertex("A"), Is.True);
            Assert.That(_abstractGraph.AddVertex("A"), Is.False);
            Assert.That(_abstractGraph.GetVertexSet(), Contains.Item("A"));
            Assert.That(_abstractGraph.GetVertexSet().Count(), Is.EqualTo(1));
        }

        [Test]
        public void AddVertexThrowsArgumentNullException()
        {
            Assert.That(() => _abstractGraph.AddVertex((string) null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddVertexNullValuesArrayRemainsEmpty()
        {
            _abstractGraph.AddVertex(new string[] {null});
            Assert.That(_abstractGraph.GetVertexSet(), Is.Empty);
        }

        [Test]
        public void AddVertexNullStringArrayThrowsArgumentNullException()
        {
            Assert.That(() => _abstractGraph.AddVertex((string[]) null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddVertexFilledArrayReturnsOk()
        {
            _abstractGraph.AddVertex(new [] { "A", "B", null, "C", "A" });
            Assert.That(_abstractGraph.GetVertexSet(), Is.EqualTo(new[] {"A", "B", "C"}));
        }

        [Test]
        public void DeleteVertexReturnsOk()
        {
            _abstractGraph.AddVertex(new[] {"A", "B", "C"});
            Assert.That(_abstractGraph.DeleteVertex("A"), Is.True);
            Assert.That(_abstractGraph.DeleteVertex("A"), Is.False);
            Assert.That(_abstractGraph.GetVertexSet(), Is.EqualTo(new[] {"B", "C"}));
        }

        [Test]
        public void DeleteVertexThrowsArgumentNullException()
        {
            Assert.That(() => _abstractGraph.DeleteVertex((string) null), Throws.ArgumentNullException);
        }

        [Test]
        public void DeleteVertexStringArrayReturnsOk()
        {
            _abstractGraph.AddVertex(new[] { "A", "B", "C" });
            _abstractGraph.DeleteVertex(new [] {"B", null, "C"});
            Assert.That(_abstractGraph.GetVertexSet(), Is.EqualTo(new[] {"A"}));
        }

        [Test]
        public void DeleteVertexNullArrayThrowsArgumentNullException()
        {
            Assert.That(() => _abstractGraph.DeleteVertex((string[])null), Throws.ArgumentNullException);
        }

        [Test]
        public void VertexNumberReturnsOk()
        {
            Assert.That(_abstractGraph.VertexNumber(), Is.EqualTo(0));
            _abstractGraph.AddVertex(new[] { "A", "B", "C" });
            Assert.That(_abstractGraph.VertexNumber(), Is.EqualTo(3));
            _abstractGraph.DeleteVertex("B");
            Assert.That(_abstractGraph.VertexNumber(), Is.EqualTo(2));
        }
    }
}