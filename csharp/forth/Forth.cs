using System;
using System.Collections.Generic;
using System.Linq;

public static class Forth
{
    private static readonly HashSet<char> nonSeparators = new HashSet<char>("+-*/:;");

    private static readonly char[] separators = Enumerable.Range(0, 128)
        .Select(c => (char)c)
        .Where(c => !nonSeparators.Contains(c) && !char.IsLetterOrDigit(c))
        .ToArray();

    public static string Evaluate(string[] programText)
    {
        if (!programText.Any())
            return string.Empty;

        var commands = string.Join(" ", programText);
        var inputs = new Stack<string>(commands.ToUpper().Split(separators).Reverse());
        var operations = new Stack<int>();
        var custom = new Dictionary<string, string[]>();

        while (inputs.Any())
        {
            var value = inputs.Pop();

            if (IsNumber(value))
            {
                operations.Push(int.Parse(value));
                continue;
            }

            if (custom.ContainsKey(value))
            {
                foreach (var w in custom[value].Reverse())
                    inputs.Push(w);

                continue;
            }

            switch (value)
            {
                case "+":
                    operations.Push(operations.Pop() + operations.Pop());
                    break;
                case "-":
                    operations.Push(-operations.Pop() + operations.Pop());
                    break;
                case "*":
                    operations.Push(operations.Pop() * operations.Pop());
                    break;
                case "/":
                    if (operations.Peek() == 0)
                        throw new InvalidOperationException();

                    operations.Push((int)((1 / (double)operations.Pop()) * operations.Pop()));

                    break;
                case "DUP":
                    operations.Push(operations.Peek());

                    break;
                case "DROP":
                    operations.Pop();

                    break;
                case "SWAP":
                    foreach (var t in new[] { operations.Pop(), operations.Pop() })
                        operations.Push(t);

                    break;
                case "OVER":
                    foreach (var t in new[] { operations.Pop(), operations.Peek() })
                        operations.Push(t);

                    break;
                case ":":
                    var key = inputs.Pop();

                    if (IsNumber(key))
                        throw new InvalidOperationException();

                    var values = new List<string>();
                    value = inputs.Pop();

                    while (!value.Equals(";"))
                    {
                        values.Add(value);
                        value = inputs.Pop();
                    }

                    custom[key] = values
                        .SelectMany(v => custom.ContainsKey(v) ? custom[v] : new[] { v })
                        .ToArray();

                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        return string.Join(" ", operations.Reverse());
    }

    private static bool IsNumber(string s)
    {
        if (s.StartsWith("-"))
            s = s.Substring(1);

        if (s.Equals(string.Empty))
            return false;

        return s.All(c => char.IsDigit(c));
    }
}