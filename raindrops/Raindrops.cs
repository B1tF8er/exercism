using System;

public static class Raindrops
{
    public static string Convert(int number)
    {
        var sound = number.IsFactorOf(3, "Pling")
            + number.IsFactorOf(5, "Plang")
            + number.IsFactorOf(7, "Plong");

        if (string.IsNullOrEmpty(sound))
            return $"{number}";
        else
            return sound;
    }

    private static string IsFactorOf(this int number, int factor, string sound) =>
        number % factor == 0 ? sound : string.Empty;
}