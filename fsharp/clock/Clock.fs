module Clock

type Clock = { hours: int; minutes: int }

let normalize x y = (int)(((x % y) + y) % y)

let create hours minutes =
    let totalMinutes = hours * 60 + minutes
    let normalizedHours = normalize ((double)totalMinutes / 60.0) 24.0
    let normalizedMinutes = normalize ((double)minutes) 60.0

    { hours = normalizedHours; minutes = normalizedMinutes }

let add minutes clock =
    create clock.hours (clock.minutes + minutes)

let subtract minutes clock =
    create clock.hours (clock.minutes - minutes)

let display clock =
    sprintf "%02i:%02i" clock.hours clock.minutes