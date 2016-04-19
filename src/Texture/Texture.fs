/// Copyright (C) 2016 The Authors.
module Texture

open Material

[<NoComparison>]
[<NoEquality>]
type Texture =
    | T of (float -> float -> Material)

/// <summary>
/// Creates a Texture from a function, which describes the mapping in [0,1] x [0,1] to a texture.
/// </summary>
/// <params name=fn>The function, which describes the texture mapping</params>
/// <returns>A texture, which wraps the function.</returns>
let make fn = T(fn)

/// <summary>
/// Gets the material from a texture, using u, v floating coordinates and the function wrapped within the texture.
/// </summary>
/// <params name=u>The u coordinate to retrieve the material for.</params>
/// <params name=v>The v coordinate to retrieve the material for.</params>
/// <params name=t>The texture to retrieve the material from.</params>
/// <returns>The material for the specified coordinates in the texture, defined by the function.</returns>
let getMaterial (u:float) (v:float) (t:Texture) =
    if u < 0.0 || u > 1.0
    then invalidArg "u" "Is either less than 0.0 or more than 1.0"

    if v < 0.0 || v > 1.0
    then invalidArg "v" "Is either less than 0.0 or more than 1.0"

    match t with T(fn) -> fn u v
