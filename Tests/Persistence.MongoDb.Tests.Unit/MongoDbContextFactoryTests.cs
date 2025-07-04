// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     MongoDbContextFactoryTests.cs
// Company :       mpaulosky
// Author :        Matthew
// Solution Name : TailwindBlog
// Project Name :  Persistence.MongoDb.Tests.Unit
// =======================================================

namespace Persistence;

/// <summary>
///   Unit tests for the <see cref="MongoDbContextFactory"/> class.
/// </summary>
[ExcludeFromCodeCoverage]
[TestSubject(typeof(MongoDbContextFactory))]
public sealed class MongoDbContextFactoryTests
{

	private readonly Mock<IDatabaseSettings> _mockSettings;

	private readonly Mock<IMongoClient> _mockClient;

	private readonly Mock<IMongoDatabase> _mockDatabase;

	public MongoDbContextFactoryTests()
	{

		// Setup mock settings
		_mockSettings = new Mock<IDatabaseSettings>();
		_mockSettings.Setup(s => s.ConnectionStrings).Returns("mongodb://localhost:27017");
		_mockSettings.Setup(s => s.DatabaseName).Returns("test-db");

		// Setup mock MongoDB client
		_mockClient = new Mock<IMongoClient>();
		_mockDatabase = new Mock<IMongoDatabase>();
		var mockCollection = new Mock<IMongoCollection<TestEntity>>();

		// Setup client to return a database
		_mockClient.Setup(c => c.GetDatabase(It.IsAny<string>(), null))
				.Returns(_mockDatabase.Object);

		// Setup database to return collection
		_mockDatabase.Setup(d => d.GetCollection<TestEntity>(It.IsAny<string>(), null))
				.Returns(mockCollection.Object);

	}

	public class TestEntity
	{

		public string Id { get; set; } = string.Empty;

	}

	internal MongoDbContextFactory CreateSut()
	{

		// Create a new instance of MongoDbContextFactory with our mocked client
		var factory = new MongoDbContextFactory(_mockSettings.Object, _mockClient.Object);

		return factory;

	}

	[Fact(DisplayName = "Constructor - Should Initialize Properties")]
	public void Constructor_ShouldInitializeProperties()
	{

		// Arrange & Act
		var sut = CreateSut();

		// Assert
		sut.ConnectionString.Should().Be("mongodb://localhost:27017");
		sut.DbName.Should().Be("test-db");
		sut.Database.Should().NotBeNull();
		sut.Client.Should().NotBeNull();

	}

	[Fact(DisplayName = "GetCollection - With Valid Name - Should Return Collection")]
	public void GetCollection_WithValidName_ShouldReturnCollection()
	{

		// Arrange
		var collectionName = "test-collection";
		var sut = CreateSut();

		// Act
		var result = sut.GetCollection<TestEntity>(collectionName);

		// Assert
		result.Should().NotBeNull();
		_mockDatabase.Verify(d => d.GetCollection<TestEntity>(collectionName, null), Times.Once);

	}

	[Fact(DisplayName = "GetCollection - With Null Name - Should Throw ArgumentException")]
	public void GetCollection_WithNullName_ShouldThrowArgumentException()
	{

		// Arrange
		string? nullName = null;
		var sut = CreateSut();

		// Act & Assert
		var action = () => sut.GetCollection<TestEntity>(nullName);

		action.Should().Throw<ArgumentException>()
				.WithMessage("Value cannot be null. (Parameter 'name')");

	}

	[Fact(DisplayName = "GetCollection - With Empty Name - Should Throw ArgumentException")]
	public void GetCollection_WithEmptyName_ShouldThrowArgumentException()
	{

		// Arrange
		var emptyName = string.Empty;
		var sut = CreateSut();

		// Act & Assert
		var action = () => sut.GetCollection<TestEntity>(emptyName);

		action.Should().Throw<ArgumentException>()
				.WithMessage("The value cannot be an empty string. (Parameter 'name')");

	}

	[Fact(DisplayName = "Constructor - Should Create Client With Correct Connection String")]
	public void Constructor_ShouldCreateClientWithCorrectConnectionString()
	{

		// Arrange
		var connectionString = "mongodb://test-server:27017";
		_mockSettings.Setup(s => s.ConnectionStrings).Returns(connectionString);

		// Act
		var sut = CreateSut();

		// Assert
		sut.ConnectionString.Should().Be(connectionString);

	}

	[Fact(DisplayName = "Constructor - Should Get Database With Correct Name")]
	public void Constructor_ShouldGetDatabaseWithCorrectName()
	{

		// Arrange
		var databaseName = "custom-test-db";
		_mockSettings.Setup(s => s.DatabaseName).Returns(databaseName);

		// Act
		var sut = CreateSut();

		// Assert
		sut.DbName.Should().Be(databaseName);
		_mockClient.Verify(c => c.GetDatabase(databaseName, null), Times.Once);

	}

}