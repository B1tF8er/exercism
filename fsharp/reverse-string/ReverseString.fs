module ReverseString

open System

let reverse (input: string): string =
    input
    |> Seq.rev
    |> Seq.toArray
    |> String
