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
