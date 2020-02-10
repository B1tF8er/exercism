module Pangram

let normalize (s: string) =
    s.ToLowerInvariant()

let contains (s: string) (c: char)  =
    let n = normalize s
    n.Contains(c)

let isPangram (input: string): bool =
    Seq.forall (input |> contains) <| ['a'..'z']