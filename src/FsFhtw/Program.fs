open Repl
open Commands
open User

[<EntryPoint>]
let main argv =
    printfn "Welcome to the FPR Document Management System!"
    printfn "Please enter your commands to interact with the system."
    printfn "Press CTRL+C to stop the program."
    printf "> "

    //CurrentUser.Instance.SetUsername({ FirstName = "Hansiiiiiiiiii"; LastName = "Dampf" })
    //CurrentUser.Instance.SetNewUser({ Name = { FirstName = "Sepp"; LastName = "Dampf" }; AccessLevel = AccessLevel.Read; Department = Department.Accounting })

    //let username = CurrentUser.Instance.GetUsername()
    //let accessLevel = CurrentUser.Instance.GetAccessLevel()
    //let department = CurrentUser.Instance.GetDepartment()
    
    //printf "%s %s %s %s" username.FirstName username.LastName (string accessLevel) (string department)

    Repl.loop Command.DisplayAllDocuments
