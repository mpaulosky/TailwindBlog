
namespace AppHost;

public static class DatabaseConfig
{

	public static IDistributedApplicationBuilder AddDatabase(
			this IDistributedApplicationBuilder builder,
			out IResourceBuilder<PostgresDatabaseResource> articlesDb,
			out IResourceBuilder<PostgresDatabaseResource> userDb,
			out IResourceBuilder<ProjectResource> migration)
	{

		var dbPassword = builder.AddParameter("dbPassword");

		var dbServer = builder.AddPostgres(POSTGRES_SERVER, password: dbPassword)
				.WithPgAdmin()
				.WithDataVolume("{POSTGRES_SERVER}-dev");

		articlesDb = dbServer.AddDatabase(ARTICLES_DB);

		userDb = dbServer.AddDatabase(USER_DB);


		migration = builder.AddProject<Projects.MigrationService>("db-migrations")
				.WaitFor(articlesDb)
				.WaitFor(userDb)
				.WithReference(articlesDb)
				.WithReference(userDb);

		return builder;

	}

}