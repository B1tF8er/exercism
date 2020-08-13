module SpiralMatrix

type Dimension = D1 | D2

let operations size =
    [(size - 1) .. -1 .. 1]
    |> List.mapi (fun index i ->
        i |> List.replicate (if index = 0 then 3 else 2)
    )
    |> List.collect id
    |> List.mapi (fun index i ->
        (
            i,
            (if index % 2 = 0 then D2 else D1),
            (if (index / 2 |> float |> floor |> int) % 2 = 0 then (+) else (-))
        )
    )
    |> List.collect (fun (rep, dim, op) ->
        (dim, op) |> List.replicate rep
    )

let spiral size =
    operations size
    |> List.scan (fun (index1, index2) (dim, op) ->
        match dim with
        | D1 -> (op index1 1, index2)
        | D2 -> (index1, op index2 1)
    ) (0, 0)
    |> List.indexed

let convert2DarrayToListOfLists array2D = 
    [
        for i1 in [0..(Array2D.length1 array2D) - 1] do
        [
            for i2 in [0..(Array2D.length2 array2D) - 1] do 
            yield array2D.[i1, i2]
        ] 
    ]

let spiralMatrix size =
    match size with
    | 0 -> List.empty
    | _ ->
        let arr = Array2D.zeroCreate<int> size size
        spiral size
        |> List.iter (fun (i, (index1, index2)) ->
            arr.[index1, index2] <- i + 1
        )
        arr
        |> convert2DarrayToListOfLists