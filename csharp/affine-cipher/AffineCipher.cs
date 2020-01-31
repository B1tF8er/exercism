using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mod = System.Func<int, int, int, int>;

public static class AffineCipher
{
    private static int m = 26;

    public static string Encode(string plainText, int a, int b)
    {
        var inverse = FindInverse(a, m);

        return plainText
            .Where(char.IsLetterOrDigit)
            .Select((c) => Encrypt(c, a, b))
            .Select((e, i) => (i > 0 && i % 5 == 0) ? $" {e}" : $"{e}")
            .Aggregate(
                string.Empty,
                (accumulator, next) => $"{accumulator}{next}"
            );
    }

    public static string Decode(string cipheredText, int a, int b)
    {
        var inverse = FindInverse(a, m);

        return cipheredText
            .Where(char.IsLetterOrDigit)
            .Select((c) => Decrypt(c, inverse, b))
            .Aggregate(
                string.Empty,
                (accumulator, next) => $"{accumulator}{next}"
            );
    }

    private static char Decrypt(char c, int a, int b) =>
        Cipher(c, a, b, GetDecryptMod);

    private static char Encrypt(char c, int a, int b) =>
        Cipher(c, a, b, GetEncryptMod);

    private static char Cipher(char c, int a, int b, Mod GetMod)
    {
        if (char.IsDigit(c))
            return c;

        if (char.IsUpper(c))
            c = char.ToLower(c);

        var x = c - 97;
        var mod = GetMod(a, b, x);

        if (mod < 0)
            mod = mod + m;

        return (char)(mod + 97);
    }

    private static Mod GetEncryptMod =>
        (a, b, x) => (a * x + b) % m;

    private static Mod GetDecryptMod =>
        (a, b, x) => ((x - b) * a) % m;

    private static int FindInverse(int a, int b)
    {
        var (x0, x1, y0, y1) = (1, 0, 0, 1);

        while (a != 0)
        {
            var q = b / a;
            (b, a) = (a, b % a);
            (x0, x1) = (x1, x0 - q * x1);
            (y0, y1) = (y1, y0 - q * y1);
        }

        if (b > 1)
            throw new ArgumentException("a and m must be coprime.");

        return y0;
    }
}