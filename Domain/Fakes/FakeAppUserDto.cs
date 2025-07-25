// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakeAppUserDto.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Domain
// =======================================================

namespace Domain.Fakes;

/// <summary>
///   Provides methods to generate fake data for the <see cref="AppUserDto" /> class.
/// </summary>
public static class FakeAppUserDto
{

	/// <summary>
	///   Generates a new fake <see cref="AppUserDto" /> object.
	/// </summary>
	/// <param name="useSeed">Determines whether a fixed seed should be used for consistent outputs.</param>
	/// <returns>A single fake <see cref="AppUserDto" /> object.</returns>
	public static AppUserDto GetNewAppUserDto(bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate();

	}

	/// <summary>
	///   Generates a list of fake <see cref="AppUserDto" /> objects.
	/// </summary>
	/// <param name="numberRequested">The number of <see cref="AppUserDto" /> objects to generate.</param>
	/// <param name="useSeed">Determines whether a fixed seed should be used for consistent outputs.</param>
	/// <returns>A list of fake <see cref="AppUserDto" /> objects.</returns>
	public static List<AppUserDto> GetAppUserDtos(int numberRequested, bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate(numberRequested);

	}

	/// <summary>
	///   Configures a Faker instance to generate fake <see cref="AppUserDto" /> objects.
	/// </summary>
	/// <param name="useSeed">Determines whether a fixed seed should be used for consistent outputs.</param>
	/// <returns>A configured <see cref="Faker{T}" /> instance for <see cref="AppUserDto" /> objects.</returns>
	internal static Faker<AppUserDto> GenerateFake(bool useSeed = false)
	{

		const int seed = 621;

		var fake = new Faker<AppUserDto>()
				.RuleFor(x => x.Id, Guid.CreateVersion7().ToString())
				.RuleFor(x => x.UserName, f => f.Name.FullName())
				.RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.UserName))
				.RuleFor(x => x.Roles, f => [f.Random.Enum<Roles>().ToString()]);

		return useSeed ? fake.UseSeed(seed) : fake;

	}

}