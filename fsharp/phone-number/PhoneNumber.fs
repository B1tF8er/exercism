module PhoneNumber

open System

let invalidChars = ['+';'.';'-';' ';'(';')']

let cleanInvalidChars (input: string) =
    input
    |> Seq.map Char.ToLowerInvariant
    |> Seq.filter (fun c -> not (Seq.contains c invalidChars))
    |> String.Concat

let validateLength (input: string) : Result<string, string> =
    match String.length input with
    | number when number < 10 -> Error "incorrect number of digits"
    | 10 -> Ok input
    | 11 when input.[0] <> '1' -> Error "11 digits must start with 1"
    | 11 -> Ok (input.Substring 1)
    | _ -> Error "more than 11 digits"

let validateLettersAndPunctuations (input: string) : Result<string, string> =
    match input with
    | digit when Seq.exists Char.IsLetter digit -> Error "letters not permitted"
    | digit when Seq.exists Char.IsPunctuation digit -> Error "punctuations not permitted"
    | digit when Seq.forall Char.IsNumber digit -> Ok input
    | _ -> Error "not a number"

let validateAreaCode (input:string): Result<string, string> =
    match input with
    | i when i.[0] = '0' -> Error "area code cannot start with zero"
    | i when i.[0] = '1' -> Error "area code cannot start with one"
    | _ -> Ok input

let validateExchangeCode (input:string): Result<string, string> =
    match input with
    | i when i.[3] = '0' -> Error "exchange code cannot start with zero"
    | i when i.[3] = '1' -> Error "exchange code cannot start with one"
    | _ -> Ok input

let toResult (input: Result<string, string>) : Result<uint64, string> =
    match input with
    | Ok number -> Convert.ToUInt64 number |> Ok
    | Error message -> Error message

let clean (input: string) =
    input
    |> cleanInvalidChars
    |> validateLength
    |> Result.bind validateLettersAndPunctuations
    |> Result.bind validateAreaCode
    |> Result.bind validateExchangeCode
    |> toResult