module SumOfMultiples

let isFactorOfAny numbers number  =
    numbers |> List.exists (fun factor ->
        match factor with
        | 0 -> false
        | _ -> number % factor = 0
    )

let sum (numbers: int list) (upperBound: int): int =
    let isFactorOf = isFactorOfAny numbers

    [1 .. upperBound - 1]
    |> List.filter isFactorOf
    |> List.sum
