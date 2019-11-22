using System;
using System.Linq;

public class RailFenceCipher
{
    private readonly int rails;
    private readonly int size;

    public RailFenceCipher(int rails) =>
        (this.rails, size) = (rails, rails * 2 - 2);

    public string Encode(string input) =>
        input
            .Select((letter, index) => (Track(index), letter))
            .GroupBy(pair => pair.Item1)
            .Select(group => string.Concat(group.Select(pair => pair.Item2)))
            .Aggregate(string.Empty, Encoder);

    private static Func<string, string, string> Encoder =>
        (accumulator, encoded) => accumulator + encoded;

    public string Decode(string input) =>
        Enumerable.Range(0, input.Length)
            .GroupBy(Track)
            .SelectMany(group => group)
            .Zip(input, Tuple.Create)
            .OrderBy(pair => pair.Item1)
            .Aggregate(string.Empty, Decoder);

    private static Func<string, Tuple<int, char>, string> Decoder =>
        (accumulator, pair) => accumulator + pair.Item2;

    private int Track(int index) =>
        IsCorrect(index) ? 0
        : IsCorrect(index - rails + 1) ? rails - 1
        : Enumerable.Range(1, rails - 1).First(IsTrack(index));

    private bool IsCorrect(int index) =>
        index % size == 0;

    private Func<int, bool> IsTrack(int index) =>
        i => IsCorrect(index - i) || IsCorrect(index - size + i);
}