module Accumulate

let accumulate (func: 'a -> 'b) (input: 'a list): 'b list =
    [ for i in input do yield func i ]