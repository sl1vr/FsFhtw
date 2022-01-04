module Folder

open Document
open Priority

type Folder =
    | Name of string
    | Priority of Priority
    | Documents of list<Document>
