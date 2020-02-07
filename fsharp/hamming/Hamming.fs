module Hamming

let toCharArray (s: string) =
    s.ToCharArray();

let different (l, r) =
    l <> r
    
let getDistanceTo (strand1: string) (strand2: string) : int option =
    toCharArray strand1 |> Array.zip <| toCharArray strand2
    |> Array.filter different
    |> Array.length
    |> Some

let distance (strand1: string) (strand2: string): int option =
    if strand1.Length = strand2.Length
    then strand1 |> getDistanceTo <| strand2
    else None
