module DocumentLogic

open System

open User
open Document
open Priority

let rec getTemplate() : string =
    let taxInspection = "tax inspection"
    let constructionSiteSupervision =  "construction site supervision"

    printfn """%s or %s?""" taxInspection constructionSiteSupervision

    let input = Console.ReadLine()

    let body =
        if input = taxInspection then taxInspection
        elif input = constructionSiteSupervision then constructionSiteSupervision
        else
            printfn """Invalid template body name! Use %s or %s""" taxInspection constructionSiteSupervision
            getTemplate();

    body

let rec getRecipient() : Name =
    printfn "Recipient?"
    printfn "First name:"
    let input2_fn = Console.ReadLine()
    printfn "Last name:"
    let input2_ln = Console.ReadLine()

    let name =
        if input2_fn.Equals(String.Empty) || input2_ln.Equals(String.Empty) then
            printfn """Invalid first or last name!"""
            getRecipient();
        else
            {FirstName = input2_fn; LastName = input2_ln}
            
    name

let rec getPriority() : Priority =
    printfn "Priority?"
    let input3 = Console.ReadLine()
    let priority =
        if input3 = nameof(High) then High
        elif input3 = nameof(Normal) then Normal
        elif input3 = nameof(Low) then Low
        else
            printfn """Invalid priority! Use %s %s %s""" (nameof(High)) (nameof(Normal)) (nameof(Low))
            getPriority();

    priority

let createDocument() : Document =
    let from = CurrentUser.Instance.GetUsername()
    let ``to`` = getRecipient()
    let header = {From = from; To = ``to``; Date = DateTime.Today}

    let body = getTemplate()
    let content = {Header = header; Body = body}
    let priority = getPriority()

    let metaData = {AccessLevel = CurrentUser.Instance.GetAccessLevel(); Department = CurrentUser.Instance.GetDepartment(); Priority = priority}

    printf "File name?\n"
    let name = Console.ReadLine()

    { Name = name; MetaData = metaData; Content = content}

let rec getDocumentByName() : Document =
    Documents.Instance.DisplayAllNames()
    printfn "Choose a document."
    let key = Console.ReadLine()
    let value = Documents.Instance.Get(key)
    match value with
        | Some document -> document
        | None -> printfn "Document not found!\n"; getDocumentByName()

let updateDocument() : Document =
    let document = getDocumentByName()
    Documents.Instance.Remove(document)
    createDocument()

let readDocument() : string =
    let document = getDocumentByName()
    document.PrintContent()
