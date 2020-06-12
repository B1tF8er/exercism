module Triangle

let private isValid triangle = 
    let nonZero = triangle |> List.sum <> 0.0

    let equality =
        let [x; y; z] = triangle
        x + y >= z && x + z >= y && y + z >= x
    
    equality && nonZero

let private distinctSides triangle =
    triangle
    |> List.distinct
    |> List.length

let equilateral triangle =
    triangle |> isValid && triangle |> distinctSides = 1

let isosceles triangle =
    triangle |> isValid && triangle |> distinctSides <= 2

let scalene triangle =
    triangle |> isValid && triangle |> distinctSides = 3