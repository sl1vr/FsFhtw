module Document

open User
open Department
open Priority
open System

type State =
    | Opened
    | Completed
    | Deleted

type Header =
    | From of Name
    | To of Name
    | Date of DateTime

type Content =
    | Header of Header
    | Body of string

type MetaData =
    | AccessLevel of AccessLevel
    | Department of Department
    | Priority of Priority

type Document =
    | Name of string
    | Content of Content
