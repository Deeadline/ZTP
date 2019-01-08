using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new ASP(20).Calculate2();
            foreach (var x in a)
            {
                Console.WriteLine("New line");
                foreach (var y in x)
                {
                    Console.WriteLine($"x1: {y.Item1}, x2: {y.Item2}");
                }
            }
            Console.ReadLine();
        }
    }
}
