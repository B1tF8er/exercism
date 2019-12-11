using System;
using System.Collections.Generic;
using System.Linq;

public class CustomSet
{
    private readonly ISet<int> set = new HashSet<int>();

    public CustomSet(params int[] values) =>
        values
            .OrderBy(value => value)
            .ToList()
            .ForEach(value => set.Add(value));

    public CustomSet Add(int value)
    {
        set.Add(value);
        return new CustomSet(set.ToArray());
    }

    public bool Empty()
        => !set.Any();

    public bool Contains(int value)
        => set.Contains(value);

    public bool Subset(CustomSet right)
        => set.IsSubsetOf(right.set);

    public bool Disjoint(CustomSet right)
        => !set.Any(value => right.set.Contains(value));

    public CustomSet Intersection(CustomSet right)
        => new CustomSet(set.Intersect(right.set).ToArray());

    public CustomSet Difference(CustomSet right)
        => new CustomSet(set.Except(right.set).ToArray());

    public CustomSet Union(CustomSet right)
        => new CustomSet(set.Union(right.set).ToArray());

    public override bool Equals(object obj)
        => obj is CustomSet customSet
            ? set.SequenceEqual(customSet.set)
            : false;

    public override int GetHashCode() => set.GetHashCode();
}