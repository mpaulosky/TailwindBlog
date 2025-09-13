var builder = DistributedApplication.CreateBuilder(args);

// Add SQL Server resource
var sqlserver = builder.AddSqlServer("sqlserver");
var database = sqlserver.AddDatabase("tailwinddb");

builder.AddProject<Projects.Web>("web")
	.WithReference(database);

// TODO: Add MongoDB

// TODO: Add Website

builder.Build().Run();