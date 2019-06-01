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

namespace KeyValuePairs.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using KeyValuePairs;
    using NUnit.Framework;
    using static Module;

    public class Tests
    {
        [Test]
        public void WithKey()
        {
            var result = KeyValuePair("foo", 42).WithKey("bar");
            Assert.That(result, Is.EqualTo(KeyValuePair("bar", 42)));
        }

        [Test]
        public void WithValue()
        {
            var result = KeyValuePair("foo", 42).WithValue("bar");
            Assert.That(result, Is.EqualTo(KeyValuePair("foo", "bar")));
        }

        [Test]
        public void Swap()
        {
            var result = KeyValuePair("foo", 42).Swap();
            Assert.That(result, Is.EqualTo(KeyValuePair(42, "foo")));
        }

        public class Fold
        {
            [Test]
            public void NullFolder()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    KeyValuePair("foo", 42).Fold((Func<string, int, object>)null!));

                Assert.That(e.ParamName, Is.EqualTo("folder"));
            }

            [Test]
            public void Test()
            {
                var pair = KeyValuePair("foo", new object());
                var (k, v) = pair.Fold(ValueTuple.Create);
                KeyValuePair("foo", "bar").Fold(string.Concat);
                Assert.That(k, Is.SameAs(pair.Key));
                Assert.That(v, Is.SameAs(pair.Value));
            }
        }

        public class Keys
        {
            [Test]
            public void NullPairs()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    Extensions.Keys<object, object>(null!));

                Assert.That(e.ParamName, Is.EqualTo("pairs"));
            }

            [Test]
            public void Test()
            {
                var pairs = new[]
                {
                    KeyValuePair("foo", 123),
                    KeyValuePair("bar", 456),
                    KeyValuePair("baz", 789),
                };

                var keys = pairs.Keys();

                Assert.That(keys, Is.EqualTo(new[] { "foo", "bar", "baz" }));
            }
        }

        public class Values
        {
            [Test]
            public void NullPairs()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    Extensions.Values<object, object>(null!));

                Assert.That(e.ParamName, Is.EqualTo("pairs"));
            }

            [Test]
            public void Test()
            {
                var pairs = new[]
                {
                    KeyValuePair("foo", 123),
                    KeyValuePair("bar", 456),
                    KeyValuePair("baz", 789),
                };

                var values = pairs.Values();

                Assert.That(values, Is.EqualTo(new[] { 123, 456, 789 }));
            }
        }

        public class PairKey
        {
            [Test]
            public void NullSource()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    Extensions.PairKey<object, object>(null!, delegate{ throw new NotImplementedException(); }));

                Assert.That(e.ParamName, Is.EqualTo("source"));
            }

            [Test]
            public void NullKeySelector()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    new object[0].PairKey((Func<object, object>)null!));

                Assert.That(e.ParamName, Is.EqualTo("keySelector"));
            }

            [Test]
            public void Test()
            {
                var words = new[]
                {
                    "foo",
                    "bar",
                    "baz",
                    "qux",
                    "quux",
                };

                var result = words.PairKey(e => e[0]);

                Assert.That(result, Is.EqualTo(new[]
                {
                    KeyValuePair('f', "foo" ),
                    KeyValuePair('b', "bar" ),
                    KeyValuePair('b', "baz" ),
                    KeyValuePair('q', "qux" ),
                    KeyValuePair('q', "quux"),
                }));
            }
        }

        public class MapKey
        {
            [Test]
            public void NullPairs()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    Extensions.MapKey<object, object, object>(null!, delegate{ throw new NotImplementedException(); }));

                Assert.That(e.ParamName, Is.EqualTo("pairs"));
            }

            [Test]
            public void NullSelector()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    new KeyValuePair<object, object>[0].MapKey((Func<object, object>)null!));

                Assert.That(e.ParamName, Is.EqualTo("selector"));
            }

            [Test]
            public void Test()
            {
                var pairs = new[]
                {
                    KeyValuePair("foo", 123),
                    KeyValuePair("bar", 456),
                    KeyValuePair("baz", 789),
                };

                var result = pairs.MapKey(k => k.ToUpperInvariant());

                Assert.That(result, Is.EqualTo(new[]
                {
                    KeyValuePair("FOO", 123),
                    KeyValuePair("BAR", 456),
                    KeyValuePair("BAZ", 789),
                }));
            }
        }

        public class MapValue
        {
            [Test]
            public void NullPairs()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    Extensions.MapValue<object, object, object>(null!, delegate{ throw new NotImplementedException(); }));

                Assert.That(e.ParamName, Is.EqualTo("pairs"));
            }

            [Test]
            public void NullSelector()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    new KeyValuePair<object, object>[0].MapValue((Func<object, object>)null!));

                Assert.That(e.ParamName, Is.EqualTo("selector"));
            }

            [Test]
            public void Test()
            {
                var pairs = new[]
                {
                    KeyValuePair("foo", 123),
                    KeyValuePair("bar", 456),
                    KeyValuePair("baz", 789),
                };

                var result = pairs.MapValue(v => v / 10m);

                Assert.That(result, Is.EqualTo(new[]
                {
                    KeyValuePair("foo", 12.3m),
                    KeyValuePair("bar", 45.6m),
                    KeyValuePair("baz", 78.9m),
                }));
            }
        }

        public class CollectingInto
        {
            [Test]
            public void NullPairs()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    Extensions.CollectingInto((IEnumerable<KeyValuePair<object, object>>)null!,
                                              new UselessCollection<object, object>()));

                Assert.That(e.ParamName, Is.EqualTo("pairs"));
            }

            [Test]
            public void NullSelector()
            {
                var e = Assert.Throws<ArgumentNullException>(() =>
                    new KeyValuePair<object, object>[0].CollectingInto((Dictionary<object, object>)null!));

                Assert.That(e.ParamName, Is.EqualTo("collection"));
            }

            [Test]
            public void Test()
            {
                var pairs = new[]
                {
                    KeyValuePair("foo", 123),
                    KeyValuePair("bar", 456),
                    KeyValuePair("baz", 789),
                };

                var dictionary = new Dictionary<string, int>();
                var result = pairs.CollectingInto(dictionary);

                Assert.That(result, Is.SameAs(dictionary));

                Assert.That(dictionary.Count, Is.EqualTo(3));

                foreach (var pair in pairs)
                    Assert.That(dictionary[pair.Key], Is.EqualTo(pair.Value));
            }

            sealed class UselessCollection<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>
            {
                public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()         => throw Error();
                IEnumerator IEnumerable.GetEnumerator()                                => GetEnumerator();
                public void Add(KeyValuePair<TKey, TValue> item)                       => throw Error();
                public void Clear()                                                    => throw Error();
                public bool Contains(KeyValuePair<TKey, TValue> item)                  => throw Error();
                public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw Error();
                public bool Remove(KeyValuePair<TKey, TValue> item)                    => throw Error();
                public int Count                                                       => throw Error();
                public bool IsReadOnly                                                 => throw Error();

                static Exception Error() => new NotImplementedException();
            }
        }
    }
}
