using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ASP
    {
        private List<Tuple<int, int>> pairs;

        public ASP(int size)
        {
            Random rand = new Random();
            pairs = new List<Tuple<int, int>>(size);
            for (int i = 0; i < size; i++)
            {
                var y = rand.Next(0, 25);
                pairs.Add(new Tuple<int, int>(y, rand.Next(y, y + 25)));
            }
        }
        public List<Tuple<int, int>> Calculate()
        {
            pairs.Sort((x, y) => x.Item2.CompareTo(y.Item2)); // sortujemy
            int m = 0;
            var returnedPairs = new List<Tuple<int, int>>(); // to co chcemy potem zwrocic
            for (int k = 1; k < pairs.Count; k++)
            {
                if (pairs[k].Item1 >= pairs[m].Item1)
                {
                    m = k;
                    returnedPairs.Add(pairs[k]);
                }
            }
            return returnedPairs;
        }

        public List<List<int>> Calculate2() 
        {
            pairs.Sort((x, y) => x.Item1.CompareTo(y.Item1)); // sortujemy
            int m = 0;
            var _pairs = new List<Tuple<int, int>>(); // to co chcemy potem zwrocic
            var returnedList = new List<List<int>>();
            for(int i=0; i<pairs.Count;i++)
            {
                returnedList.Add(new List<int>(0));
            }
            /* COS Z CPP XD
              var tmp = [](auto &a, auto &b){return (a.item2 > b.item2);}
              using p = pair<int,int>
             priority_queue<p, List<p, decltype(tmp)> PQ;
             PQ.insert(pair(0,pairs[0].item2);
             for(int k=1; k<n; k++)
             {
             auto z = PQ.pop();
             if(z.item2 < V[k].item1) {
             returnedList[z.item1].push_back(k);
             PQ.insert(pair(z.item1,V[k].item2));
             else
             PQ.insert(z);
             PQ.insert(...)
            
             */
            for (int k = 1; k < pairs.Count; k++)
            {
                if (pairs[k].Item1 >= pairs[m].Item2)
                {
                    m = k;
                    _pairs.Add(pairs[k]);
                    returnedList.Add(_pairs);
                    pairs.RemoveAt(k);
                }
            }
            return returnedList;
        }
    }
}
