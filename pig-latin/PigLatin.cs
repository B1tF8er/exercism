using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class PigLatin
{
    public static string Translate(string word) =>
        string.Join(
            " ",
            word.Split(' ').Select(TranslateWord)
        );

    private static string TranslateWord(string word) =>  
        WordStartsWithVowelLike(word)
            ? $"{word}ay"
        : WordStartsWithPrefixes(word, "thr", "sch")
            ? $"{word.Substring(3)}{word.Substring(0, 3)}ay"
        : WordStartsWithPrefixes(word, "ch", "qu", "th", "rh")
            ? $"{word.Substring(2)}{word.Substring(0, 2)}ay"
        : WordStartsWithConsonantAndQu(word)
            ? $"{word.Substring(3)}{word[0]}quay"
            : $"{word.Substring(1)}{word[0]}ay";

    private static bool WordStartsWithVowelLike(string word) =>
        Regex.IsMatch("[aeiou]", word[0].ToString())
        || word.StartsWith("yt")
        || word.StartsWith("xr");

    private static bool WordStartsWithPrefixes(string word, params string[] prefixes) =>
        prefixes.Any(word.StartsWith);

    private static bool WordStartsWithConsonantAndQu(string word) =>
        word.Substring(1).StartsWith("qu");
}