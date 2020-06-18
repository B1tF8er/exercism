module ScrabbleScore

let private letterScore (letters, score) =
    letters
    |> Seq.map (fun letter -> (letter, score))

let private letterScores = 
    [
        ("AEIOULNRST", 1)
        ("DG", 2)
        ("BCMP", 3)
        ("FHVWY", 4)
        ("K", 5)
        ("JX", 8)
        ("QZ", 10)
    ] 
    |> List.map letterScore
    |> Seq.concat 
    |> Map.ofSeq

let scoreLetter letter =
    let score =
        letterScores
        |> Map.tryFind letter
    
    0 |> defaultArg score

let score (word:string) =
    word.ToUpperInvariant()
    |> Seq.sumBy scoreLetter