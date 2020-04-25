module Raindrops

let private factors =
    [(3, "Pling"); (5, "Plang"); (7, "Plong")]

let private isFactorOf factor number =
    number % factor = 0

let private concatSounds soundsSoFar nextSound =
    sprintf "%s%s" soundsSoFar nextSound

let convert (number: int): string =
    let sounds = [
        for (factor, sound) in factors do
            if number |> isFactorOf factor
                then yield sound
    ]

    match sounds with
    | [] -> sprintf "%i" number
    | sounds -> ("", sounds) ||> List.fold concatSounds