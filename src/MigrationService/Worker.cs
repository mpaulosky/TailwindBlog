using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using System.Diagnostics;

using Web.Data;

namespace MigrationService;

public class Worker : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private readonly IHostApplicationLifetime _hostApplicationLifetime;
	private readonly ILogger<Worker> _logger;

	internal const string ACTIVITY_NAME = "MigrationService";
	private static readonly ActivitySource _sActivitySource = new(ACTIVITY_NAME);

	public Worker(IServiceProvider serviceProvider,
			IHostApplicationLifetime hostApplicationLifetime,
			ILogger<Worker> logger)
	{
		this._serviceProvider = serviceProvider;
		this._hostApplicationLifetime = hostApplicationLifetime;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{

		using var userActivity = _sActivitySource.StartActivity("Migrating database", ActivityKind.Client);

		try
		{
			using var scope = _serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

			await dbContext.Database.MigrateAsync(stoppingToken);

		}
		catch (Exception ex)
		{
			userActivity?.AddException(ex);
			throw;
		}

		using var webActivity = _sActivitySource.StartActivity("Migrating database", ActivityKind.Client);

		try
		{
			using var scope = _serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<WebDbContext>();

			await dbContext.Database.MigrateAsync(stoppingToken);

		}
		catch (DbUpdateConcurrencyException dbEx)
		{
			webActivity?.AddException(dbEx);

			throw;
		}
		catch (Exception ex)
		{
			userActivity?.AddException(ex);

			throw;
		}

		_hostApplicationLifetime.StopApplication();
	}
}