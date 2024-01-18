namespace FMail.Services;

public class SmtpHostedService(SmtpServer.SmtpServer smtpServer, ILogger<SmtpHostedService> logger)
	: IHostedService
{
	public Task StartAsync(CancellationToken cancellationToken)
	{
		_ = smtpServer.StartAsync(cancellationToken);
		logger.LogInformation("Started SMTP Server");
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		smtpServer.Shutdown();
		logger.LogInformation("Shutdown SMTP Server");
		return Task.CompletedTask;
	}
}