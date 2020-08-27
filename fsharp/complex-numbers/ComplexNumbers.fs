module ComplexNumbers

open System

type ComplexNumber =
    { 
        real: float
        imaginary: float
    }

let private sumPows complexNumber =
    Math.Pow(complexNumber.real, 2.0) + Math.Pow(complexNumber.imaginary, 2.0)

let create real imaginary =
    {
        real = real
        imaginary = imaginary
    }

let mul z1 z2 =
    {
        real = z1.real * z2.real - z1.imaginary * z2.imaginary
        imaginary = z1.imaginary * z2.real + z1.real * z2.imaginary
    }

let add z1 z2 =
    {
        real = z1.real + z2.real
        imaginary = z1.imaginary + z2.imaginary
    }

let sub z1 z2 =
    {
        real = z1.real - z2.real
        imaginary = z1.imaginary - z2.imaginary
    }

let div z1 z2 = 
    {
        real = (z1.real * z2.real + z1.imaginary * z2.imaginary) / (z2 |> sumPows)
        imaginary = (z1.imaginary * z2.real - z1.real * z2.imaginary) / (z2 |> sumPows)
    }

let abs z =
    Math.Sqrt(z |> sumPows)

let conjugate z =
    { z with imaginary = -z.imaginary }

let real z =
    z.real

let imaginary z =
    z.imaginary

let exp z =
    {
        real = Math.Cos(z.imaginary) * Math.Exp(z.real)
        imaginary = Math.Sin(z.imaginary) * Math.Exp(z.real)
    }