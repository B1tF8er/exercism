using System;
using System.Collections.Generic;
using static System.Math;

public static class PythagoreanTriplet
{
    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        for (var c = sum / 2; c > 1; c--)
        {
            var left = sum - c;

            for (int a = 1, b = left - a; b > a; a++, b--)
            {
                var ruleOne = a * a + b * b == c * c;
                var ruleTwo = a < b && b < c;
            
                if (ruleOne && ruleTwo)
                    yield return (a, b, c);
            }
        }
    }
}