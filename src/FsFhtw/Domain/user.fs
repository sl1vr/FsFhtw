module User

open Department

type Name = {
    FirstName : string 
    LastName : string
}

type AccessLevel =
    | Read
    | Write
    | Admin

type User = {
    Name : Name
    AccessLevel : AccessLevel
    Department : Department
}

//singleton for current user
type CurrentUser private () =
    let mutable name : Name = { FirstName = "Hans"; LastName = "Dampf" }
    let mutable accessLevel = AccessLevel.Admin
    let mutable department = Department.IT
    let mutable user = { Name = name; AccessLevel = accessLevel; Department = department }

    static let instance = CurrentUser()
    static member Instance = instance

    member this.SetNewUser(newUser : User) =
        user <- { Name = newUser.Name ; AccessLevel = newUser.AccessLevel; Department = newUser.Department }

    member this.GetUsername() = 
        user.Name

    //member this.SetUsername(newUsername : Name) = 
    //    name <- newUsername
        //user <- { Name = newUsername; AccessLevel = accessLevel; Department = department }

    member this.GetAccessLevel() = 
        user.AccessLevel

    //member this.SetAccessLevel(newAccessLevel : AccessLevel) = 
    //    accessLevel <- newAccessLevel

    member this.GetDepartment() = 
        user.Department

    //member this.SetDepartment(newDepartment : Department) = 
    //    department <- newDepartment
