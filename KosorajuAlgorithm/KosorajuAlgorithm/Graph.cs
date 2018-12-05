using System;
using System.Collections.Generic;
using System.Linq;

namespace KosorajuAlgorithm
{
    public class Graph
    {
        public int V { get; set; }
        public LinkedList<int>[] AdjacencyLists { get; set; }

        public Graph(int V)
        {
            this.V = V;
            AdjacencyLists = new LinkedList<int>[V];
            for (var i = 0; i < V; i++)
            {
                AdjacencyLists[i] = new LinkedList<int>();
            }
        }

        public void AddEdge(int v, int w) => AdjacencyLists[v].AddLast(w);

        /// <summary>
        /// Find and print strongly connected components
        /// </summary>
        public void PrintScss()
        {
            //First DFS
            var stack = new Stack<int>();
            var visited = new bool[V];
            // Fill vertices in stack according to finishing times
            for (var i = 0; i < V; i++)
            {
                if (!visited[i])
                    Fill(i, visited, stack);
            }

            //Second DFS
            var transposedGraph = TransposeGraph();
            visited = Enumerable.Repeat(false, V).ToArray();
            while (stack.Count > 0)
            {
                var v = stack.Pop();
                if (!visited[v])
                {
                    transposedGraph.Dfs(v, visited);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Function that returns transposed graph
        /// </summary>
        /// <returns>New transposed graph</returns>
        private Graph TransposeGraph()
        {
            var transposed = new Graph(V);
            for (var v = 0; v < V; v++)
            {
                var enumerator = AdjacencyLists[v].GetEnumerator();
                while (enumerator.MoveNext())
                {
                    transposed.AdjacencyLists[enumerator.Current].AddLast(v);
                }
            }
            return transposed;
        }

        /// <summary>
        ///  Fill stack with v. The top element of stack has the maximum finishing time.
        /// </summary>
        /// <param name="v">vertice</param>
        /// <param name="visited">Array of visited v</param>
        /// <param name="stack">Stack with maximum finishing time of each v</param>
        private void Fill(int v, IList<bool> visited, Stack<int> stack)
        {
            visited[v] = true;
            var enumerator = AdjacencyLists[v].GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!visited[enumerator.Current])
                    Fill(enumerator.Current, visited, stack);
            }
            stack.Push(v);
        }

        /// <summary>
        /// DFS 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="visited"></param>
        private void Dfs(int v, IList<bool> visited)
        {
            visited[v] = true;
            Console.Write($"{v} ");

            var enumerator = AdjacencyLists[v].GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!visited[enumerator.Current])
                    Dfs(enumerator.Current, visited);
            };
        }
    }
}
