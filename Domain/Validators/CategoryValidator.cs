// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     CategoryValidator.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Domain
// =======================================================

namespace Domain.Validators;

/// <summary>
///   Validator for the <see cref="Category" /> entity.
/// </summary>
public class CategoryValidator : AbstractValidator<Category>
{

	/// <summary>
	///   Initializes a new instance of the <see cref="CategoryValidator" /> class.
	/// </summary>
	public CategoryValidator()
	{

		RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required")
				.MaximumLength(80);

	}

}