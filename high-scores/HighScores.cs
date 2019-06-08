using System;
using System.Collections.Generic;
using System.Linq;

public class HighScores
{
    private readonly IEnumerable<int> scores;

    public HighScores(List<int> list) => scores = list;

    public List<int> Scores() => scores.ToList();

    public int Latest() => scores.Last();

    public int PersonalBest() => scores.Max();

    public List<int> PersonalTopThree() => scores
        .OrderByDescending(s => s)
        .Take(3)
        .ToList();
}