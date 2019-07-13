using System;
using System.Collections.Generic;
using System.Linq;
using static Constants.Numbers;
using static Constants.Sounds;

public static class Raindrops
{
    private static readonly IList<ValueTuple<int, string>> factors;

    static Raindrops()
    {
        factors = new List<ValueTuple<int, string>>
        {
            (Three, Pling),
            (Five, Plang),
            (Seven, Plong)
        };
    }

    public static string Convert(int number)
    {
        var sound = number.GetSound();

        return sound.IsEmpty() ? $"{number}" : sound;
    }

    private static string GetSound(this int number) =>
        factors.Aggregate(string.Empty, GetSoundOf(number));

    private static Func<string, (int, string), string> GetSoundOf(int number) =>
        (accumulator, next) =>
        {
            var tuple = next.ToNamedTuple();
            
            return accumulator + number
                .IsFactorOf(tuple.factor)
                .MakeSound(tuple.sound);
        };

    private static (int factor, string sound) ToNamedTuple(this (int factor, string sound) namedTuple) =>
        namedTuple;

    private static bool IsFactorOf(this int number, int factor) =>
        number % factor == Zero;

    private static string MakeSound(this bool makeSound, string sound) =>
        makeSound ? sound : string.Empty;

    private static bool IsEmpty(this string value) =>
        string.IsNullOrWhiteSpace(value);
}

internal static class Constants
{
    internal static class Numbers
    {
        internal const int Zero = 0;
        internal const int Three = 3;
        internal const int Five = 5;
        internal const int Seven = 7;
    }

    internal static class Sounds
    {
        internal const string Pling = "Pling";
        internal const string Plang = "Plang";
        internal const string Plong = "Plong";
    }
}