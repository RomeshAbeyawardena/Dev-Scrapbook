using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleApp.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, IEnumerable<T> itemsToAppend)
        {
            var newItemArray = items.ToList();
            newItemArray.AddRange(itemsToAppend);
            return newItemArray;
        }

    }
}
