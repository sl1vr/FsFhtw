module User

open Department

type Name =
    | FirstName of string
    | LastName of string

type AccessLevel =
    | Read
    | Write
    | Admin

type User =
    | Name of Name
    | AccessLevel of AccessLevel
    | Department of Department
