using System;
using System.Linq;

public static class ResistorColorTrio
{
    private const int KiloOhms = 1_000;

    private static readonly string[] AllColors = new[]
    { 
        "black", "brown", "red", "orange", "yellow",
        "green", "blue", "violet", "grey", "white" 
    };

    public static string Label(string[] colors) =>
        $"{GetMainValue(colors)} " + GetUnit(GetValue(colors));
    
    private static int GetMainValue(string[] colors) =>
        GetValue(colors) is int value && value >= KiloOhms
            ? value / KiloOhms
            : value;

    private static int GetValue(string[] colors) =>
        ((ResistorValue(colors[0]) * 10) + ResistorValue(colors[1])) * (int)Math.Pow(10, ResistorValue(colors[2]));
    
    private static string GetUnit(int value) =>
        (value >= KiloOhms ? "kilo" : string.Empty) + "ohms";
    
    private static int ResistorValue(string color) =>
        Array.IndexOf(AllColors, color);
}
