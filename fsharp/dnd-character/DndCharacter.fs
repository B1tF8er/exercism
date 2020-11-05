module DndCharacter

open System

type character = 
    { 
        Strength: int
        Dexterity: int
        Constitution: int
        Intelligence: int
        Wisdom: int
        Charisma: int
        Hitpoints: int
    }

let modifier x =
    floor (float(x - 10) / 2.) |> int

let ability() = 
    [1 .. 4]
    |> List.map (fun _ -> Random().Next(1, 7))
    |> List.sortDescending
    |> Seq.take 3
    |> Seq.sum

let createCharacter() : character =
    let constitution = ability()

    {
        Strength = ability()
        Dexterity = ability()
        Constitution = constitution
        Intelligence = ability()
        Wisdom = ability()
        Charisma = ability()
        Hitpoints = 10 + (modifier constitution)
    }