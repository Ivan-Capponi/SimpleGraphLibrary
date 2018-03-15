using System;
using System.Collections.Generic;

namespace GraphLibrary
{
    public abstract class AbstractGraph <TV,TK> : IGraph<TV,TK>
    {
        protected readonly List<TV> VertexSet = new List<TV>();
        protected readonly List<PairValue<TV>> EdgeSet = new List<PairValue<TV>>();
        protected readonly Dictionary<IPairValue<TV>, TK> Weigths = new Dictionary<IPairValue<TV>, TK>();
        public bool AddVertex(TV vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
            if (VertexSet.Contains(vertex))
                return false;
            VertexSet.Add(vertex);
            return true;
        }
        public void AddVertex(IEnumerable<TV> set)
        {
            if (set == null)
                throw new ArgumentNullException();
            using (var it = set.GetEnumerator())
                while (it.MoveNext())
                    if (it.Current != null && !VertexSet.Contains(it.Current))
                        VertexSet.Add(it.Current);
        }
        public void DeleteVertex(IEnumerable<TV> set)
        {
            if (set == null)
                throw new ArgumentNullException();
            using (var it = set.GetEnumerator())
                while (it.MoveNext())
                    if (it.Current != null)
                        VertexSet.Remove(it.Current);
        }
        public bool DeleteVertex(TV vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(vertex))
                return false;
            VertexSet.Remove(vertex);
            return true;
        }
        public IEnumerable<TV> GetVertexSet()
        {
            foreach (TV vertex in VertexSet)
                yield return vertex;
        }

        public IEnumerable<IPairValue<TV>> GetEdgeSet()
        {
            foreach (PairValue<TV> edge in EdgeSet)
                yield return edge;
        }

        public int VertexNumber()
        {
            return VertexSet.Count;
        }
        public int EdgeNumber()
        {
            return EdgeSet.Count;
        }
        public abstract bool AddEdge(TV v1, TV v2, TK weigth);
        public abstract TK GetWeigth(TV v1, TV v2);

        public abstract bool DeleteEdge(TV v1, TV v2);
        public abstract bool AreAdjacent(TV v1, TV v2);
        public abstract int Degree(TV vertex);
        public abstract int OutDegree(TV vertex);
        public abstract int InDegree(TV vertex);
        public abstract IEnumerable<TV> AdjacentVertex(TV vertex);
    }
}
