using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParsingTesting.Utility
{
    public static class ExtensionMethods
    {
        public static IEnumerable<List<T>> SplitList<T>(this List<T> locations, int numOfLists = 4)
        {
            var lists = new List<List<T>>();

            for (int i = 0; i < numOfLists; i++)
            {
                lists.Add(new List<T>());
            }

            for (int i = 0; i < locations.Count; i++)
            {
                lists.ElementAt(i % numOfLists).Add(locations.ElementAt(i));
            }
            
            return lists;
        }
    }
}
