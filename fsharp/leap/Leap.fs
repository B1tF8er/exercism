module Leap

(* Divisible By Operator *)
let divisibleBy x y =
    x % y = 0

(* Not Divisible By Operator *)
let notDivisibleBy x y =
    x % y <> 0

let leapYear (year: int): bool =
    year |> divisibleBy <| 4 && year |> notDivisibleBy <| 100 || year |> divisibleBy <| 400