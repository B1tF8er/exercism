module QueenAttack

open System

let abs (x:int) =
    Math.Abs x

let isValidPosition position =
    position >= 0 && position < 8

let create (row, col) =
    row |> isValidPosition && col |> isValidPosition

let sameRowAs row1 row2 =
    row1 = row2

let sameColumnAs col1 col2 =
    col1 = col2

let inRangeOf (row1, col1) (row2, col2) =
    (row1 - row2 |> abs) = (col1 - col2 |> abs)

let canAttack (row1, col1) (row2, col2) =
    row2 |> sameRowAs row1 ||  col2 |> sameColumnAs col1 || (row2, col2) |> inRangeOf (row1, col1)