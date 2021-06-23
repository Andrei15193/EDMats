using System.Collections.Generic;

namespace EDMats
{
    public static class ListExtensions
    {
        public static void Reset<T>(this IList<T> destination, IEnumerable<T> source)
        {
            var index = 0;
            foreach (var item in source)
            {
                if (index < destination.Count)
                    destination[index] = item;
                else
                    destination.Add(item);
                index++;
            }
            while (destination.Count > index)
                destination.RemoveAt(destination.Count - 1);
        }
    }
}