using System.Collections;
using System.Collections.Generic;

public class Node : Element
{
    public Node(string name)
        => Name = name;

    public string Name { get; }

    public override bool Equals(object obj)
        => obj is Node node
            ?  Name == node?.Name 
            : false;

    public override int GetHashCode() => Name.GetHashCode();
}

public class Edge : Element
{
    public Edge(string nodeOne, string nodeTwo)
        => (NodeOne, NodeTwo) = (nodeOne, nodeTwo);

    public string NodeOne { get; }
    public string NodeTwo { get; }

    public override bool Equals(object obj)
        => obj is Edge edge
            ? NodeOne == edge.NodeOne && NodeTwo == edge.NodeTwo
            : false;

    public override int GetHashCode()
        => NodeOne.GetHashCode() ^ NodeTwo.GetHashCode();
}

public class Attr
{
    public Attr(string key, string value)
        => (Key, Value) = (key, value);

    public string Key { get; }
    public string Value { get; }

    public override bool Equals(object obj)
        => obj is Attr attr
            ? Key == attr.Key && Value == attr.Value
            : false;

    public override int GetHashCode()
        => Key.GetHashCode() ^ Value.GetHashCode();
}

public class Graph : Element
{
    private List<Node> nodes = new List<Node>();
    private List<Edge> edges = new List<Edge>();

    public IEnumerable<Node> Nodes => nodes;
    public IEnumerable<Edge> Edges => edges;
    public IEnumerable<Attr> Attrs => attrs;

    public void Add(Node node) => nodes.Add(node);
    public void Add(Edge edge) => edges.Add(edge);
}

public abstract class Element : IEnumerable<Attr>
{
    protected List<Attr> attrs = new List<Attr>();

    public void Add(string key, string value) => attrs.Add(new Attr(key, value));

    public IEnumerator<Attr> GetEnumerator() => attrs.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}