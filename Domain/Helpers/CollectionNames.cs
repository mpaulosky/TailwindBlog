﻿// ============================================
// Copyright (c) 2023. All rights reserved.
// File Name :     CollectionNames.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : TailwindBlog
// Project Name :  Domain
// =============================================

namespace Domain.Helpers;

/// <summary>
///   CollectionNames class
/// </summary>
public static class CollectionNames
{

	/// <summary>
	///   GetCollectionName method
	/// </summary>
	/// <param name="entityName">string</param>
	/// <returns>Result string collection name</returns>
	public static Result<string> GetCollectionName(string? entityName)
	{

		switch (entityName)
		{

			case "Article": return Result.Ok("articles");

			case "Category": return Result.Ok("categories");

			default: return Result<string>.Fail("Invalid entity name provided.");

		}

	}

}