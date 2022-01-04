module Parser

open System

let safeEquals (it : string) (theOther : string) =
    String.Equals(it, theOther, StringComparison.OrdinalIgnoreCase)

[<Literal>]
let HelpLabel = "Help"

let (|CreateDocument|ReadDocument|UpdateDocument|DisplayAllDocuments|SwitchUser|Help|ParseFailed|) (input : string) =
    let tryParseInt (arg : string) valueConstructor =
        let (worked, arg') = Int32.TryParse arg
        if worked then valueConstructor arg' else ParseFailed

    let parts = input.Split(' ') |> List.ofArray
    match parts with
    | [ verb ] when safeEquals verb (nameof Commands.CreateDocument) -> CreateDocument
    | [ verb ] when safeEquals verb (nameof Commands.ReadDocument) -> ReadDocument
    | [ verb ] when safeEquals verb (nameof Commands.UpdateDocument) -> UpdateDocument
    | [ verb ] when safeEquals verb (nameof Commands.DisplayAllDocuments) -> DisplayAllDocuments
    | [ verb ] when safeEquals verb (nameof Commands.SwitchUser) -> SwitchUser
    | [ verb ] when safeEquals verb HelpLabel -> Help
    | _ -> ParseFailed
