using System;
using System.Linq;

public static class ResistorColorDuo
{
    private enum Color
    {
        black,
        brown,
        red,
        orange,
        yellow,
        green,
        blue,
        violet,
        grey,
        white
    }

    public static int Value(string[] colors) =>
        Convert.ToInt16(string.Join(
            string.Empty,
            colors.Select(ToColorValue).Take(2)
        ));

    private static int ToColorValue(string color) =>
        (int)((Color)Enum.Parse(typeof(Color), color));
}