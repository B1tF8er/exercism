using System;

public static class Darts
{
    public static int Score(double x, double y)
    {
        var diff = Math.Pow(x, 2) + Math.Pow(y, 2);
        if (diff > 100)
            return 0;
        if (diff > 25 && diff <= 100)
            return 1;
        if (diff > 1 && diff <= 25)
            return 5;
        if (diff <= 1)
            return 10;

        return 0;
    }
}
