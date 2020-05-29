module ArmstrongNumbers

let isArmstrongNumber number =
    let charToNumber c = int c - int '0'
    let numberAsString = string number
    let charToPower c = pown (charToNumber c) numberAsString.Length
    Seq.sumBy charToPower numberAsString = number
 