module Etl

open System

let normalizeLetter letter =
    Char.ToLowerInvariant(letter)

let transformLetterWithScore score lettersWithScore letter = 
    lettersWithScore
    |> Map.add (normalizeLetter letter) score

let transformScoreWithLetters lettersWithScore score letters = 
    letters
    |> List.fold (transformLetterWithScore score) lettersWithScore

let transform scoresWithLetters: Map<char, int> = 
    Map.fold transformScoreWithLetters Map.empty scoresWithLetters