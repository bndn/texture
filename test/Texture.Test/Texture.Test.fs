/// Copyright (C) 2016 The Authors.
module Texture.Test

open Color
open Material
open Xunit
open FsUnit.Xunit

// Material construction
let c1 = Color.make 0.56 0.44 0.1
let m1 = Material.make c1 0.4
let c2 = Color.make 0.33 0.21 0.65
let m2 = Material.make c2 0.6
let c3 = Color.make 0.89 0.2 0.76
let m3 = Material.make c3 0.945
let c4 = Color.make 0.378 0.067 0.69
let m4 = Material.make c4 0.289

// Texture construction
let t = Texture.make (fun x y ->
                        match (x, y) with
                        | (x, y) when x < 0.5 && y < 0.5 -> m1
                        | (x, y) when x < 0.5 -> m2
                        | (x, y) when y < 0.5 -> m3
                        | (x, y) -> m4)

[<Fact>]
let ``make constructs a texture from a function``() =

    // Check that we do have our texture
    t |> should be instanceOfType<Texture>

[<Fact>]
let ``getMaterial gets a material from a texture``() =

    // Get the materials
    let mat1 = Texture.getMaterial 0.453 0.2 t
    let mat2 = Texture.getMaterial 0.156 0.768 t
    let mat3 = Texture.getMaterial 0.55 0.2 t
    let mat4 = Texture.getMaterial 0.55 0.86 t

    // Check that the materials are material types.
    mat1 |> should be instanceOfType<Material>
    mat2 |> should be instanceOfType<Material>
    mat3 |> should be instanceOfType<Material>
    mat4 |> should be instanceOfType<Material>

    // Get reflection values from materials (to prove we can extract data back out)
    abs (Material.getReflect mat1 - 0.4) |> should be (lessThan 0.01)
    abs (Material.getReflect mat2 - 0.6) |> should be (lessThan 0.01)
    abs (Material.getReflect mat3 - 0.945) |> should be (lessThan 0.01)
    abs (Material.getReflect mat4 - 0.289) |> should be (lessThan 0.01)


[<Fact>]
let ``getMaterial fails if x is less than 0.0``() =

    (fun f -> Texture.getMaterial -0.4 0.32 t |> ignore) |> shouldFail

[<Fact>]
let ``getMaterial fails if x is more than 1.0``() =

    (fun f -> Texture.getMaterial 2.6 0.32 t |> ignore) |> shouldFail

[<Fact>]
let ``getMaterial fails if y is less than 0.0``() =

    (fun f -> Texture.getMaterial 0.65 -4.32 t |> ignore) |> shouldFail

[<Fact>]
let ``getMaterial fails if y is more than 1.0``() =

    (fun f -> Texture.getMaterial 0.65 1.89 t |> ignore) |> shouldFail
