module PalindromeProducts

let isPalindrome value =
    let valueAsString = value |> string
    valueAsString = (valueAsString |> Seq.rev |> Array.ofSeq |> System.String)

let palindrome predicate compare startValue minFactor maxFactor = 
    match minFactor > maxFactor with
    | true -> invalidArg "minFactor" "min must be <= max"
    | false ->
        let allPalindromes = 
            let mutable compareValue = startValue
            [for y in minFactor..maxFactor do
                 for x in minFactor ..y do
                     let prod = x * y
                     if (compare prod compareValue) && isPalindrome prod then
                        compareValue <- prod
                        yield prod, (x, y)]

        match List.isEmpty allPalindromes with
        | true -> (None,[])
        | false ->
            let value = 
                allPalindromes 
                |> List.map fst 
                |> predicate
            
            let factors = 
                allPalindromes 
                |> List.filter (fun x -> fst x = value) 
                |> List.map snd 
                |> List.sort

            (Some value, factors)

let largest minFactor maxFactor =
    palindrome List.max (>=) 0 minFactor maxFactor

let smallest minFactor maxFactor =
    palindrome List.min (<=) System.Int32.MaxValue minFactor maxFactor