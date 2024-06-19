using System;
using System.Collections.Generic;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    public static class SerializableDictionaryExtensions
    {
        public static SerializableDictionary<TKey, TValue> ToSerializableDictionary<TKey, TValue, TElement>(
            this IEnumerable<TElement> elements,
            Func<TElement, TKey> getKey,
            Func<TElement, TValue> getValue)
        {
            var dicitonary = new SerializableDictionary<TKey, TValue>();
            foreach (var element in elements)
            {
                var key = getKey.Invoke(element);
                var value = getValue.Invoke(element);
                dicitonary.Add(key, value);
            }

            return dicitonary;
        }
    }
}
