// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakeCategoryTests.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Domain.Tests.Unit
// =======================================================

namespace Domain.Fakes;

[ExcludeFromCodeCoverage]
[TestSubject(typeof(FakeCategory))]
public class FakeCategoryTests
{

	[Fact]
	public void GetNewCategory_ShouldReturnCategory()
	{

		// Act
		var result = FakeCategory.GetNewCategory();

		// Assert
		result.Should().NotBeNull();
		result.Should().BeOfType<Category>();
		result.Name.Should().NotBeNullOrEmpty();

	}

	[Fact]
	public void GetNewCategory_WithSeed_ShouldReturnDeterministicResult()
	{

		// Act
		var result1 = FakeCategory.GetNewCategory(true);
		var result2 = FakeCategory.GetNewCategory(true);

		// Assert
		result1.Should().NotBeNull();
		result2.Should().NotBeNull();
		result1.Name.Should().Be(result2.Name);

	}

	[Theory]
	[InlineData(1)]
	[InlineData(5)]
	[InlineData(10)]
	public void GetCategories_ShouldReturnRequestedNumberOfCategories(int count)
	{

		// Act
		var results = FakeCategory.GetCategories(count);

		// Assert
		results.Should().NotBeNull();
		results.Should().HaveCount(count);
		results.Should().AllBeOfType<Category>();
		results.Should().OnlyContain(c => !string.IsNullOrEmpty(c.Name));

	}

	[Fact]
	public void GetCategories_WithSeed_ShouldReturnDeterministicResults()
	{

		// Arrange
		const int count = 3;

		// Act
		var results1 = FakeCategory.GetCategories(count, true);
		var results2 = FakeCategory.GetCategories(count, true);

		// Assert
		results1.Should().NotBeNull();
		results2.Should().NotBeNull();
		results1.Should().HaveCount(count);
		results2.Should().HaveCount(count);

		for (var i = 0; i < count; i++)
		{
			results1[i].Name.Should().Be(results2[i].Name);
		}

	}

	[Fact]
	public void GenerateFake_ShouldConfigureFakerCorrectly()
	{

		// Act
		var faker = FakeCategory.GenerateFake();
		var category = faker.Generate();

		// Assert
		category.Should().NotBeNull();
		category.Name.Should().NotBeNullOrEmpty();
		category.Name.Should().BeOneOf(Enum.GetNames<CategoryNames>());

	}

	[Fact]
	public void GenerateFake_WithSeed_ShouldApplySeed()
	{

		// Act
		var faker1 = FakeCategory.GenerateFake(true);
		var faker2 = FakeCategory.GenerateFake(true);

		var category1 = faker1.Generate();
		var category2 = faker2.Generate();

		// Assert
		category1.Name.Should().Be(category2.Name);

	}

}