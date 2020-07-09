module IsbnVerifier

open System.Text.RegularExpressions

let [<Literal>] IsbnPattern = "^[0-9]{9}[0-9X]$"

let private digitToInt digit =
    match digit with
    | d when d = 'X' -> 10
    | _ -> int digit - int '0'

let multiply character digit =
    (10 - character) * (digit |> digitToInt) 

let private checkSum isbn = 
    isbn
    |> Seq.mapi multiply
    |> Seq.sum

let private cleanup (isbn: string) =
    isbn.Replace("-", "")

let isValid (isbn: string) = 
    let cleanedUpIsbn = isbn |> cleanup 

    match Regex.IsMatch(cleanedUpIsbn, IsbnPattern) with
    | false -> false
    | true  -> (cleanedUpIsbn |> checkSum) % 11 = 0