// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakeArticle.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Domain
// =======================================================

namespace Domain.Fakes;

/// <summary>
///   Provides fake data generation methods for the <see cref="Article" /> entity.
/// </summary>
public static class FakeArticle
{

	/// <summary>
	///   Generates a new fake <see cref="Article" /> object.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A single fake <see cref="Article" /> object.</returns>
	public static Article GetNewArticle(bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate();

	}

	/// <summary>
	///   Generates a list of fake <see cref="Article" /> objects.
	/// </summary>
	/// <param name="numberRequested">The number of <see cref="Article" /> objects to generate.</param>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A list of fake <see cref="Article" /> objects.</returns>
	public static List<Article> GetArticles(int numberRequested, bool useSeed = false)
	{

		var articles = new List<Article>();

		for (var i = 0; i < numberRequested; i++)
		{

			var article = GenerateFake(useSeed).Generate();
			articles.Add(article);

		}

		return articles;

	}

	/// <summary>
	///   Generates a Faker instance configured to generate fake <see cref="Article" /> objects.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>Configured Faker <see cref="Article" /> instance.</returns>
	internal static Faker<Article> GenerateFake(bool useSeed = false)
	{
		var fake = new Faker<Article>()
				.RuleFor(x => x.Id, Guid.CreateVersion7)
				.RuleFor(f => f.Title, f => f.WaffleTitle())
				.RuleFor(f => f.Introduction, f => f.Lorem.Sentence())
				.RuleFor(f => f.Content, f => f.WaffleMarkdown(5))
				.RuleFor(x => x.UrlSlug, (_, x) => x.Title.GetSlug())
				.RuleFor(f => f.CoverImageUrl, f => f.Image.PicsumUrl())
				.RuleFor(f => f.CreatedOn, _ => Helpers.Helpers.GetStaticDate())
				.RuleFor(f => f.ModifiedOn, _ => Helpers.Helpers.GetStaticDate())
				.RuleFor(f => f.IsPublished, f => f.Random.Bool())
				.RuleFor(x => x.PublishedOn, (_, x) => x.IsPublished ? Helpers.Helpers.GetStaticDate() : null)
				.RuleFor(f => f.Category, _ => null)
				.RuleFor(f => f.Author, _ => null);


		const int seed = 621;

		return useSeed ? fake.UseSeed(seed) : fake;

	}

}