// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakeArticleDto.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : BlazorBlogApplication
// Project Name :  Shared
// =======================================================

namespace Shared.Fakes;

/// <summary>
///   Provides fake data generation methods for the <see cref="ArticleDto" /> entity.
/// </summary>
public static class FakeArticleDto
{

	private const int SEED = 621;

	/// <summary>
	///   Generates a new fake <see cref="ArticleDto" /> object.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A single fake <see cref="Article" /> object.</returns>
	public static ArticleDto GetNewArticleDto(bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate();

	}

	/// <summary>
	///   Generates a list of fake <see cref="ArticleDto" /> objects.
	/// </summary>
	/// <param name="numberRequested">The number of <see cref="Article" /> objects to generate.</param>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A list of fake <see cref="Article" /> objects.</returns>
	public static List<ArticleDto> GetArticleDtos(int numberRequested, bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate(numberRequested);

	}

	/// <summary>
	///   Generates a Faker instance configured to generate fake <see cref="ArticleDto" /> objects.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>Configured Faker <see cref="ArticleDto" /> instance.</returns>
	internal static Faker<ArticleDto> GenerateFake(bool useSeed = false)
	{
		var fake = new Faker<ArticleDto>()
				.RuleFor(f => f.Title, f => f.WaffleTitle())
				.RuleFor(f => f.Introduction, f => f.Lorem.Sentence())
				.RuleFor(f => f.Content, f => f.WaffleMarkdown(5))
				.RuleFor(f => f.UrlSlug, (_, f) => f.Title.GetSlug())
				.RuleFor(f => f.CoverImageUrl, f => f.Image.PicsumUrl() ?? string.Empty)
				.RuleFor(f => f.IsPublished, f => f.Random.Bool())
				.RuleFor(f => f.PublishedOn, (_, f) => f.IsPublished ? DateTime.Now : null)
				.RuleFor(f => f.Category, _ => FakeCategoryDto.GetNewCategoryDto(useSeed))
				.RuleFor(f => f.Author, _ => FakeAppUserDto.GetNewAppUserDto(useSeed))
				.RuleFor(f => f.CreatedOn, _ => DateTime.Now)
				.RuleFor(f => f.ModifiedOn, _ => DateTime.Now);


		return useSeed ? fake.UseSeed(SEED) : fake;

	}

}
