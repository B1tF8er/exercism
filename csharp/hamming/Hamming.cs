using System;
using System.Linq;

public static class Hamming
{
    public static int Distance(string firstStrand, string secondStrand)
    {
        if (firstStrand.Length != secondStrand.Length)
            throw new ArgumentException();

        return firstStrand
            .ToCharArray()
            .Zip(secondStrand.ToCharArray(), (left, right) => new { left, right })
            .Count(pair => pair.left != pair.right);
    }
}