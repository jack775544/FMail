namespace FMail.Services;

public class SmtpHostedService : IHostedService
{
	private readonly SmtpServer.SmtpServer _smtpServer;
	private readonly ILogger<SmtpHostedService> _logger;

	public SmtpHostedService(SmtpServer.SmtpServer smtpServer, ILogger<SmtpHostedService> logger)
	{
		_smtpServer = smtpServer;
		_logger = logger;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_ = _smtpServer.StartAsync(cancellationToken);
		_logger.LogInformation("Started SMTP Server");
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_smtpServer.Shutdown();
		_logger.LogInformation("Shutdown SMTP Server");
		return Task.CompletedTask;
	}
}