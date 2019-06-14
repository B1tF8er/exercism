using System;
using System.Linq;
using static System.Math;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number) => number
        .ToString()
        .Select(digit => Convert.ToInt32(digit - 48))
        .Sum(digit => Pow(digit, number.ToString().Length)) == number;
}