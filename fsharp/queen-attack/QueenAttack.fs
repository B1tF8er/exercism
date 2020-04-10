module QueenAttack

open System

let abs (x:int) =
    Math.Abs x

let isValidPosition position =
    position >= 0 && position < 8

let create (row, col) =
    row |> isValidPosition && col |> isValidPosition

let canAttack (row1, col1) (row2, col2) =
    if row1 = row2 && col1 = col2 then false
    else row1 = row2 || col1 = col2 || (row1 - row2 |> abs) = (col1 - col2 |> abs)