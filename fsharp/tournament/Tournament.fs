module Tournament
    
type Result =
    | Win
    | Lost
    | Draw

type Schedule =
    {
        Team: string
        Wins: int
        Losses: int
        Draws: int
        Points: int
    }

let parse (line:string) =
    match line.Split(";") with
    | [| team1; team2; "win"|] -> [
            { Team = team1; Wins = 1; Losses = 0; Draws = 0; Points=3 }
            { Team = team2; Wins = 0; Losses = 1; Draws = 0; Points =0 }
        ]
    | [| team1; team2; "loss"|] -> [
            { Team = team1; Wins = 0; Losses = 1; Draws = 0; Points=0 }
            { Team = team2; Wins = 1; Losses = 0; Draws = 0; Points=3 }
        ]
    | [| team1; team2; "draw"|] -> [
            { Team = team1; Wins = 0; Losses = 0; Draws = 1; Points=1 }
            { Team = team2; Wins = 0; Losses = 0; Draws = 1; Points=1 }
        ]
    | x -> failwithf "Unable to parse %A" x

let condenseSchedule team games = 
    let wins = games |> List.sumBy (fun g -> g.Wins)
    let losses = games |> List.sumBy (fun g -> g.Losses)
    let draws = games |> List.sumBy (fun g -> g.Draws)
    let points = games |> List.sumBy (fun g -> g.Points)

    {Team = team; Wins = wins; Losses = losses; Draws = draws; Points  = points;}

let prettyPrintTeam team =
    sprintf "%-30s |%3d |%3d |%3d |%3d |%3d"
        team.Team
        (team.Wins + team.Losses + team.Draws)
        team.Wins
        team.Draws
        team.Losses
        team.Points

let prettyPrintLeague schedule =
    "Team                           | MP |  W |  D |  L |  P" :: (schedule |> List.map prettyPrintTeam)

let tally input = 
    input 
    |> List.collect parse 
    |> List.groupBy (fun item -> item.Team) 
    |> List.map (fun (teams, games) -> condenseSchedule teams games)
    |> List.sortBy (fun team -> (-team.Points, team.Team))
    |> prettyPrintLeague