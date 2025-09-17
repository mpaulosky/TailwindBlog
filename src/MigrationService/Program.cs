using MigrationService;
using Microsoft.EntityFrameworkCore;
using static Shared.Services;
using Web.Data;
using Web.Migrations;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<UserDbContext>(
		options => options.UseNpgsql(builder.Configuration.GetConnectionString(), options =>
		{
			options.MigrationsAssembly(typeof(UserDbContextModelSnapshot).Assembly.FullName);
		})
);

builder.Services.AddDbContext<WebDbContext>(
		options => options.UseNpgsql(builder.Configuration.GetConnectionString(ARTICLES_DB))
);

builder.Services.AddOpenTelemetry()
		.WithTracing(tracing => tracing.AddSource(Worker.ACTIVITY_NAME));

var host = builder.Build();

host.Run();