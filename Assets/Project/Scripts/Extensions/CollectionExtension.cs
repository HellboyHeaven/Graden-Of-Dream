using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class CollectionExtension
    {
        private static Random random = new Random();
        
        

        public static T  GetRandom<T> (this IReadOnlyCollection<T> e) => e.Count > 0 ? e.ElementAt(random.Next(e.Count)) : default;

        //public static T  GetRandom<T> (this IEnumerable<T> e, int count) => e.ElementAt(random.Next(count));
        
        
        public static int IndexOf<T>(this IReadOnlyList<T> self, T element)
        {

            for (int i = 0; i < self.Count; i++)
            {
                if (self[i].Equals(element))
                    return i;
            }

            return -1;
        }
    }
}