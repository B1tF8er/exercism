module DotDsl

type Attribute = string * string

type Element =
    | Attr of Attribute
    | Node of string * Attribute list
    | Edge of string * string * Attribute list

type Graph = Element list

let private isAttr element =
    match element with
    | Attr _ -> Some element
    | _      -> None

let private isNode element =
    match element with
    | Node _ -> Some element
    | _      -> None

let private isEdge element =
    match element with
    | Edge _ -> Some element
    | _      -> None

let graph children =
    children
    |> List.sort

let attr key value =
    Attr (key, value)

let node key attrs =
    Node (key, attrs)

let edge left right attrs =
    Edge (left, right, attrs)

let attrs graph =
    graph
    |> List.choose isAttr

let nodes graph =
    graph
    |> List.choose isNode

let edges graph =
    graph
    |> List.choose isEdge