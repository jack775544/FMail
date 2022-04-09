using SmtpServer;
using SmtpServer.Authentication;

namespace FMail.Services;

public class AllowAllUserAuthenticator : IUserAuthenticator, IUserAuthenticatorFactory
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<AllowAllUserAuthenticator> _logger;

	public AllowAllUserAuthenticator(IServiceProvider serviceProvider, ILogger<AllowAllUserAuthenticator> logger)
	{
		_serviceProvider = serviceProvider;
		_logger = logger;
	}

	public Task<bool> AuthenticateAsync(ISessionContext context, string user, string password, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Authenticating user {User} with password {Password}", user, password);
		return Task.FromResult(true);
	}

	public IUserAuthenticator CreateInstance(ISessionContext context)
	{
		return _serviceProvider.GetRequiredService<IUserAuthenticator>();
	}
}