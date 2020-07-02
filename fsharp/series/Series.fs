module Series

let slices (value : string) (length : int) =
    match (value, length) with
    | _, length when length < 1 -> None
    | value, length when length > value.Length -> None
    | value, _  when value.Length = 0 -> None
    | _ ->
        let range = [0 .. value.Length - length]
        Some [for i in range -> value.[i..i + length - 1]]