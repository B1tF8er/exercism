using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class LedgerEntry
{
    public LedgerEntry(string date, string description, int change)
    {
        Date = DateTime.Parse(date, CultureInfo.InvariantCulture);
        Description = description;
        Change = change / 100.0m;
    }

    public DateTime Date { get; }
    public string Description { get; }
    public decimal Change { get; }
}

public static class Ledger
{
    public static LedgerEntry CreateEntry(string date, string description, int change)
         => new LedgerEntry(date, description, change);

    private static CultureInfo CreateCulture(string currency, string location)
    {
        if (!IsValidCurrency(currency) || !IsValidLocation(location))
            throw new ArgumentException("Invalid currency");

        return GetCulture(currency, location);
    }

    private static bool IsValidCurrency(string currency)
        => currency == "USD" || currency == "EUR";

    private static bool IsValidLocation(string location)
        => location == "nl-NL" || location == "en-US";

    private static CultureInfo GetCulture(string currency, string locale)
    {
        var culture = CultureInfo(locale);
        culture.NumberFormat.CurrencySymbol = CurrencySymbol(currency);
        culture.NumberFormat.CurrencyNegativePattern = CurrencyNegativePattern(locale);
        culture.DateTimeFormat.ShortDatePattern = ShortDateFormat(locale);
        return culture;
    }

    private static CultureInfo CultureInfo(string locale)
    {
        switch (locale)
        {
            case "en-US":
            case "nl-NL":
                return new CultureInfo(locale);
            default:
                throw new ArgumentException("Invalid locale");
        }
    }

    private static string CurrencySymbol(string currency)
    {
        switch (currency)
        {
            case "USD": return "$";
            case "EUR": return "€";
            default: throw new ArgumentException("Invalid currency");
        }
    }

    private static int CurrencyNegativePattern(string locale)
    {
        switch (locale)
        {
            case "en-US": return 0;
            case "nl-NL": return 12;
            default: throw new ArgumentException("Invalid locale");
        }
    }

    private static string ShortDateFormat(string locale)
    {
        switch (locale)
        {
            case "en-US": return "MM/dd/yyyy";
            case "nl-NL": return "dd/MM/yyyy";
            default: throw new ArgumentException("Invalid locale");
        }
    }

    private static string PrintHead(string locale)
    {
        switch (locale)
        {
            case "en-US": return "Date       | Description               | Change       ";
            case "nl-NL": return "Datum      | Omschrijving              | Verandering  ";
            default: throw new ArgumentException("Invalid locale");
        }
    }

    private static string PrintEntry(this IFormatProvider culture, LedgerEntry entry)
        => $"{Date(culture, entry.Date)} | {Description(entry.Description)} | {Change(culture, entry.Change)}";

    private static string Date(IFormatProvider culture, DateTime date)
        => date.ToString("d", culture);

    private static string Description(string description)
        => description.Length > 25
            ? string.Format("{0,-25}", $"{description.Substring(0, 22)}...")
            : string.Format("{0,-25}", description);

    private static string Change(IFormatProvider culture, decimal change)
        => change < 0.0m
            ? string.Format("{0,13}", change.ToString("C", culture))
            : string.Format("{0,13}", $"{change.ToString("C", culture)} ");

    private static IEnumerable<LedgerEntry> Sort(this LedgerEntry[] entries)
        => entries.Filter(entry => entry.Change < 0)
            .Union(entries.Filter(entry => entry.Change >= 0));

    private static IOrderedEnumerable<LedgerEntry> Filter(this LedgerEntry[] entries, Func<LedgerEntry, bool> predicate)
        => entries
            .Where(predicate)
            .OrderBy(entry => entry.Date)
            .ThenBy(entry => entry.Description)
            .ThenBy(entry => entry.Change);

    public static string Format(string currency, string locale, LedgerEntry[] entries)
        => entries
            .Sort()
            .Aggregate(
                PrintHead(locale),
                (accumulator, next) => $"{accumulator}\n{CreateCulture(currency, locale).PrintEntry(next)}"
            );
}
