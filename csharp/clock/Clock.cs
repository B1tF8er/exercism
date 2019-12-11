using System;

public class Clock : IEquatable<Clock>
{
    public Clock(int hours, int minutes = 0)
    {
        Hours = Wrap((hours * 60D + minutes) / 60D, 24D);
        Minutes = Wrap(minutes, 60D);
    }

    public int Hours { get; }

    public int Minutes { get; }

    public Clock Add(int minutesToAdd) =>
        new Clock(Hours, Minutes + minutesToAdd);

    public Clock Subtract(int minutesToSubtract) =>
        new Clock(Hours, Minutes - minutesToSubtract);

    public override string ToString() => $"{Hours:00}:{Minutes:00}";

    public bool Equals(Clock other) =>  other is null
        ? false
        : Hours == other.Hours && Minutes == other.Minutes;

    private int Wrap(double amount, double @base) =>
        (int)((amount % @base + @base) % @base);
}