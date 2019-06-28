using System;
using System.Linq;
using System.Text.RegularExpressions;

public class PhoneNumber
{
    private static readonly Regex phoneNumberFormat = new Regex("^1?([2-9][0-9]{2}[2-9][0-9]{6})$");

    public static string Clean(string phoneNumber)
    {
        var digits = string.Join(string.Empty, phoneNumber.Where(l => l >= '0' && l <= '9'));
        var phoneMatch = phoneNumberFormat.Match(digits);

        if (phoneMatch.Success)
            return phoneMatch.Groups[1].ToString();
        else
            throw new ArgumentException(nameof(phoneNumber));
    }
}