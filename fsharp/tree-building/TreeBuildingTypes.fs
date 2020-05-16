// This file was created manually and its version is 1.0.0.

module TreeBuildingTypes

type Record = { RecordId: int; ParentId: int }

type Tree =
    | Branch of int * Tree list
    | Leaf of int