module Yacht

type Category = 
    | Ones
    | Twos
    | Threes
    | Fours
    | Fives
    | Sixes
    | FullHouse
    | FourOfAKind
    | LittleStraight
    | BigStraight
    | Choice
    | Yacht

type Die =
    | One 
    | Two 
    | Three
    | Four 
    | Five 
    | Six

let value die =
    match die with
    | One -> 1
    | Two -> 2
    | Three -> 3
    | Four -> 4
    | Five -> 5
    | Six -> 6

let private straightPoints = 30

let private yachtPoints = 50

let private countsOf =
    List.countBy id >> List.map snd >> List.sort

let private countBy die =
    List.filter ((=) die) >> List.length

let private total =
    List.fold (fun state d -> state + value d) 0

let score (category: Category) (dice: Die list) =
    match (category, countsOf dice) with
    | (Ones, _) ->
        (dice |> countBy Die.One) * value Die.One
    | (Twos, _) ->
        (dice |> countBy Die.Two) * value Die.Two
    | (Threes, _) ->
        (dice |> countBy Die.Three) * value Die.Three
    | (Fours, _) ->
        (dice |> countBy Die.Four) * value Die.Four
    | (Fives, _) ->
        (dice |> countBy Die.Five) * value Die.Five
    | (Sixes, _) ->
        (dice |> countBy Die.Six) * value Die.Six
    | (FullHouse, [2;3]) ->
        total dice
    | (FourOfAKind, [1;4]) | (FourOfAKind, [5]) ->
        dice
        |> List.countBy id
        |> List.find (fun (_, count) -> count >= 4)
        |> fun (die, _) -> value die * 4
    | (LittleStraight, [1;1;1;1;1]) when dice |> countBy Die.Six = 0 ->
        straightPoints
    | (BigStraight, [1;1;1;1;1]) when dice |> countBy Die.One = 0 ->
        straightPoints
    | (Choice, _) ->
        total dice
    | (Yacht, [5]) ->
        yachtPoints
    | _ -> 0