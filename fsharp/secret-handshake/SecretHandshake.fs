module SecretHandshake

let commandsMapping = 
    [
        (1 <<< 0, (fun accumulator -> accumulator @ ["wink"]));
        (1 <<< 1, (fun accumulator -> accumulator @ ["double blink"]));
        (1 <<< 2, (fun accumulator -> accumulator @ ["close your eyes"]));
        (1 <<< 3, (fun accumulator -> accumulator @ ["jump"]))
        (1 <<< 4, (fun accumulator -> accumulator |> List.rev))
    ]

let applyCommand number accumulator (mask, apply) =
    if number &&& mask <> 0 then apply accumulator
    else accumulator

let commands number =
    ([], commandsMapping)
    ||> List.fold (applyCommand number)