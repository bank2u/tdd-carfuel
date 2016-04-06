open System
open canopy
open runner
open configuration
open reporters

let baseUrl = "http://localhost:1854" 
chromeDir <- "C:\\chromedriver" 
start firefox 

"Log in" &&& fun _ ->
    url (baseUrl + "/Account/Login")
    "#Email" << "suthep@gfbd.co.th"
    "#Password" << "Test999/*"
    click "input[type=submit]"
    on baseUrl

"Click add link then go to create page" &&& fun _ ->
    url (baseUrl + "/cars")
    displayed "a#gotoAdd"
    click "a#gotoAdd"
    on (baseUrl + "/cars/create")

"Add new car" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url (baseUrl + "/cars/create")
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"

    on (baseUrl + "/cars")
    "td" *= make

run() 
//printfn "press [enter] to exit"
//System.Console.ReadLine() |> ignore 
quit()