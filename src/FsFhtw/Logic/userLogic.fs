module UserLogic

open System

open User

let rec switchUser() =
    printfn "Available users:"
    CurrentUser.Instance.DisplayUsers()
    printfn "First name of new user?"
    let firstName = Console.ReadLine()
    printfn "Last name of new user?"
    let lastName = Console.ReadLine()

    if (CurrentUser.Instance.SwitchUser({ FirstName = firstName; LastName = lastName})) then
        printfn "User successfully changed to %s %s!" firstName  lastName
    else
        printfn "User %s %s unknown, please enter again!" firstName lastName
        switchUser()
