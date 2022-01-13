module User

open Department

type Name = {
    FirstName : string 
    LastName : string
}

type AccessLevel =
    | Read = 1
    | Write = 2
    | Admin = 3

type User = {
    Name : Name
    AccessLevel : AccessLevel
    Department : Department
}

let users : User array = [| 
    {Name = {FirstName = "Lisa"; LastName = "Lustig"}; AccessLevel = AccessLevel.Read; Department = Management};
    {Name = {FirstName = "Walter"; LastName = "Wunderbar"}; AccessLevel = AccessLevel.Write; Department = Accounting}
    {Name = {FirstName = "Gerald"; LastName = "Gans"}; AccessLevel = AccessLevel.Read; Department = Sales}
    {Name = {FirstName = "Ad"; LastName = "Min"}; AccessLevel = AccessLevel.Admin; Department = IT}
    {Name = {FirstName = "Anton"; LastName = "Apfel"}; AccessLevel = AccessLevel.Read; Department = Accounting}
    |]

let stringify(name : Name) =
    name.FirstName + " " + name.LastName

//singleton for current user
type CurrentUser private () =
    let mutable name : Name = users.[3].Name
    let mutable accessLevel = AccessLevel.Admin
    let mutable department = Department.IT
    let mutable user = { Name = name; AccessLevel = accessLevel; Department = department }

    static let instance = CurrentUser()
    static member Instance = instance

    member private this.SetNewUser(newUser : User) =
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

    member this.DisplayUsers() =
        for user in users do
            printfn """%s %s, %s, %s""" user.Name.FirstName user.Name.LastName (string user.Department) (string user.AccessLevel)

    member private this.SetUserAndConfirm(value : bool, user : User) : bool =
        this.SetNewUser user
        value

    member this.SwitchUser(newUser : Name) : bool =
        let selectedUser = users |> Array.filter(fun (user : User) -> user.Name.FirstName.Equals(newUser.FirstName) && user.Name.LastName.Equals(newUser.LastName))
        if (selectedUser.Length = 1) then
            //this.SetUserAndConfirm(true, selectedUser.[0])
            this.SetNewUser selectedUser.[0]
            true
        else
            false
