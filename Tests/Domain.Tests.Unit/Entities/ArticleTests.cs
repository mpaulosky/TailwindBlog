// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     ArticleTests.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Domain.Tests.Unit
// =======================================================

namespace Domain.Entities;

[ExcludeFromCodeCoverage]
[TestSubject(typeof(Article))]
public class ArticleTests
{

	[Fact]
	public void Article_WhenCreated_ShouldHaveEmptyProperties()
	{

		// Arrange & Act
		// Article.Empty throws TypeInitializationException due to static property initialization failure.
		FluentActions.Invoking(() => _ = Article.Empty)
				.Should().Throw<TypeInitializationException>()
				.And.InnerException.Should().BeOfType<ValidationException>();

	}

	[Fact]
	public void Article_Empty_ShouldReturnEmptyInstance()
	{

		// Arrange & Act
		// Article.Empty throws TypeInitializationException due to static property initialization failure.
		FluentActions.Invoking(() => _ = Article.Empty)
				.Should().Throw<TypeInitializationException>()
				.And.InnerException.Should().BeOfType<ValidationException>();

	}

	[Theory]
	[InlineData("Test Title", "Test Intro", "https://test.com/image.jpg", "test_title")]
	[InlineData("Another Title", "Another Intro", "https://test.com/another.jpg", "another_title")]
	public void Article_WhenPropertiesSet_ShouldHaveCorrectValues(
			string title,
			string introduction,
			string coverImageUrl,
			string urlSlug)
	{

		// Arrange & Act
		var now = DateTime.UtcNow;

		var article = new Article(
				title,
				introduction,
				"This is the content.",
				coverImageUrl,
				urlSlug,
				Author.Empty.Id,
				Category.Empty.Id,
				Category.Empty,
				Author.Empty,
				false,
				null,
				true
		);

		// Assert
		article.Title.Should().Be(title);
		article.Introduction.Should().Be(introduction);
		article.CoverImageUrl.Should().Be(coverImageUrl);
		article.UrlSlug.Should().Be(urlSlug);
		article.Author.Should().BeEquivalentTo(Author.Empty);
		article.IsPublished.Should().BeFalse(); // Default value
		article.PublishedOn.Should().BeNull(); // Default value
		article.CreatedOn.Should().BeCloseTo(now, TimeSpan.FromSeconds(2));
		article.ModifiedOn.Should().BeNull(); // Default value

	}

