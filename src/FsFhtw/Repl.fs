module Repl

open System
open Document
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
open DocumentLogic

let evaluateCommands (command : Command) =
    match command with
    | Command.CreateDocument ->
        Documents.Instance.Add(createDocument())
        (command, sprintf "CreateDocument finished successfully.")
    | Command.ReadDocument ->
        printf "%s \n" (readDocument())
        (command, sprintf "ReadDocument finished successfully.")
    | Command.UpdateDocument ->
        Documents.Instance.Add(updateDocument())
        (command, sprintf "UpdateDocument finished successfully.")
    | Command.DisplayAllDocuments ->
        Documents.Instance.DisplayAll()
        (command, sprintf "DisplayAllDocuments finished successfully.")
    | Command.SwitchUser ->
        UserLogic.switchUser()
        (command, sprintf "SwitchUsers finished successfully.")

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
