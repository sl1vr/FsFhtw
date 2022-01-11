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

let rec getPriority() : Priority =
    printfn "Priority?"
    let input2 = Console.ReadLine()
    let priority =
        if input2 = nameof(High) then High
        elif input2 = nameof(Normal) then Normal
        elif input2 = nameof(Low) then Low
        else
            printfn """Invalid priority! Use %s %s %s""" (nameof(High)) (nameof(Normal)) (nameof(Low))
            getPriority();

    priority

let createDocument() : Document =
    let from = CurrentUser.Instance.GetUsername()
    let ``to`` = {FirstName = "Herbert"; LastName = "Emmentaler"}
    let header = {From = from; To = ``to``; Date = DateTime.Today}

    let body = getTemplate()
    let content = {Header = header; Body = body}
    let priority = getPriority()

    let metaData = {AccessLevel = CurrentUser.Instance.GetAccessLevel(); Department = CurrentUser.Instance.GetDepartment(); Priority = priority}

    printf "File name?"
    let name = Console.ReadLine()

    { Name = name; MetaData = metaData; Content = content}

let createDocumentWithName(name : string) : Document =
    let from = CurrentUser.Instance.GetUsername()
    let ``to`` = {FirstName = "Herbert"; LastName = "Emmentaler"}
    let header = {From = from; To = ``to``; Date = DateTime.Today}

    let body = getTemplate()
    let content = {Header = header; Body = body}
    let priority = getPriority()

    let metaData = {AccessLevel = CurrentUser.Instance.GetAccessLevel(); Department = CurrentUser.Instance.GetDepartment(); Priority = priority}

    { Name = name; MetaData = metaData; Content = content}

let rec getDocumentByName() : Document =
    printfn "Which document do you want to update?"
    let key = Console.ReadLine()
    let value = Documents.Instance.Get(key)
    match value with
        | Some document -> document
        | None -> getDocumentByName()

let updateDocument() : Document =
    let name = getDocumentByName().Name
    createDocumentWithName(name)

let readDocument() : string =
    let document = getDocumentByName()
    document.PrintContent()
