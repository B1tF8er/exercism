module Proverb

let line (want, lost) =
    sprintf "For want of a %s the %s was lost." want lost

let ending input =
    sprintf "And all for the want of a %s." (input |> List.head)

let recite input =
    match input with
    | [] -> []
    | _ ->
        let lines =
            input
            |> List.pairwise
            |> List.map line
        [ending input] |> List.append lines 