module Leap

(* Divisible By Operator *)
let (%%) x y =
    x % y = 0

(* Not Divisible By Operator *)
let (^%%) x y =
    x % y <> 0

let leapYear (year: int): bool =
    year %% 4
    && year ^%% 100
    || year %% 400