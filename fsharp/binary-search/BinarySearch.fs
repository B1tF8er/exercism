module BinarySearch

let rec private binarySearch index value =
    function
    | [||] ->
        None
    | input ->   
        let middle = input.Length / 2
        let middleValue = input.[middle]

        match value with
        | value when value < middleValue ->
            input.[0 .. middle - 1] |> binarySearch index value
        | value when value > middleValue ->
            input.[middle + 1 ..] |> binarySearch (index + middle + 1) value 
        | _ ->
            Some (index + middle)

let find input value =
    match (input |> Array.sort) <> input with
    | true -> failwith "The input must be an ordered lists"
    | false -> input |> binarySearch 0 value