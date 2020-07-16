module RotationalCipher
open System

let private alphabet =
    ['a' .. 'z']

let private createKey shiftKey = 
    alphabet @ alphabet 
    |> List.skip shiftKey 
    |> List.take (List.length alphabet)

let private rotateLetter key c =
    let currentChar x =
        x = Char.ToLower(c)
    let index = 
        alphabet
        |> List.findIndex currentChar
    let rotatedLetter = 
        key
        |> List.item index 

    match Char.IsUpper(c) with
    | false -> rotatedLetter
    | true  -> Char.ToUpper(rotatedLetter)

let private rotateChar shiftKey =
    let key = createKey shiftKey
    let rotate = rotateLetter key

    fun (c: char) -> 
        match Char.IsLetter(c) with
        | false -> c
        | true  -> rotate c

let rotate shiftKey text =
    text
    |> String.map (rotateChar shiftKey)