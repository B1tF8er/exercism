using System;

public static class Leap
{
    public static bool IsLeapYear(int year)
    {
        Func<bool> isDivisibleByFour = () => year % 4 == 0;
        Func<bool> isDivisibleByOneHundred = () => year % 100 == 0;
        Func<bool> isDivisibleByFourHundred = () => year % 400 == 0;

        if (isDivisibleByOneHundred())
            return isDivisibleByFourHundred();

        return isDivisibleByFour();
    }
}