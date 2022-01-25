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
        "ACCESS LEVEL: " + string this.MetaData.AccessLevel + "\n" +
        "DEPARTMENT: " + string this.MetaData.Department + "\n" +
        "PRIORITY: " + string this.MetaData.Priority + "\n" +
        "FROM: " + stringify this.Content.Header.From + "\n" +
        "TO:   " + stringify this.Content.Header.To + "\n" +
        "BODY: " + string this.Content.Body

//singleton for all documents created at runtime
type Documents private () =
    let mutable docMap : Map<string,Document> = Map.empty

    static let instance = Documents()
    static member Instance = instance

    member this.Remove(document : Document) =
        docMap <- docMap.Remove(document.Name)

    member this.Add(newDocument : Document) =
        docMap <- docMap.Add(newDocument.Name, newDocument)

    member this.GetAll() =
        docMap

    member this.Get(key : string) : Option<Document> =
        try
            Some(docMap.Item(key))
        with
            | :? System.Collections.Generic.KeyNotFoundException -> None

    member private this.SelectDocumentsByUser() =
        docMap.Values
            |> Seq.filter(fun (doc : Document) ->
                doc.MetaData.Department.Equals(CurrentUser.Instance.GetDepartment())
                && doc.MetaData.AccessLevel <= CurrentUser.Instance.GetAccessLevel())
            |> Seq.sortByDescending(fun (doc : Document) -> doc.MetaData.Priority)

    member this.DisplayAll() =
        let documents = this.SelectDocumentsByUser()
        for document in documents do
            printfn """%s""" (document.PrintContent())
            printf "\n"

    member this.DisplayAllNames() =
        let documents = this.SelectDocumentsByUser()
        for document in documents do
            printfn """%s""" (document.Name)

    member this.Init() =
        this.Add({ Name= "Test1"; MetaData = { AccessLevel = users.[0].AccessLevel; Department = users.[0].Department; Priority = Normal}; Content = {Header = {From = users.[0].Name; To = {FirstName = "Lena"; LastName = "Lustig"}; Date = DateTime.Now}; Body = "tax inspection"}})
        this.Add({ Name= "Test2"; MetaData = { AccessLevel = users.[1].AccessLevel; Department = users.[1].Department; Priority = High}; Content = {Header = {From = users.[1].Name; To = {FirstName = "Hans"; LastName = "Hammer"}; Date = DateTime.Now}; Body = "construction site supervision"}})
        this.Add({ Name= "Test3"; MetaData = { AccessLevel = users.[2].AccessLevel; Department = users.[2].Department; Priority = Normal}; Content = {Header = {From = users.[2].Name; To = {FirstName = "Herbert"; LastName = "Höher"}; Date = DateTime.Now}; Body = "tax inspection"}})
        this.Add({ Name= "Test4"; MetaData = { AccessLevel = users.[3].AccessLevel; Department = users.[3].Department; Priority = Low}; Content = {Header = {From = users.[3].Name; To = {FirstName = "Benjamin"; LastName = "Berger"}; Date = DateTime.Now}; Body = "construction site supervision"}})
        this.Add({ Name= "Test5"; MetaData = { AccessLevel = users.[2].AccessLevel; Department = users.[2].Department; Priority = High}; Content = {Header = {From = users.[2].Name; To = {FirstName = "Sebastian"; LastName = "Seher"}; Date = DateTime.Now}; Body = "tax inspection"}})
        this.Add({ Name= "Test6"; MetaData = { AccessLevel = users.[1].AccessLevel; Department = users.[1].Department; Priority = Low}; Content = {Header = {From = users.[1].Name; To = {FirstName = "Otto"; LastName = "Ohrig"}; Date = DateTime.Now}; Body = "construction site supervision"}})
        this.Add({ Name= "Test7"; MetaData = { AccessLevel = users.[1].AccessLevel; Department = users.[1].Department; Priority = High}; Content = {Header = {From = users.[1].Name; To = {FirstName = "Tom"; LastName = "Temmer"}; Date = DateTime.Now}; Body = "tax inspection"}})
        this.Add({ Name= "Test8"; MetaData = { AccessLevel = users.[3].AccessLevel; Department = users.[3].Department; Priority = Low}; Content = {Header = {From = users.[3].Name; To = {FirstName = "Albert"; LastName = "Albern"}; Date = DateTime.Now}; Body = "construction site supervision"}})
        this.Add({ Name= "Test9"; MetaData = { AccessLevel = users.[3].AccessLevel; Department = users.[3].Department; Priority = Normal}; Content = {Header = {From = users.[3].Name; To = {FirstName = "Gisela"; LastName = "Günstig"}; Date = DateTime.Now}; Body = "tax inspection"}})
        this.Add({ Name= "Test10"; MetaData = { AccessLevel = users.[4].AccessLevel; Department = users.[4].Department; Priority = Normal}; Content = {Header = {From = users.[4].Name; To = {FirstName = "Gisela"; LastName = "Günstig"}; Date = DateTime.Now}; Body = "tax inspection"}})
