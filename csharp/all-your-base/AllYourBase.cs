using System;
using System.Collections.Generic;
using System.Linq;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        Guard(inputBase, inputDigits, outputBase);

        if (inputDigits.Length == 0 || inputDigits.All(d => d == 0))
            return new int[] { 0 };

        var units = GetUnits(inputBase, inputDigits);
        return GetDigits(outputBase, units, GetMaxPower(outputBase, units));
    }

    private static void Guard(int inputBase, int[] inputDigits, int outputBase)
    {
        if (outputBase <= 1 || inputBase <= 1 || inputDigits.Any(d => d >= inputBase || d < 0))
            throw new ArgumentException();
    }

    private static int GetUnits(int inputBase, int[] inputDigits)
    {
        int units = 0;
        int maxPower = inputDigits.Length - 1;

        for (int i = 0; i < inputDigits.Length; i++, maxPower--)
            units += inputDigits[i] * (int)Math.Pow(inputBase, maxPower);
        return units;
    }

    private static int[] GetDigits(int outputBase, int units, int outBaseMaxPower)
    {
        int[] digits = new int[outBaseMaxPower + 1];

        for (int i = 0; i < digits.Length; i++)
            digits[i] = (units % (int)Math.Pow(outputBase, digits.Length - i)) / (int)Math.Pow(outputBase, digits.Length - i - 1);

        return digits;
    }

    private static int GetMaxPower(int outputBase, int units)
    {
        var outBaseMaxPower = 0;

        while (Math.Pow(outputBase, outBaseMaxPower) <= units)
            outBaseMaxPower++;

        outBaseMaxPower--;
        return outBaseMaxPower;
    }
}