/// Copyright (C) 2016 The Authors.
module Texture

open Material

type Texture

/// <summary>
/// Creates a Texture from a function, which describes the mapping in [0,1] x [0,1] to a texture.
/// </summary>
/// <params name=fn>The function, which describes the texture mapping</params>
/// <returns>A texture, which wraps the function.</returns>
val make : fn:(float -> float -> Material) -> Texture

/// <summary>
/// Gets the material from a texture, using u, v floating coordinates and the function wrapped within the texture.
/// </summary>
/// <params name=u>The u coordinate to retrieve the material for.</params>
/// <params name=v>The v coordinate to retrieve the material for.</params>
/// <params name=t>The texture to retrieve the material from.</params>
/// <returns>The material for the specified coordinates in the texture, defined by the function.</returns>
val getMaterial : u:float -> v:float -> t:Texture -> Material