module Document

open User
open Department
open Priority
open System

type Template =
    | TaxInspection
    | ConstructionSiteSupervision

type State =
    | Opened
    | Completed
    | Deleted

type Header = {
    From : Name
    To : Name
    Date : DateTime
}

type Content = {
    Header : Header
    Body : string
}

type MetaData = {
    AccessLevel : AccessLevel
    Department : Department
    Priority : Priority
}

type Document = {
    Name : string
    MetaData : MetaData
    Content : Content
}

type Document with
    member this.PrintContent() =
        "DOCUMENT: " + this.Name + "\n" +
        "FROM: " + string this.Content.Header.From + "\n" +
        "TO:   " + string this.Content.Header.To + "\n" +
        "BODY: " + string this.Content.Body

//singleton for all documents created at runtime
type Documents private () =
    let mutable docMap : Map<string,Document> = Map.empty

    static let instance = Documents()
    static member Instance = instance

    member this.Add(newDocument : Document) =
        docMap <- docMap.Add(newDocument.Name, newDocument)

    member this.GetAll() =
        docMap

    member this.Get(key : string) : Option<Document> =
        try
            Some(docMap.Item(key))
        with
            | :? System.Collections.Generic.KeyNotFoundException -> None

    member this.DisplayAll() =
        for document in docMap do
            printfn """%s""" (document.Value.PrintContent())

