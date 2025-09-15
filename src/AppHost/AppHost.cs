var builder = DistributedApplication.CreateBuilder(args);

// Add SQL Server resource
var sqlserver = builder.AddSqlServer("sqlserver");
var database = sqlserver.AddDatabase("securitydb");

// TODO: Add MongoDB


builder.AddProject<Projects.Web>("web")
	.WithReference(database)
	.WithReference(mongoDatabase);

builder.Build().Run();