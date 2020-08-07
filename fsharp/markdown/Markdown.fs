module Markdown

open System.Text.RegularExpressions

let regexReplaceStr pattern (replaceStr: string) (s: string) =
    Regex.Replace (s, pattern, replaceStr, RegexOptions.Multiline)

let regexReplaceFun pattern (replaceFun: Match -> string) (s: string) =
    Regex.Replace (s, pattern, replaceFun, RegexOptions.Multiline)

let markupUnorderedList =
    regexReplaceFun @"(((^|\n)\* (.+))+)" (
        fun (m: Match) ->
            m.Groups.[4].Captures
            |> Seq.cast<Capture>
            |> Seq.map (fun c -> sprintf "<li>%s</li>" c.Value)
            |> String.concat ""
            |> sprintf "\n<ul>%s</ul>"
    )
    >> regexReplaceStr "^\n" ""

let markupHeader =
    regexReplaceFun @"^(#{1,6}) (.+)$" (
        fun (m: Match) ->
            let level = m.Groups.[1].Value.Length
            sprintf "<h%i>%s</h%i>" level (m.Groups.[2].Value) level
    )
    >> regexReplaceStr "\n" ""

let markupParagraph =
    regexReplaceStr @"^([^*#\<].+)$" "<p>$1</p>"

let markupBold =
    regexReplaceStr @"__(.+?)__" "<strong>$1</strong>"

let markupItalic =
    regexReplaceStr @"_(.+?)_" "<em>$1</em>"

let rec parse (markdown: string) =
    markdown
    |> markupUnorderedList
    |> markupParagraph
    |> markupHeader
    |> markupBold
    |> markupItalic