using System.Collections.Generic;

namespace GraphLibrary
{
    public interface IGraph <TV, TK>
    {
        bool AddVertex(TV vertex);
        void AddVertex(IEnumerable<TV> set);
        bool DeleteVertex(TV vertex);
        void DeleteVertex(IEnumerable<TV> set);
        bool AddEdge(TV v1, TV v2, TK weigth);
        TK GetWeigth(TV v1, TV v2);
        bool DeleteEdge(TV v1, TV v2);
        bool AreAdjacent(TV v1, TV v2);
        int Degree(TV vertex);
        int OutDegree(TV vertex);
        int InDegree(TV vertex);
        int VertexNumber();
        int EdgeNumber();
        IEnumerable<TV> AdjacentVertex(TV vertex);
        IEnumerable<TV> GetVertexSet();
        IEnumerable<IPairValue<TV>> GetEdgeSet();
    }

    public interface IPairValue<T>
    {
        T GetFirst();
        T GetSecond();
        bool Contains(T value);
    }
}
