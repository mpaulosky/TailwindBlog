var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Web>("web");

// TODO: Add SqlServer

// TODO: Add MongoDB

// TODO: Add Website

builder.Build().Run();