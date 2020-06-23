module Sieve

let rec private sieve remainder primes =
    match remainder with
    | [] -> primes |> List.rev
    | head::tail -> sieve (tail |> List.filter (fun x -> x % head > 0)) (head :: primes)
    
let primes limit = 
    let possiblePrimes = [2 .. limit]    
    sieve possiblePrimes []