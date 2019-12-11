using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class RotationalCipher
{
    private static readonly string lowerCaseAlphabet = CreateAlphabet('a', 'z');

    private static readonly string upperCaseAlphabet = CreateAlphabet('A', 'Z');

    public static string Rotate(string text, int shiftKey) =>
        ConvertCharsToString(text.Select(Rotate(shiftKey)));

    private static Func<char, char> Rotate(int shiftKey) =>
        letter => Rotate(letter, shiftKey);

    private static char Rotate(char letter, int shiftKey)
    {
        if (!char.IsLetter(letter))
            return letter;

        var alphabet = char.IsLower(letter)
            ? lowerCaseAlphabet
            : upperCaseAlphabet;

        var newPosition = alphabet.IndexOf(letter) + shiftKey;
        var cipheredLetter = alphabet[newPosition % alphabet.Length];

        return cipheredLetter;
    }

    private static string CreateAlphabet(char a, char z) =>
        ConvertCharsToString(Enumerable
            .Range(a, z - a + 1)
            .Select(i => (Char)i));

    private static string ConvertCharsToString(IEnumerable<char> chars) =>
        string.Join(string.Empty, chars);
}
