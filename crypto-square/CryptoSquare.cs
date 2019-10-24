using System;
using System.Collections.Generic;
using System.Linq;

public static class CryptoSquare
{
    public static string NormalizedPlaintext(string plaintext)
        => string.Concat(plaintext.ToLowerInvariant().Where(char.IsLetterOrDigit));

    public static IEnumerable<string> PlaintextSegments(string plaintext)
        => NormalizedPlaintext(plaintext).Segments();

    public static string Encoded(string plaintext)
        => string.Concat(PlaintextSegments(plaintext).Transpose());

    public static string Ciphertext(string plaintext)
        => string.Join(" ", PlaintextSegments(plaintext).AddPad(plaintext).Transpose());

    private static IEnumerable<string> Segments(this string input)
    {
        var chunkSize = input.ChunkSize();

        return input.Length == 0
            ? Enumerable.Empty<string>()
            : Enumerable
                .Range(0, (int) Math.Ceiling(input.Length / (double) chunkSize))
                .Select(it =>
                    input.Substring(it * chunkSize, Math.Min(input.Length - it * chunkSize, chunkSize))
                );
    }

    private static int ChunkSize(this string normalizedPlaintext) =>
        (int)Math.Ceiling(Math.Sqrt(normalizedPlaintext.Length));

    private static IEnumerable<string> Transpose(this IEnumerable<string> input) 
        => input
            .SelectMany(segment =>
                segment.Select((character, index) => new
                {
                    Character = character,
                    Index = index
                })
            )
            .GroupBy(it => it.Index)
            .Select(group => string.Concat(group.Select(it => it.Character)));

    private static IEnumerable<string> AddPad(this IEnumerable<string> segments, string plaintext)
        => segments
            .Select(segment =>
                segment.PadRight(NormalizedPlaintext(plaintext).ChunkSize())
            );
}