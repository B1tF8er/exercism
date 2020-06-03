module Grains

open System.Numerics

let private reducer x y =
    match x, y with
    | Ok a, Ok b -> Ok (a + b)
    | _ -> Error "Invalid input"

let square (n:int) =
    match n with
    | n when n <= 0 || n > 64 -> Error "square must be between 1 and 64"
    | _ ->  Ok (2I ** (n - 1) |> uint64)

let total = 
    [1 .. 64]
    |> List.map square
    |> List.reduce reducer