// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     ICustomRule.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Architecture.Tests
// =======================================================

namespace Architecture.Tests;

/// <summary>
/// Interface for custom architecture rules.
/// </summary>
public interface ICustomRule
{

	/// <summary>
	/// Determines whether the given type meets the custom rule.
	/// </summary>
	/// <param name="type">The type to check.</param>
	/// <returns>True if the type meets the rule, false otherwise.</returns>
	bool MeetsRule(Type type);

}