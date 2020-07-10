module RomanNumerals

let private arabicToRoman =
    [
        (1000, "M")
        (900,  "CM")
        (500,  "D")
        (400,  "CD")
        (100,  "C")
        (90,   "XC")
        (50,   "L")
        (40,   "XL")
        (10,   "X")
        (9,    "IX")
        (5,    "V")
        (4,    "IV")
        (1,    "I")
    ]

let rec private toRomanLoop remainder acc thresholds =         
    match thresholds with
    | [] -> acc
    | (threshold, numeral)::xs ->
        match threshold <= remainder with
        | true -> toRomanLoop (remainder - threshold) (acc + numeral) thresholds
        | false -> toRomanLoop remainder acc xs

let roman (arabicNumeral: int) =
    toRomanLoop arabicNumeral "" arabicToRoman