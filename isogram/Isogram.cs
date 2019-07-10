using System;
using System.Linq;

public static class Isogram
{
    public static bool IsIsogram(string word) =>
        word.ToLowerInvariant()
            .Where(char.IsLetter)
			.GroupBy(letter => letter)
			.All(letter => letter.Count() == 1);
}
