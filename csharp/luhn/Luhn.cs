using System;
using System.Linq;

public static class Luhn
{
    public static bool IsValid(string number)
    {
        var normalizedNumber = number
            .Replace(" ", string.Empty);
        
        if (normalizedNumber.IsInvalid())
           return false;

        return normalizedNumber
            .GetChecksum()
            .IsDivisibleByTen();
    }

    private static bool IsInvalid(this string number)
        => number.Length < 2 || !number.All(char.IsDigit);

    private static int GetChecksum(this string number)
        => number
            .Select(digit => int.Parse(digit.ToString()))
            .Reverse()
            .Select((digit, index) =>
                index.IsDivisibleByTwo()
                    ? digit
                    : digit.Double()
            )
            .Sum();

    private static bool IsDivisibleByTwo(this int number)
        => number % 2 == 0;

    private static int Double(this int number)
    {
        var doubled = number * 2;
        return doubled < 10 ? doubled : doubled - 9;
    }

    private static bool IsDivisibleByTen(this int number)
        =>  number % 10 == 0;
}