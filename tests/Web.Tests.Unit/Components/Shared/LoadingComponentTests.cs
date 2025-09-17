// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     LoadingComponentTests.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : BlazorBlogApplication
// Project Name :  Web.Tests.Unit
// =======================================================

namespace Web.Components.Shared;

/// <summary>
///   Unit tests for LoadingComponent.
/// </summary>
[ExcludeFromCodeCoverage]
[TestSubject(typeof(LoadingComponent))]
public class LoadingComponentTests : BunitContext
{

	[Fact]
	public void LoadingComponent_RendersSpinner()
	{

		// Arrange
		const string expectedHtml =
				"""
				<div class="mx-auto max-w-7xl flex justify-center items-center py-4 bg-gray-800 rounded-md shadow-md shadow-blue-500">
				  <svg class="animate-spin h-8 w-8 text-blue-500 fill-cyan-500 ml-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
				    <circle class="opacity-25 " cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
				    <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
				  </svg>
				  <h3 class="text-2xl font-bold tracking-tight text-gray-50 text-shadow-md text-shadow-blue-500 ml-4">Loading...</h3>
				</div>
				""";

		// Act
		var cut = Render<LoadingComponent>();

		// Assert
		cut.MarkupMatches(expectedHtml); // Update selector as needed

	}

}
