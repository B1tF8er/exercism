using System;
using System.Linq;

public class Anagram
{
    private readonly string baseWord;
    private readonly string orderedChars;

    public Anagram(string baseWord)
    {
        this.baseWord = baseWord;
        orderedChars = ToOrderedChars(baseWord);
    }

    public string[] FindAnagrams(string[] potentialMatches) =>
        potentialMatches
            .Where(IsDifferentWord)
            .Where(IsAnagram)
            .Distinct()
            .ToArray();

    private Func<string, bool> IsDifferentWord =>
        (word) => !AreEqualWords(word, baseWord);

    private Func<string, bool> IsAnagram =>
        (word) => AreEqualWords(ToOrderedChars(word), orderedChars);


    private bool AreEqualWords(string left, string right) =>
        left.Equals(right, StringComparison.InvariantCultureIgnoreCase);

    private string ToOrderedChars(string word) =>
        string.Concat(word.ToLowerInvariant().OrderBy(letter => letter));
}