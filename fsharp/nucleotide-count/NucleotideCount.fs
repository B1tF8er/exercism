module NucleotideCount

let private validNucleotides =
    ['A'; 'T'; 'C'; 'G']

let private isValid nucleotide =
    validNucleotides
    |> List.contains nucleotide

let private increment sum nextNucleotide nucleotide =
    if nextNucleotide = nucleotide then sum + 1 else sum

let count (nucleotide:char) (strand:string) = 
    match isValid nucleotide with
    | true  -> (0, strand) ||> Seq.fold (fun sum nextNucleotide -> increment sum nextNucleotide nucleotide) 
    | false -> failwith "Invalid nucleotide"

let nucleotideCounts strand = 
    if String.forall isValid strand then
        List.map (fun nucleotide -> (nucleotide, count nucleotide strand)) validNucleotides 
        |> Map.ofSeq
        |> Some
    else    
        None
