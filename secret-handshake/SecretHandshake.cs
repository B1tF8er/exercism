using System;
using System.Collections.Generic;
using System.Linq;

public static class SecretHandshake
{
    private static readonly IDictionary<int, string> commandValues;

    static SecretHandshake()
    {
        commandValues = new Dictionary<int, string>
        {
            { 1, "wink" },
            { 2, "double blink" },
            { 4, "close your eyes" },
            { 8, "jump" }
        };
    }

    public static string[] Commands(int commandValue) =>    
        commandValue.ReverseOrder()
            ? commandValue.GetCommands().Reverse().ToArray()
            : commandValue.GetCommands().ToArray();

    private static bool ReverseOrder(this int commandValue) =>
        (commandValue & 16) != 0;

    private static IEnumerable<string> GetCommands(this int commandValue)
    {
        foreach (var commands in commandValues)
        {
            if ((commandValue & commands.Key) != 0)
                yield return commands.Value;
        }
    }
}
