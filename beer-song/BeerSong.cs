using System.Text;

public static class BeerSong
{
    private const string NoMore = "No more bottles of beer on the wall, no more bottles of beer.\nGo to the store and buy some more, 99 bottles of beer on the wall.\n";
    private const string One = "1 bottle of beer on the wall, 1 bottle of beer.\nTake it down and pass it around, no more bottles of beer on the wall.\n";
    private const string Two = "2 bottles of beer on the wall, 2 bottles of beer.\nTake one down and pass it around, 1 bottle of beer on the wall.\n";
    private const string Other = "{0} bottles of beer on the wall, {0} bottles of beer.\nTake one down and pass it around, {1} bottles of beer on the wall.\n";

    public static string Recite(int startBottles, int takeDown)
    {
        var beerSongBuilder = new StringBuilder();

        while (startBottles >= 0 && takeDown > 0)
        {
            beerSongBuilder.Append(Verse(startBottles));
            startBottles--;
            takeDown--;

            if (takeDown > 0)
                beerSongBuilder.AppendLine();
        }

        return beerSongBuilder.ToString().Substring(0, beerSongBuilder.Length - 1);
    }

    private static string Verse(int bottles)
    {
        switch (bottles)
        {
            case 0: return NoMore;
            case 1: return One; 
            case 2: return  Two;
            default: return string.Format(Other, bottles, bottles -1);
        }
    }
}