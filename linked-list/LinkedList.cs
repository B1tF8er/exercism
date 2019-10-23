using System;
using System.Collections.Generic;
using System.Linq;

public class Deque<T>
{
    public Node First { get; set; }
    public Node Last { get; set; }     

    public void Push(T value)
    {
        if (First == null)
        {
            First = new Node(value);
            Last = First;
        }
        else
        {
            Last.Next = new Node(value, Last, null);
            Last = Last.Next;
        }
    }

    public T Pop()
    {
        T popped = Last.Value;
        Last = Last.Previous;
        return popped;
    }

    public void Unshift(T value)
    {
        if (First == null)
        {
            First = new Node(value);
            Last = First;
        }
        else
        {
            First.Previous = new Node(value, null, First);
            First = First.Previous;
        }
    }

    public T Shift()
    {
        T shifted = First.Value;
        First = First.Next;
        return shifted;
    }

    public class Node
    {
        public T Value { get; set; }
        public Node Previous { get; set; }
        public Node Next { get; set; }
       
        public Node (T value)
        {
            Value = value;
            Previous = null;
            Next = null;
        }

        public Node (T value, Node previous, Node next)
        {
            Value = value;
            Previous = previous;
            Next = next;
        }
    }  
}