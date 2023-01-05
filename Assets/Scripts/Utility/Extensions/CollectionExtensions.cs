using System.Collections.Generic;
using System.Linq;

namespace ColonizationMobileGame.Utility.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}