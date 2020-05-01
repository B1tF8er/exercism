module Bob

open System

let private isEmpty (input : string) =
    String.IsNullOrWhiteSpace(input)

let private isYell (input : string) =
    Seq.exists Char.IsLetter input && input = input.ToUpperInvariant()

let private isQuestion (input : string) =
    input.Trim().EndsWith "?"

let private fine = "Fine. Be that way!"
let private camlDown = "Calm down, I know what I'm doing!"
let private chill = "Whoa, chill out!"
let private sure = "Sure."
let private whatevs = "Whatever."

let response (input: string) : string  =
    match input with
    | i when i |> isEmpty -> fine
    | i when i |> isYell && i |> isQuestion -> camlDown
    | i when i |> isYell -> chill
    | i when i |> isQuestion -> sure 
    | _ -> whatevs