module PascalsTriangle

let rows numberOfRows : int list list =
    match numberOfRows with 
    | rows when rows < 0 -> []
    | _ ->
        let row column = 
            [ 1 .. column - 1 ] 
            |> List.scan (fun acc j -> acc * (column - j) / j) 1 

        [ 1 .. numberOfRows ] 
        |> List.map row