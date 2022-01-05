module DocumentLogic

open System

open User
open Document
open Priority

let createDocument () : Document =
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
        if input2 = nameof(High) then High
        elif input2 = nameof(Normal) then Normal
        elif input2 = nameof(Low) then Low
        else failwith "Invalid Priority"

    let metaData = {AccessLevel = CurrentUser.Instance.GetAccessLevel();Department = CurrentUser.Instance.GetDepartment();Priority = priority}

    printf "File name?"
    let name = Console.ReadLine()

    { Name = name; MetaData = metaData; Content = content}
