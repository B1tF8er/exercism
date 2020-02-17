module Leap

let divisibleBy x y =
    y % x = 0

let leapYear (year: int): bool =
    divisibleBy 4 year && not (divisibleBy 100 year) || divisibleBy 400 year