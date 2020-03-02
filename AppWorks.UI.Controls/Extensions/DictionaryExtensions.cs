using System.Collections.Generic;

namespace AppWorks.UI.Controls.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool DictionaryEquals<TKey, TValue>(this Dictionary<TKey, TValue> left, Dictionary<TKey, TValue> right)
        {
            var comparer = EqualityComparer<TValue>.Default;
            if (left.Count != right.Count)
            {
                return false;
            }
            foreach (var item in left)
            {
                TValue value;
                if (!right.TryGetValue(item.Key, out value) || !comparer.Equals(item.Value, value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
