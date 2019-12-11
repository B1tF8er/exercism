using System;
using System.Collections.Generic;

public static class Strain
{
    public static IEnumerable<T> Keep<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        => collection.Filter(predicate);

    public static IEnumerable<T> Discard<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        => collection.Filter(item => !predicate(item));

    private static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        foreach (var item in collection)
            if (predicate(item))
                yield return item;
    }
}