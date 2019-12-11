using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class IsbnVerifier
{
    public static bool IsValid(string number)
    {
        var isbn = number.Replace("-", string.Empty);
        
        return HasValidFormat(isbn)
            ? IsValidIsbn10(isbn)
            : false;
    }

    private static bool HasValidFormat(string isbn) =>
        Regex.IsMatch(isbn, @"^(\d{9}[\dX])$");

    private static bool IsValidIsbn10(string isbn) =>
        isbn.Select(ToInt)
            .Select(ToIsbn10)
            .Sum() % 11 == 0;

    private static Func<char, int> ToInt => @char =>
    {
        if (@char == 'X')
            return 10;

        return Convert.ToInt32(@char.ToString());
    };

    private static Func<int, int, int> ToIsbn10 =>
        (digit, index) => digit * (10 - index);
}