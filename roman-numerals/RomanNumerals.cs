using System;
using System.Collections.Generic;
using System.Linq;
using ArabicToRomanConversions = System.Collections.Generic.Dictionary<int, string>;

public static class RomanNumeralExtension
{
    private static readonly ArabicToRomanConversions conversions = new ArabicToRomanConversions
    {
        { 1000, "M" },
        { 900, "CM" },
        { 500, "D" },
        { 400, "CD" },
        { 100, "C" },
        { 90, "XC" },
        { 50, "L" },
        { 40, "XL" },
        { 10, "X" },
        { 9, "IX" },
        { 5, "V" },
        { 4, "IV" },
        { 1, "I" }
    };

    public static string ToRoman(this int value) =>
        string.Concat(value.FromArabicToRoman());

    private static IEnumerable<string> FromArabicToRoman(this int value)
    {
        foreach (var conversion in conversions)
        {
            while (value / conversion.Key > 0)
            {
                value -= conversion.Key;
                yield return conversion.Value;
            }
        }
    }
}