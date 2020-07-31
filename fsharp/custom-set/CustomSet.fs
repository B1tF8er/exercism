module CustomSet

type Set<'T> = { Items: 'T list }

let empty =
    { Items = [] }

let singleton value =
    { Items = [value] }

let isEmpty set =
    set.Items.IsEmpty

let size set =
    set.Items.Length 

let fromList list =
    { 
        Items = list
            |> List.sort
            |> List.distinct 
    } 

let toList set =
    set.Items

let contains value set =
    List.contains value set.Items

let insert value set =
    value::set.Items
    |> fromList

let union left right =
    left.Items @ right.Items
    |> fromList

let intersection left right =
    left.Items
    |> List.filter (fun x -> List.contains x right.Items)
    |> fromList

let difference left right =
    left.Items
    |> List.filter (fun x -> List.contains x right.Items |> not)
    |> fromList

let isEqualTo left right =
    (size left = size right) && (isEmpty (difference left right))

let isSubsetOf left right =
    left.Items
    |> List.forall (fun x -> List.contains x right.Items)

let isDisjointFrom left right =
    left.Items
    |> List.exists (fun x -> List.contains x right.Items)
    |> not