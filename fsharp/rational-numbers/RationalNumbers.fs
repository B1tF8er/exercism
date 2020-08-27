module RationalNumbers

let create (n: int) (d: int) =
    match n, d with
    | _, 0 -> failwith "Denominator cannot be zero"
    | n, d -> (n, d)

let reduce (n, d) =
    let rec gcd a b: int = if b = 0 then a else gcd b (a % b)

    if n = 0 then
        (0, 1)
    else
        let an, ad = abs n, abs d
        let sign = n / an * d / ad
        let dd = gcd an ad
        (an / dd * sign, ad / dd)

let add (a1, b1) (a2, b2) = ((a1 * b2 + a2 * b1), (b1 * b2)) |> reduce

let sub (a1, b1) (a2, b2) = add (a1, b1) (-a2, b2)

let mul (a1, b1) (a2, b2) = ((a1 * a2), (b1 * b2)) |> reduce

let div (a1, b1) (a2, b2) = mul (a1, b1) (b2, a2)

let abs (a1, b1) = (Microsoft.FSharp.Core.Operators.abs a1, Microsoft.FSharp.Core.Operators.abs b1)

let exprational n (a, b) =
    if n = 0 then (1, 1)
    elif n > 0 then (pown a n, pown b n)
    else (pown b (-n), pown a (-n))

let expreal (a: int, b: int) (n: int) = float n ** (float a / float b)