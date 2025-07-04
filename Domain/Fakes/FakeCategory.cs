// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakeCategory.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Domain
// =======================================================

namespace Domain.Fakes;

/// <summary>
///   Provides fake data generation methods for the <see cref="Category" /> entity.
/// </summary>
public class FakeCategory
{

	/// <summary>
	///   Generates a new fake <see cref="Category" /> object.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A single fake <see cref="Category" /> object.</returns>
	public static Category GetNewCategory(bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate();

	}

	/// <summary>
	///   Generates a list of fake <see cref="Category" /> objects.
	/// </summary>
	/// <param name="numberRequested">The number of <see cref="Category" /> objects to generate.</param>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A list of fake <see cref="Category" /> objects.</returns>
	public static List<Category> GetCategories(int numberRequested, bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate(numberRequested);

	}

	/// <summary>
	///   Generates a Faker Categories instance configured to generate fake Categories objects.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A configured Faker Categories instance.</returns>
	internal static Faker<Category> GenerateFake(bool useSeed = false)
	{
		const int seed = 621;

		var fake = new Faker<Category>()
				.RuleFor(x => x.Id, ObjectId.GenerateNewId())
				.RuleFor(x => x.Name, f => f.Random.Enum<CategoryNames>().ToString())
				.RuleFor(x => x.Description, (_, x) => $"This category is for {x.Name} related items.");

		return useSeed ? fake.UseSeed(seed) : fake;
	}

}