module Repl

open System
open Parser

type Message =
    | Command of Commands.Command
    | HelpRequested
    | NotParsable of string

let read (input : string) =
    match input with
    | CreateDocument -> Commands.CreateDocument |> Command
    | ReadDocument -> Commands.ReadDocument |> Command
    | UpdateDocument -> Commands.UpdateDocument |> Command
    | DisplayAllDocuments -> Commands.DisplayAllDocuments |> Command
    | SwitchUser -> Commands.SwitchUser |> Command
    | Help -> Message.HelpRequested
    | ParseFailed -> NotParsable input
    
open Microsoft.FSharp.Reflection

let createHelpText () : string =
    FSharpType.GetUnionCases typeof<Commands.Command>
    |> Array.map (fun case -> case.Name)
    |> Array.fold (fun prev curr -> prev + " " + curr) ""
    |> (fun s -> s.Trim() |> sprintf "Known commands are: %s")

open Commands



open User
open Document
open Priority

let CreateDocument () : Document =
    let from = CurrentUser.Instance.GetUsername()
    let ``to`` = {FirstName = "Herbert"; LastName = "Emmentaler"}
    let header = {From = from; To = ``to``; Date = DateTime.Today}
    
    let taxInspection = nameof(TaxInspection)
    let constructionSiteSupervision = nameof(ConstructionSiteSupervision)
    
    printf """%s or %s""" taxInspection constructionSiteSupervision
    let input = Console.ReadLine()
    let body = 
        if input = taxInspection then taxInspection
        elif input = constructionSiteSupervision then constructionSiteSupervision
        else failwith "Invalid Template"
    
    let content = {Header = header; Body = body}
    
    printf "Priority?"
    let input2 = Console.ReadLine()
    let priority = 
        if input2 = nameof(High) then Priority.High
        elif input2 = nameof(Normal) then Priority.Normal
        elif input2 = nameof(Low) then Priority.Low
        else failwith "Invalid Priority"
    
    let metaData = {AccessLevel = CurrentUser.Instance.GetAccessLevel();Department = CurrentUser.Instance.GetDepartment();Priority = priority}

    printf "File name?"
    let name = Console.ReadLine()

    { Name = name; MetaData = metaData; Content = content}

    


let evaluateCommands (command : Command) =
    match command with
    | Command.CreateDocument -> 
        //(command, sprintf "CreateDocument")
        CreateDocument()
        (command, sprintf "Document created! Probably...")
    | Command.ReadDocument -> 
        (command, sprintf "ReadDocument")
    | Command.UpdateDocument -> 
        (command, sprintf "UpdateDocument")
    | Command.DisplayAllDocuments -> 
        (command, sprintf "DisplayAllDocuments")
    | Command.CreateUser -> 
        (command, sprintf "CreateUser")
    | Command.SwitchUser -> 
        (command, sprintf "SwitchUser")

let evaluate (command : Command) (msg : Message) =
    match msg with
    | Message.Command cmd ->
        evaluateCommands cmd
    | Message.HelpRequested ->
        let message = createHelpText ()
        (command, message)
    | Message.NotParsable originalInput ->
        (command, sprintf """"%s" was not parsable. %s""" originalInput "You can get information about known commands by typing \"Help\"")

let print (cmd : Command, outputToPrint : string) =
    printfn "%s\n" outputToPrint
    printf "> "
    cmd

let rec loop (cmd : Command) =
    Console.ReadLine()
    |> read
    |> evaluate cmd
    |> print
    |> loop



