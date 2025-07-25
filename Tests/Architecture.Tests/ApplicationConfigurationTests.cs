// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name:     ApplicationConfigurationTests.cs
// Project Name:  Architecture.Tests
// =======================================================

namespace Architecture.Tests;

[ExcludeFromCodeCoverage]
public class ApplicationConfigurationTests
{

	[Fact(DisplayName = "Config Test: Configuration classes should follow naming convention")]
	public void Configuration_Classes_Should_Follow_Naming_Convention()
	{
		// Arrange
		var assemblies = new[]
		{
				AssemblyReference.Domain,
				AssemblyReference.Persistence,
				AssemblyReference.Web,
				AssemblyReference.MyMediator,
				AssemblyReference.Migrations
		};

		// Act
		var result = Types.InAssemblies(assemblies)
				.That()
				.HaveNameEndingWith("Configuration")
				.Should()
				.ResideInNamespaceStartingWith("TailwindBlog")
				.And()
				.BePublic()
				.GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact(DisplayName = "Config Test: Configuration classes should be concrete")]
	public void Configuration_Classes_Should_Be_Concrete()
	{
		// Arrange
		var assemblies = new[]
		{
				AssemblyReference.Domain,
				AssemblyReference.Persistence,
				AssemblyReference.Web,
				AssemblyReference.MyMediator,
				AssemblyReference.Migrations
		};

		// Act
		var result = Types.InAssemblies(assemblies)
				.That()
				.HaveNameEndingWith("Configuration")
				.Should()
				.BeClasses()
				.And()
				.NotBeAbstract()
				.GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue();
	}

}