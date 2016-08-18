namespace Suave1

open System
open Suave
open Suave.Filters
open Suave.Successful
open Suave.Operators

type Person =
    { name: string
      age: int }

type Envelope<'a> =
    { payload: 'a
      server: string
      appId: string }

module Envelop =
    let private id = Guid.NewGuid().ToString("N")

    let create payload =
        { payload = payload
          server = Environment.MachineName
          appId = id }

module Server =
    open Suave.Writers

    let app =
        choose [
            GET >=> choose
                [ path "/person/kot" >=> 
                    OK (NetJSON.NetJSON.Serialize <| Envelop.create { name = "kot"; age = 41 }) 
                    >=> setMimeType "application/json; charset=utf-8" 
                ]
        ]

    startWebServer 
        { defaultConfig with bindings = [ HttpBinding.mkSimple Protocol.HTTP "0.0.0.0" 8083 ] }
        app