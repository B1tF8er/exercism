module Say

let quotRem (x: int64) (y: int64) =
    let div = x / y
    let rem = x % y
    (div, rem)

let bases n = 
    let values = 
        [| "one"
           "two";
           "three";
           "four";
           "five";
           "six";
           "seven";
           "eight";
           "nine";
           "ten";
           "eleven";
           "twelve";
           "thirteen";
           "fourteen";
           "fifteen";
           "sixteen";
           "seventeen";
           "eighteen";
           "nineteen" |]

    Array.tryItem (n - 1) values
                        
let tens n = 
    match n < 20 with
    | true ->
        bases n
    | false ->
        let values = 
            [| "twenty"
               "thirty"
               "forty"
               "fifty"
               "sixty"
               "seventy"
               "eighty"
               "ninety" |]
        
        let (count, remainder) =
            quotRem (int64 n) 10L

        let countStr =
            values
            |> Array.item ((int count) - 2)

        let basesStr =
            (bases (int remainder))
            |> Option.fold (fun _ item -> "-" + item) ""

        Some (countStr + basesStr)

let hundreds n = 
    match n < 100L with
    | true -> tens (int n)
    | false ->
        let (count, remainder) =
            quotRem (int64 n) 100L
        
        let tensStr =
            (tens (int remainder))
            |> Option.fold (fun _ item -> " " + item) ""

        (bases (int count))
        |> Option.bind (fun item -> Some (item + " hundred" + tensStr))

let chunk str n =
    (hundreds n)
    |> Option.bind (fun item -> Some (item + " " + str))

let thousands =
    chunk "thousand"

let millions =
    chunk "million"

let billions =
    chunk "billion"

let parts n = 
    let (billionsCount, billionsRemainder) =
        quotRem n 1000000000L
    
    let (millionsCount, millionsRemainder) =
        quotRem billionsRemainder 1000000L
    let (thousandsCount, remainder) =
        quotRem millionsRemainder 1000L
    
    (billionsCount, millionsCount, thousandsCount, remainder)
    
let say n = 
    match n with
    | _ when n < 0L || n>= 1000000000000L -> None
    | 0L -> Some "zero"
    | _ -> 
        let (billionsCount, millionsCount, thousandsCount, remainder) = parts n

        [ billions billionsCount; 
          millions millionsCount;
          thousands thousandsCount;
          hundreds remainder]
        |> List.filter Option.isSome
        |> List.map Option.get
        |> List.reduce (fun x y -> x + " " + y)
        |> Some