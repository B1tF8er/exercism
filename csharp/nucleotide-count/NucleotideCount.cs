using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        var nucleotides = new Dictionary<char, int>
        {
            {'A', 0},
            {'C', 0},
            {'G', 0},
            {'T', 0}
        };

        sequence
            .ToList()
            .ForEach(nucleotide => {
                if (nucleotides.TryGetValue(nucleotide, out var count))
                    nucleotides[nucleotide] = count + 1;
                else
                    throw new ArgumentException();
            });

        return nucleotides;
    }
}