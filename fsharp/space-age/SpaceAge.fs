module SpaceAge

open System

type Planet =
    | Mercury
    | Venus
    | Earth
    | Mars
    | Jupiter
    | Saturn
    | Uranus
    | Neptune

let earthOrbitalPeriod  = 31557600.0

let orbitalPeriods = 
    [
        Mercury, 0.2408467
        Venus,   0.61519726
        Earth,   1.0
        Mars,    1.8808158
        Jupiter, 11.862615
        Saturn,  29.447498
        Neptune, 164.79132
        Uranus,  84.016846
    ] 
    |> Map.ofList

let yearsUsingPeriod (seconds) (orbitalPeriod) =
    let planetOrbitalPeriod = (seconds / orbitalPeriod)
    Math.Round(planetOrbitalPeriod / earthOrbitalPeriod , 2)

let age (planet: Planet) (seconds: int64): float =
    orbitalPeriods.[planet] |> yearsUsingPeriod (float seconds)