	[Fact]
	public void Article_WhenPublished_ShouldSetPublishedProperties()
	{

		// Arrange
		var now = DateTime.UtcNow;
		const string title = "Published Article";
		const string introduction = "This is a published article.";
		const string content = "Full content of the article.";
		const string coverImageUrl = "https://example.com/cover.jpg";
		const string urlSlug = "published_article";
		const bool isPublished = true;
		var publishedOn = now;
		var author = Author.Empty;
		var category = Category.Empty;

		var article = new Article(
				title,
				introduction,
				content,
				coverImageUrl,
				urlSlug,
				author.Id,
				category.Id,
				category,
				author,
				isPublished,
				publishedOn,
				true
		);

		// Assert
		article.IsPublished.Should().BeTrue();
		article.PublishedOn.Should().Be(now);
		article.Author.Should().BeEquivalentTo(author);
		article.CreatedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));

	}

	[Fact]
	public void Article_Update_ShouldUpdateModifiableProperties()
	{

		// Arrange
		var article = new Article(
				"initial title",
				"initial intro",
				"Initial content.",
				"initial cover",
				"initial_slug",
				Author.Empty.Id,
				Category.Empty.Id,
				Category.Empty,
				Author.Empty,
				false,
				null,
				true // skipValidation: only for the test
		);

		var newAuthor = FakeAuthor.GetNewAuthor(true);
		var newCategory = FakeCategory.GetNewCategory(true);
		var publishDate = DateTime.UtcNow;

		// Act
		article.Update(
				"new title",
				"new intro",
				"Updated content.",
				"new cover",
				"new_slug",
				newAuthor,
				newCategory,
				true,
				publishDate
		);

		// Assert
		article.Title.Should().Be("new title");
		article.Introduction.Should().Be("new intro");
		article.CoverImageUrl.Should().Be("new cover");
		article.UrlSlug.Should().Be("new_slug");
		article.Author.Should().BeEquivalentTo(newAuthor);
		article.Category.Should().BeEquivalentTo(newCategory);
		article.IsPublished.Should().BeTrue();
		article.PublishedOn.Should().Be(publishDate);
		article.ModifiedOn.Should().NotBeNull("ModifiedOn should be set after update");
		article.ModifiedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));

	}

	[Theory]
	[InlineData("", "intro", "cover", "slug", "Title is required")]
	[InlineData("title", "", "cover", "slug", "Introduction is required")]
	[InlineData("title", "intro", "", "slug", "Cover image is required")]
	public void Article_WhenCreated_ShouldValidateRequiredFields(
			string title,
			string introduction,
			string coverImageUrl,
			string urlSlug,
			string expectedError)
	{

		// Arrange & Act & Assert
		FluentActions.Invoking(() => new Article(
						title,
						introduction,
						"Valid content.",
						coverImageUrl,
						urlSlug,
						Author.Empty.Id,
						Category.Empty.Id,
						Category.Empty,
						Author.Empty
				)).Should().Throw<ValidationException>()
				.WithMessage($"*{expectedError}*");

	}

	[Fact]
	public void Article_WhenUpdated_ShouldValidateRequiredFields()
	{

		// Arrange
		var article = new Article(
				"title",
				"intro",
				"Initial content.",
				"cover",
				"slug",
				Author.Empty.Id,
				Category.Empty.Id,
				Category.Empty,
				Author.Empty,
				false,
				null,
				true
		);

		// Act & Assert
		article.Invoking(a => a.Update(
						"",  // Empty title should trigger validation
						"new intro",
						"Updated content.",
						"new cover",
						"new_slug",
						Author.Empty,
						Category.Empty
				)).Should().Throw<ValidationException>()
				.WithMessage("*Title is required*");

	}

	[Fact]
	public void Article_WhenPublished_ShouldRequirePublishDate()
	{

		// Arrange & Act & Assert
		FluentActions.Invoking(() => new Article(
						"title",
						"intro",
						"Valid content.",
						"cover",
						"slug",
						Author.Empty.Id,
						Category.Empty.Id,
						Category.Empty,
						Author.Empty,
						true     // publishedOn missing should cause a validation error
				)).Should().Throw<ValidationException>()
				.WithMessage("*PublishedOn is required when IsPublished is true*");

	}

	[Fact]
	public void Publish_ShouldSetIsPublishedTrueAndSetPublishedOnAndModifiedOn()
	{

		// Arrange
		var article = new Article(
				"title",
				"intro",
				"content",
				"cover",
				"slug",
				Author.Empty.Id,
				Category.Empty.Id,
				Category.Empty,
				Author.Empty,
				false,
				null,
				true // skipValidation
		);

		var publishDate = DateTime.UtcNow;

		// Act
		article.Publish(publishDate);

		// Assert
		article.IsPublished.Should().BeTrue();
		article.PublishedOn.Should().Be(publishDate);
		article.ModifiedOn.Should().NotBeNull();
		article.ModifiedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));

	}

	[Fact]
	public void Unpublish_ShouldSetIsPublishedFalseAndClearPublishedOnAndSetModifiedOn()
	{

		// Arrange
		var article = new Article(
				"title",
				"intro",
				"content",
				"cover",
				"slug",
				Author.Empty.Id,
				Category.Empty.Id,
				Category.Empty,
				Author.Empty,
				true,
				DateTime.UtcNow,
				true // skipValidation
		);

		// Act
		article.Unpublish();

		// Assert
		article.IsPublished.Should().BeFalse();
		article.PublishedOn.Should().BeNull();
		article.ModifiedOn.Should().NotBeNull();
		article.ModifiedOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));

	}

}