open Commands
open Document

[<EntryPoint>]
let main argv =
    printfn "Welcome to the FPR Document Management System!"
    printfn "Please enter your commands to interact with the system."
    printfn "Press CTRL+C to stop the program."
    printf "> "

    Documents.Instance.Init()
    Repl.loop Command.DisplayAllDocuments
