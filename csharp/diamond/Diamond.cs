using System;
using System.Collections.Generic;
using System.Linq;
using LetterIndexPair = System.Tuple<char, int>;

public static class Diamond
{
    
    public static string Make(char target)
    {
        var letters = GetLetters(target);
        var diamondLetters = letters.Concat(letters.Reverse().Skip(1)).ToArray();

        return string.Join(
            "\n", MakeLines(letters, diamondLetters)
        );
    }

    private static LetterIndexPair[] GetLetters(char target) =>
        Enumerable
            .Range('A', target - 'A' + 1)
            .Select((@char, index) => Tuple.Create((char)@char, index))
            .ToArray();

    private static IEnumerable<string> MakeLines(LetterIndexPair[] letters, LetterIndexPair[] diamondLetters)
        => diamondLetters
            .Select(diamondLetter =>
                MakeLine(letters.Length, diamondLetter)
            );

    private static string MakeLine(int letterCount, LetterIndexPair letterIndexPair)
    {
        var letter = letterIndexPair.Item1;
        var row = letterIndexPair.Item2;
        var outerSpaces = "".PadRight(letterCount - row - 1);
        var innerSpaces = "".PadRight(row == 0 ? 0 : row * 2 - 1);

        return letter == 'A'
            ? $"{outerSpaces}{letter}{outerSpaces}"
            : $"{outerSpaces}{letter}{innerSpaces}{letter}{outerSpaces}";
    }
}