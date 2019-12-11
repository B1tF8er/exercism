using System;
using System.Collections.Generic;
using System.Linq;

public static class ProteinTranslation
{
    private static IDictionary<string[], string> proteins;

    static ProteinTranslation()
    {
        proteins = new Dictionary<string[], string>
        {
            { new string[] { "AUG" }, "Methionine" },
            { new string[] { "UUU", "UUC" }, "Phenylalanine" },
            { new string[] { "UUA", "UUG" }, "Leucine" },
            { new string[] { "UCU", "UCC", "UCA", "UCG" }, "Serine" },
            { new string[] { "UAU", "UAC" }, "Tyrosine" },
            { new string[] { "UGU", "UGC" }, "Cysteine" },
            { new string[] { "UGG" }, "Tryptophan" },
            { new string[] { "UAA", "UAG", "UGA" }, "STOP" }
        };
    }

    public static string[] Proteins(string strand) =>
        strand
            .Select((letter, index) => new {letter, index})
            .GroupBy(it => it.index / 3)
            .Select(group => new string(group.Select(it => it.letter).ToArray()))
            .Select(codon => proteins.Keys.FirstOrDefault(codons => Array.IndexOf(codons, codon) > -1))
            .Select(codon => proteins[codon])
            .TakeWhile(protein => protein != "STOP")
            .ToArray();
}