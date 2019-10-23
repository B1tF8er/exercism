using System;
using System.Collections.Generic;
using System.Linq;

public static class ScaleGenerator
{
    private static readonly string[] ChromaticScale =
    {
        "C", "C#", "D", "D#", "E", "F",
        "F#", "G", "G#", "A", "A#", "B"
    };
    private static readonly string[] FlatChromaticScale =
    {
        "C", "Db", "D", "Eb", "E", "F",
        "Gb", "G", "Ab", "A", "Bb", "B"
    };
    private static readonly string[] FlatKeys =
    {
        "F", "Bb", "Eb", "Ab", "Db", "Gb",
        "d", "g", "c", "f", "bb", "eb"
    };
    private static readonly IDictionary<char, int> Intervals = new Dictionary<char, int>
    {
        ['m'] = 1, ['M'] = 2, ['A'] = 3
    };

    public static string[] Chromatic(string tonic)
        => Interval(tonic, "mmmmmmmmmmmm");

    public static string[] Interval(string tonic, string pattern)
    {
        var scale = Scale(tonic);
        var index = Array.FindIndex(scale, Match(tonic));
        var shiftedScale = Shift(index, scale);

        var pitches = new List<string>();

        foreach (var interval in pattern)
        {
            pitches.Add(shiftedScale[0]);
            shiftedScale = SkipInterval(interval, shiftedScale);
        }

        return pitches.ToArray();
    }

    private static string[] Scale(string tonic) =>
        FlatKeys.Contains(tonic) ? FlatChromaticScale : ChromaticScale;

    private static Predicate<string> Match(string tonic) =>
        pitch => string.Equals(pitch, tonic, StringComparison.OrdinalIgnoreCase);

    private static string[] Shift(int index, string[] scale) =>
        scale.Skip(index).Concat(scale.Take(index)).ToArray();

    private static string[] SkipInterval(char interval, string[] scale) =>
        scale.Skip(Intervals[interval]).ToArray();
}