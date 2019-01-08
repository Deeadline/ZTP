using System;
using System.IO;
using System.Text;

namespace KosorajuAlgorithm
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var graph = ReadFromFile();
            graph.PrintScss();
            var message = graph.HasCycle()
                ? $"Cannot set children in one row and minimum rows are equal to {graph.GetMinimalSizeOfRows()}"
                : "We can set children in one row";
            Console.WriteLine(message);
            Console.ReadKey();
        }
        private static Graph ReadFromFile()
        {
            Graph graph = null;
            try
            {
                var path = Path.GetFullPath("..//..//..//Graph.txt");
                using (var fileStream = File.OpenRead(path))
                {
                    using (var reader = new StreamReader(fileStream, Encoding.UTF8, true, 128))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Length < 2)
                            {
                                graph = new Graph(int.Parse(line));
                            }
                            else
                            {
                                graph.AddEdge(int.Parse(line[0].ToString()), int.Parse(line[2].ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return graph;
        }
    }
}
