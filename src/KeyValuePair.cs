#region Copyright 2019, Atif Aziz
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
#endregion

namespace KeyValuePairs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Module;

    /// <summary>
    /// A class whose members are designed to be statically imported.
    /// </summary>

    public static class Module
    {
        /// <summary>
        /// Creates and initializes a <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </summary>

        public static KeyValuePair<TKey, TValue> KeyValuePair<TKey, TValue>(TKey key, TValue value) =>
            new KeyValuePair<TKey, TValue>(key, value);
    }

    public static class Extensions
    {
        /// <summary>
        /// Replaces the key of a key-value with another key.
        /// </summary>

        public static KeyValuePair<TKey2, TValue>
            WithKey<TKey1, TValue, TKey2>(this KeyValuePair<TKey1, TValue> pair, TKey2 key) =>
            KeyValuePair(key, pair.Value);

        /// <summary>
        /// Replaces the value of a key-value with another value.
        /// </summary>

        public static KeyValuePair<TKey, TValue2>
            WithValue<TKey, TValue1, TValue2>(this KeyValuePair<TKey, TValue1> pair, TValue2 value) =>
            KeyValuePair(pair.Key, value);

        /// <summary>
        /// Swaps the key and value of a key-value pair such that the value
        /// becomes the key and the key becomes the value.
        /// </summary>

        public static KeyValuePair<TValue, TKey>
            Swap<TKey, TValue>(this KeyValuePair<TKey, TValue> pair) =>
            KeyValuePair(pair.Value, pair.Key);

        /// <summary>
        /// Apply a function to fold the key and value of a key-value pair
        /// into a result.
        /// </summary>

        public static TResult
            Fold<TKey, TValue, TResult>(
                this KeyValuePair<TKey, TValue> pair,
                Func<TKey, TValue, TResult> folder)
            => folder == null ? throw new ArgumentNullException(nameof(folder))
             : folder(pair.Key, pair.Value);

        /// <summary>
        /// Extracts the keys in a sequence of key-value pairs.
        /// </summary>

        public static IEnumerable<TKey>
            Keys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
            pairs switch
            {
                null => throw new ArgumentNullException(nameof(pairs)),
                IDictionary<TKey, TValue> dict => dict.Keys,
                var enumerable => from pair in enumerable select pair.Key
            };

        /// <summary>
        /// Extracts the values in a sequence of key-value pairs.
        /// </summary>

        public static IEnumerable<TValue>
            Values<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
            pairs switch
            {
                null => throw new ArgumentNullException(nameof(pairs)),
                IDictionary<TKey, TValue> dict => dict.Values,
                var enumerable => from pair in enumerable select pair.Value
            };

        /// <summary>
        /// Pairs each element in a sequence with its key.
        /// </summary>

        public static IEnumerable<KeyValuePair<TKey, TSource>>
            PairKey<TSource, TKey>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return from e in source
                   select KeyValuePair(keySelector(e), e);
        }

        /// <summary>
        /// Applies a function to each key of a sequence of key-value pairs.
        /// </summary>

        public static IEnumerable<KeyValuePair<TKey2, TValue>>
            MapKey<TKey1, TValue, TKey2>(
                this IEnumerable<KeyValuePair<TKey1, TValue>> pairs,
                Func<TKey1, TKey2> selector)
        {
            if (pairs == null) throw new ArgumentNullException(nameof(pairs));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return from pair in pairs
                   select pair.WithKey(selector(pair.Key));
        }

        /// <summary>
        /// Applies a function to each value of a sequence of key-value pairs.
        /// </summary>

        public static IEnumerable<KeyValuePair<TKey, TValue2>>
            MapValue<TKey, TValue1, TValue2>(
                this IEnumerable<KeyValuePair<TKey, TValue1>> pairs,
                Func<TValue1, TValue2> selector)
        {
            if (pairs == null) throw new ArgumentNullException(nameof(pairs));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return from pair in pairs
                   select pair.WithValue(selector(pair.Value));
        }

        /// <summary>
        /// Adds a sequence of key-value pairs to a given collection and
        /// returns that collection.
        /// </summary>

        public static TCollection
            CollectingInto<TKey, TValue, TCollection>(
                this IEnumerable<KeyValuePair<TKey, TValue>> pairs,
                TCollection collection)
            where TCollection : ICollection<KeyValuePair<TKey, TValue>>
        {
            if (pairs == null) throw new ArgumentNullException(nameof(pairs));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var pair in pairs)
                collection.Add(pair);

            return collection;
        }
    }
}
