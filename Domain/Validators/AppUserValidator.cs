// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     AppUserValidator.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Domain
// =======================================================

namespace Domain.Validators;

/// <summary>
///   Validator for the Author entity.
/// </summary>
public class AppUserValidator : AbstractValidator<Author>
{

	/// <summary>
	///   Initializes a new instance of the <see cref="AppUserValidator" /> class.
	/// </summary>
	public AppUserValidator()
	{
		RuleFor(x => x.Id)
				.NotEmpty().WithMessage("User ID is required");

		RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Username is required")
				.Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

		RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("Invalid email address format");

		RuleFor(x => x.Roles)
				.NotNull().WithMessage("Roles collection cannot be null");
	}

}