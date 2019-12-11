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
                return dice.IsYacht();
            case YachtCategory.Ones:
            case YachtCategory.Twos:
            case YachtCategory.Threes:
            case YachtCategory.Fours:
            case YachtCategory.Fives:
            case YachtCategory.Sixes:
                return dice.IsAnyCombinationOf(category);
            case YachtCategory.FullHouse:
                return dice.IsFullHouse();
            case YachtCategory.FourOfAKind:
                return dice.IsFourOfAKind();
            case YachtCategory.LittleStraight:
                return dice.IsLittleStraight();
            case YachtCategory.BigStraight:
                return dice.IsBigStraight();
            case YachtCategory.Choice:
                return dice.Sum();
            default:
                return 0;
        }
    }

    private static int IsYacht(this int[] dice)
        => dice.Distinct().Count() == 1
            ? 50
            : 0;

    private static int IsAnyCombinationOf(this int[] dice, YachtCategory yachtCategory)
        => dice.Where(value => value == (int)yachtCategory)
               .Sum();

    private static int IsFullHouse(this int[] dice)
        => dice.GroupBy(value => value)
               .All(diceGroup => diceGroup.Count() == 2 || diceGroup.Count() == 3)
            ? dice.Sum()
            : 0;

    private static int IsFourOfAKind(this int[] dice)
        => dice.GroupBy(value => value)
               .FirstOrDefault(value => value.Count() >= 4)
               ?.First() * 4 ?? 0;

    private static int IsLittleStraight(this int[] dice)
        => dice.OrderBy(value => value).SequenceEqual(Enumerable.Range(1, 5))
            ? 30
            : 0;

    private static int IsBigStraight(this int[] dice)
        => dice.OrderBy(value => value).SequenceEqual(Enumerable.Range(2, 5))
            ? 30
            : 0;
}

