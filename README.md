# KeyValuePairs

KeyValuePairs is a .NET Standard library with helper and extension methods that
exclusively deal with [`KeyValuePair<,>`][kvp].

It is [available as a package for installation from nuget.org][nupkg].


## Usgae

Statically import `KeyValuePairs.Module`:

```c#
using static KeyValuePairs.Module;
```

Then create `KeyValuePair<,>` simply and without any type arguments by
calling `KeyValuePair`:

```c#
var pair = KeyValuePair("foo", 42);
```

Following extension methods are available when you import the
`KeyValuePairs` namespace:

```c#
// change the key of a pair
Console.WriteLine(pair.WithKey("bar"));         // [bar, 42]

// change the value of a pair
Console.WriteLine(pair.WithValue("baz"));       // [foo, baz]

// swap the key and the value of a pair
Console.WriteLine(pair.Swap());                 // [42, foo]

// fold the key and value of a pair into a result
Console.WriteLine(pair.WithValue("bar")
                      .Fold(string.Concat));    // foobar

// pair each element with its key
var words = new[] { "foo", "bar", "baz", "qux", "quux" };
Console.WriteLine(string.Join(",", words.PairKey(w => w[0])));
// [f, foo],[b, bar],[b, baz],[q, qux],[q, quux]

var pairs = new[]
{
    KeyValuePair("foo", 123),
    KeyValuePair("bar", 456),
    KeyValuePair("baz", 789),
};

// extract keys
Console.WriteLine(string.Join(",", pairs.Keys()));   // foo,bar,baz

// extract values
Console.WriteLine(string.Join(",", pairs.Values())); // 123,456,789

// map keys
Console.WriteLine(string.Join(",", pairs.MapKey(k => k.ToUpperInvariant())));
// [FOO, 123],[BAR, 456],[BAZ, 789]

// map values
Console.WriteLine(string.Join(",", pairs.MapValue(v => v / 10m)));
// [FOO, 12.3],[BAR, 45.6],[BAZ, 78.9]

// collect pairs into any collection
var list = pairs.CollectingInto(new List<KeyValuePair<string, int>>());
var dict = pairs.CollectingInto(new Dictionary<string, int>>());
```

[nupkg]: https://www.nuget.org/packages/KeyValuePairs/
[kvp]: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2
[KeyValuePair.Create]: [https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair.create
