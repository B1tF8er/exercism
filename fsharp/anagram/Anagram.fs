module Anagram

open System

let normalize (target : string) =
    let valueNormalized =
        target.ToLowerInvariant().ToCharArray()
        |> Array.sort

    new string(valueNormalized)

let unequal target other =
    let same =
        String.Equals(target, other, StringComparison.InvariantCultureIgnoreCase)
    
    not (same)
       
let findAnagrams sources target = 
    let isMatch source =
        (source |> normalize) = (target |> normalize) && target |> unequal source
    
    sources
    |> List.filter isMatch