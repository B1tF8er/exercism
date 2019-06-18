using System;
using System.Collections.Generic;
using System.Linq;

public static class RnaTranscription
{
    private static readonly IDictionary<char, char> dnaToRna;

    static RnaTranscription() =>
        dnaToRna = new Dictionary<char, char>
        {
            { 'G', 'C' },
            { 'C', 'G' },
            { 'T', 'A' },
            { 'A', 'U' }
        };

    public static string ToRna(string nucleotide) =>
        nucleotide.Any(dnaToRna.ContainsKey)
            ? string.Concat(nucleotide.Select(FromDnaToRna))
            : string.Empty;

    private static Func<char, char> FromDnaToRna =>
        nucleotide => dnaToRna[nucleotide];
}