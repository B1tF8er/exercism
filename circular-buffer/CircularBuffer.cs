using System;
using System.Collections.Generic;

public class CircularBuffer<T>
{
    private readonly int maxCapacity;
    private readonly Queue<T> queue;

    public CircularBuffer(int capacity)
    {
        maxCapacity = capacity;
        queue = new Queue<T>(capacity);
    }

    public T Read() => queue.Count == 0
        ? throw new InvalidOperationException("No elements")
        : queue.Dequeue();

    public void Write(T value)
    {
        if (queue.Count == maxCapacity)
            throw new InvalidOperationException("Full buffer");
        
        queue.Enqueue(value);
    }

    public void Overwrite(T value)
    {
        if (queue.Count < maxCapacity)
            queue.Enqueue(value);
        else
        {
            queue.Dequeue();
            queue.Enqueue(value);
        }
    }

    public void Clear() => queue.Clear();
}