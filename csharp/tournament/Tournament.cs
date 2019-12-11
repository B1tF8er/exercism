using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Tournament
{
    private static Regex GameResult =>
        new Regex(@"(?<firstTeam>.+?);(?<secondTeam>.+?);(?<result>win|draw|loss)");

    public static void Tally(Stream inStream, Stream outStream)
    {
        var games = GetGames(inStream);
        var teams = GetTeams(games);
        var buffer = CreateBuffer(teams);
        outStream.Write(buffer, 0, buffer.Length);
    }

    private static IEnumerable<string> GetGames(Stream inStream) =>
        new StreamReader(inStream)
            .ReadToEnd()
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .Where(game => GameResult.IsMatch(game));

    private static IEnumerable<Team> GetTeams(IEnumerable<string> games)
    {
        var teams = new List<Team>()
        {
            new Team("Allegoric Alaskans"),
            new Team("Devastating Donkeys"),
            new Team("Blithering Badgers"),
            new Team("Courageous Californians")
        };

        var results = from game in games
            let firstTeamName = GameResult.Match(game).Groups["firstTeam"].Value
            let secondTeamName = GameResult.Match(game).Groups["secondTeam"].Value
            let matchResult = GameResult.Match(game).Groups["result"].Value
            let firstTeam = teams.FirstOrDefault(t => t.Name == firstTeamName)
            let secondTeam = teams.FirstOrDefault(t => t.Name == secondTeamName)
            select (matchResult, firstTeam, secondTeam);

        foreach (var (matchResult, firstTeam, secondTeam) in results)
        {
            if (matchResult == "win")
            {
                firstTeam.Wins++;
                secondTeam.Losses++;
            }

            if (matchResult == "loss")
            {
                firstTeam.Losses++;
                secondTeam.Wins++;
            }

            if (matchResult == "draw")
            {
                firstTeam.Draws++;
                secondTeam.Draws++;
            }
        }

        return teams;
    }

    private static byte[] CreateBuffer(IEnumerable<Team> teams)
    {
        var output = new StringBuilder();
        output.AppendLine("Team                           | MP |  W |  D |  L |  P");

        foreach (var team in GetSortedTeams(teams))
            output.AppendLine(CreateGameRow(team));

        var games = output.ToString().Substring(0, output.Length - 1);
        return new UTF8Encoding().GetBytes(games);
    }

    private static IOrderedEnumerable<Team> GetSortedTeams(IEnumerable<Team> teams) => teams
        .Where(t => t.Games > 0)
        .OrderBy(t => t.Name)
        .OrderByDescending(t => t.Points);

    private static string CreateGameRow(Team team) =>
        $"{team.Name.PadRight(31)}|  {team.Games} |  {team.Wins} |  {team.Draws} |  {team.Losses} |  {team.Points}";

    private class Team
    {
        public string Name { get; }
        public int Wins { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int Games => Wins + Draws + Losses;
        public int Points => (Wins * 3) + Draws;

        public Team(string name) => Name = name;
    }
}
