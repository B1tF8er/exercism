﻿module PigLatin

open System.Text.RegularExpressions

let vowelRegex = Regex(@"(?<begin>^|\s+)(?<vowel>a|e|i|o|u|yt|xr)(?<rest>\w+)", RegexOptions.Compiled)
let consonantRegex = Regex(@"(?<begin>^|\s+)(?<consonant>ch|qu|thr|th|rh|sch|yt|\wqu|\w)(?<rest>\w+)", RegexOptions.Compiled)

let vowelReplacement = "${begin}${vowel}${rest}ay";
let consonantReplacement = "${begin}${rest}${consonant}ay";

let translate input =
    match input |> vowelRegex.IsMatch with
    | true -> vowelRegex.Replace(input, vowelReplacement)
    | false -> consonantRegex.Replace(input, consonantReplacement)