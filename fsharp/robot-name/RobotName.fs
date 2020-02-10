module RobotName
open System

type Robot = { name: string }

let alphabet = ['A' .. 'Z']

let random n = Random().Next(n)

let getRandomLetter() =
    alphabet.[random 26]

let getRandomName() =
    let letters = [getRandomLetter(); getRandomLetter()] |> List.map string
    String.concat "" <| letters

let getRandomNumber() =
    let number = random 1000
    number.ToString("000")

let mkRobot() =
    { name = getRandomName() + getRandomNumber() }

let name robot = robot.name

let reset robot = mkRobot()