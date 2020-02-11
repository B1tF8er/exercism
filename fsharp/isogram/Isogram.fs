module Isogram

open System

let onlyOne (_, values) =
    Seq.length values = 1

let isIsogram str =
    str
    |> Seq.filter Char.IsLetter
    |> Seq.map Char.ToLowerInvariant
    |> Seq.groupBy id
    |> Seq.forall onlyOne