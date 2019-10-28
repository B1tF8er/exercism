using System;
using System.Collections.Generic;
using System.Linq;

public enum YachtCategory
{
    Ones = 1,
    Twos = 2,
    Threes = 3,
    Fours = 4,
    Fives = 5,
    Sixes = 6,
    FullHouse = 7,
    FourOfAKind = 8,
    LittleStraight = 9,
    BigStraight = 10,
    Choice = 11,
    Yacht = 12,
}

public static class YachtGame
{
    public static int Score(int[] dice, YachtCategory category)
    {
        switch (category)
        {
            case YachtCategory.Yacht:
                return dice.Distinct().Count() == 1
                    ? 50
                    : 0;
            case YachtCategory.Ones:
                return dice.Where(value => value == 1).Sum();
            case YachtCategory.Twos:
                return dice.Where(value => value == 2).Sum();
            case YachtCategory.Threes:
                return dice.Where(value => value == 3).Sum();
            case YachtCategory.Fours:
                return dice.Where(value => value == 4).Sum();
            case YachtCategory.Fives:
                return dice.Where(value => value == 5).Sum();
            case YachtCategory.Sixes:
                return dice.Where(value => value == 6).Sum();
            case YachtCategory.FullHouse:
                var diceByValue = dice.ToLookup(value => value);
                return diceByValue.Count == 2 && diceByValue.First().Count() == 2 || diceByValue.First().Count() == 3
                    ? dice.Sum()
                    : 0;
            case YachtCategory.FourOfAKind:
                return dice.GroupBy(value => value)
                    .FirstOrDefault(value => value.Count() >= 4)
                    ?.First() * 4 ?? 0;
            case YachtCategory.LittleStraight:
                return dice.OrderBy(value => value).SequenceEqual(new[] { 1, 2, 3, 4, 5 })
                    ? 30
                    : 0;
            case YachtCategory.BigStraight:
                return dice.OrderBy(value => value).SequenceEqual(new[] { 2, 3, 4, 5, 6 })
                    ? 30
                    : 0;
            case YachtCategory.Choice:
                return dice.Sum();
            default:
                return 0;
        }
    }
}

