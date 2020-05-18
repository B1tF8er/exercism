module CollatzConjecture

let rec private counter seed number =
    match number with 
    | n when n < 1 -> None
    | n when n = 1 -> Some seed
    | n when n % 2 = 0 -> counter (seed + 1) (n / 2)
    | _ -> counter (seed + 1) (number * 3 + 1)

let steps number =
    counter 0 number
 