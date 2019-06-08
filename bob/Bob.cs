using System;
using System.Linq;

public static class Bob
{
    public static string Response(string statement)
    {
        if (statement.IsEmpty())
            return "Fine. Be that way!";
        if (statement.IsQuestion() && statement.IsYell())
            return "Calm down, I know what I'm doing!";
        if (statement.IsQuestion())
            return "Sure.";
        if (statement.IsYell())
            return "Whoa, chill out!";
        
        return "Whatever.";
    }

    private static bool IsQuestion(this string statement) =>
        statement.Trim().EndsWith("?");

    private static bool IsYell(this string statement) => 
        statement.Any(char.IsUpper) && !statement.Any(char.IsLower);

    private static bool IsEmpty(this string statement) =>
        string.IsNullOrEmpty(statement) || string.IsNullOrWhiteSpace(statement);
}