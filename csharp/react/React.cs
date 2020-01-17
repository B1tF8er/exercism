using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Reactor
{
    private readonly Dictionary<int, Cell> cells = new Dictionary<int, Cell>();

    public InputCell CreateInputCell(int value)
    {
        var inputCell = new InputCell(cells.Count, value);
        inputCell.Changed += CellChanged;

        cells[inputCell.Id] = inputCell;        

        return inputCell;
    }

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
    {
        var computeCell = new ComputeCell(cells.Count, producers, compute);
        cells[computeCell.Id] = computeCell;

        return computeCell;
    }

    private void CellChanged(object sender, int value)
    {
        var cell = (Cell)sender;
        var consumers = new BitArray(cells.Count);
        
        foreach (var consumer in cell.Consumers)
            consumers.Set(consumer.Id, true);

        for (var id = cell.Id + 1; id < cells.Count; id++)
        {
            if (!consumers.Get(id))
                continue;

            var consumer = (ComputeCell)cells[id];
            consumer.Recompute();

            foreach (var transitiveConsumer in consumer.Consumers)
                consumers.Set(transitiveConsumer.Id, true);
        }
    }
}

public abstract class Cell
{
    public int Id { get; }
    public List<Cell> Consumers { get; }
    public abstract int Value { get; set; }
    public abstract event EventHandler<int> Changed;

    public Cell(int id) =>
        (Id, Consumers) = (id, new List<Cell>());
}

public class InputCell : Cell
{   
    public override event EventHandler<int> Changed;

    private int _value;
    public override int Value
    {
        get => _value;
        set
        {
            if (_value == value)
                return;

            _value = value;
            Changed?.Invoke(this, _value);
        }
    }   

    public InputCell(int id, int value) : base(id)
        => _value = value;
}

public class ComputeCell : Cell
{
    private readonly IEnumerable<Cell> producers;
    private readonly Func<int[], int> compute;
    
    public override int Value { get; set; }
    public override event EventHandler<int> Changed;
    
    public ComputeCell(int id, IEnumerable<Cell> producers, Func<int[], int> compute) : base(id)
    {
        this.producers = producers;
        this.compute = compute;

        foreach (var producer in producers)
            producer.Consumers.Add(this);

        Recompute();
    }

    public void Recompute()
    {
        var updatedValue = compute(producers.Select(producer => producer.Value).ToArray());

        if (updatedValue == Value)
            return;
        
        Value = updatedValue;
        Changed?.Invoke(this, updatedValue);
    }
}