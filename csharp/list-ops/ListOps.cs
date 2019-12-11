using System;
using System.Collections.Generic;
using System.Linq;

public static class ListOps
{
    public static int Length<T>(List<T> input)
    {
        var counter = 0;

        foreach (var item in input)
            counter++;

        return counter;
    }

    public static List<T> Reverse<T>(List<T> input)
    {
        var reversedItems = new List<T>();
        var totalItems = Length(input) - 1;

        for (var index = totalItems; index >= 0; index--)
            reversedItems.Add(input[index]);

        return reversedItems;
    }

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map)
    {
        var mappedItems = new List<TOut>();

        foreach (var item in input)
            mappedItems.Add(map(item));

        return mappedItems;
    }

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate)
    {
        var filteredItems = new List<T>();

        foreach (var item in input)
        {
            if (predicate(item))
                filteredItems.Add(item);
        }

        return filteredItems;
    }

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func) =>
        Fold(input, start, func);

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func) =>
        Fold(Reverse(input), start, (acc, x) => func(x, acc));

    public static List<T> Concat<T>(List<List<T>> input)
    {
        var items = new List<T>();

        foreach (var innerList in input)
        {
            foreach (var item in innerList)
                items.Add(item);
        }

        return items;
    }

    public static List<T> Append<T>(List<T> left, List<T> right)
    {
        foreach (var item in right)
            left.Add(item);

        return left;
    }

    private static TOut Fold<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        TOut accumulator = start;

        foreach (var item in input)
            accumulator = func(accumulator, item);

        return accumulator;
    }
}