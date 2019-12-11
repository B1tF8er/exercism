using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree : IEnumerable<int>
{
    public BinarySearchTree(int value) =>
        Value = value;

    public BinarySearchTree(IEnumerable<int> values)
    {
        if (!values.Any())
            throw new ArgumentException("Cannot create tree from empty enumerable");

        Value = values.First();

        foreach (var value in values.Skip(1))
            Add(value);
    }

    public int Value { get; }

    public BinarySearchTree Left { get; private set; }

    public BinarySearchTree Right { get; private set; }

    public BinarySearchTree Add(int value)
    {
        if (value <= Value)
            Left = Add(value, Left);
        else
            Right = Add(value, Right);

        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        foreach (var left in GetNodes(Left))
            yield return left;

        yield return Value;

        foreach (var right in GetNodes(Right))
            yield return right;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static BinarySearchTree Add(int value, BinarySearchTree tree)
    {
        if (tree == null)
            return new BinarySearchTree(value);

        return tree.Add(value);
    }

    private IEnumerable<int> GetNodes(BinarySearchTree tree) =>
        tree?.AsEnumerable() ?? Enumerable.Empty<int>();
}