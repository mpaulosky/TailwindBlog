using static Shared.Services;

var builder = DistributedApplication.CreateBuilder(args);

// Add PostgreSQL server resource with data volume and PgAdmin
var postgres = builder.AddPostgres("postgres")
	.WithLifetime(ContainerLifetime.Persistent)
	.WithDataVolume("postgres-data")
	.WithPgAdmin();

// Add databases
var userDatabase = postgres.AddDatabase(USER_DB);
var articlesDatabase = postgres.AddDatabase(ARTICLES_DB);

builder.AddProject<Projects.Web>("web")
	.WithReference(userDatabase)
	.WithReference(articlesDatabase);

builder.Build().Run();