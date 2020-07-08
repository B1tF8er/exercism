module ListOps

let rec foldl folder state list =
    match list with
    | [] -> state
    | x::xs -> foldl folder (folder state x) xs

let rec foldr folder state list =
    list
    |> List.rev
    |> foldl (fun acc item -> folder item acc) state

let length list =
    list
    |> foldl (fun acc _ -> acc + 1) 0

let reverse list = 
    list
    |> foldl (fun acc item -> item :: acc) []

let map f list =
    list
    |> foldr (fun item acc -> f item :: acc) []

let filter f list =
    list
    |> foldr (fun item acc -> if f item then item :: acc else acc) []

let append xs ys =
    xs
    |> foldr (fun item acc -> item :: acc) ys

let concat xs =
    xs
    |>foldr append []