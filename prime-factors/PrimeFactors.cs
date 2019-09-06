using System;
using System.Collections.Generic;

public static class PrimeFactors
{
    public static long[] Factors(long number)
    {
        var divisor = 2;
        var factors = new List<long>();

        while (number > 1)
        {
            while (number % divisor == 0)
            {
                factors.Add(divisor);
                number /= divisor;
            }
            divisor++;
        }

        return factors.ToArray();
    }
}