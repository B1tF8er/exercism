module RobotSimulator

type Direction = North | East | South | West
type Position = int * int
type Robot = { Direction: Direction; Position: Position }

let create direction position =
    { Direction = direction; Position = position }

let private turnRight robot = 
    match robot.Direction with
    | North -> { robot with Direction = East  }
    | East  -> { robot with Direction = South }
    | South -> { robot with Direction = West  }
    | West  -> { robot with Direction = North }

let private turnLeft robot = 
    match robot.Direction with
    | North -> { robot with Direction = West  }
    | East  -> { robot with Direction = North }
    | South -> { robot with Direction = East  }
    | West  -> { robot with Direction = South }

let private advance robot = 
    let (x, y) = robot.Position

    match robot.Direction with
    | North -> { robot with Position = (x, y + 1) }
    | East  -> { robot with Position = (x + 1, y) }
    | South -> { robot with Position = (x, y - 1) }
    | West  -> { robot with Position = (x - 1, y) }
    
let private applyInstructionTo robot instruction =
    match instruction with
    | 'L' -> robot |> turnLeft 
    | 'R' -> robot |> turnRight
    | 'A' -> robot |> advance
    | _   -> failwith "Invalid instruction"

let move instructions robot =
    instructions
    |> Seq.fold applyInstructionTo robot