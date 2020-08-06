module CircularBuffer

type CircularBuffer<'a> = 
    {
        items: 'a list;
        size: int 
    }

let mkCircularBuffer size =
    { 
        items = []
        size = size 
    }

let clear buffer =
    { buffer with items = [] }    

let write value buffer = 
    match List.length buffer.items = buffer.size with
    | true -> failwith "Cannot write to full buffer"
    | false ->{ buffer with items = buffer.items @ [value] }
        
let forceWrite value buffer =
    match List.length buffer.items = buffer.size with
    | true -> { buffer with items = List.tail buffer.items @ [value] }
    | false  -> { buffer with items = buffer.items @ [value] }

let read buffer =
    match buffer.items with    
    | x::xs -> x, { buffer with items = xs }
    | [] -> failwith "Cannot read from empty buffer" 