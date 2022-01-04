module DocumentLogic

open Domain.Document
open Domain.User
open Domain.Priority


let CreateDocument () : Domain.Document =
    let from = CurrentUser.Instance.GetUsername
    let ``to`` = {FirstName = "Herbert"; LastName = "Emmentaler"}
    let header = {From = from, To = ``to``, Date = DateTime.Today}

    let taxInspection = nameof(TaxInspection)
    let constructionSiteSupervision = nameof(ConstructionSiteSupervision)

    sprintf """%s or %s""" taxInspection constructionSiteSupervision
    let input = Console.ReadLine();
    let body = 
        if input = taxInspection then taxInspection
        elif input = constructionSiteSupervision then constructionSiteSupervision
        else failWith "Invalid Template"

    let content = {Header = header; Body = body}

    sprintf "Priority?"
    input = Console.ReadLine()
    let priority = 
        if input = nameof(High) then High
        elif input = nameof(Normal) then Normal
        elif input = nameof(Low) then low
        else failWith "Invalid Priority"

    let metaData = {AccessLevel = CurrentUser.Instance.GetAccessLevel;Department = CurrentUser.Instance.GetDepartment;Priority = priority}

    let name = Console.ReadLine()

    { Name = name; MetaData = metaData; Content = content}


