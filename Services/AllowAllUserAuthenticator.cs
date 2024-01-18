using SmtpServer;
using SmtpServer.Authentication;

namespace FMail.Services;

public class AllowAllUserAuthenticator(IServiceProvider serviceProvider, ILogger<AllowAllUserAuthenticator> logger)
	: IUserAuthenticator, IUserAuthenticatorFactory
{
	public Task<bool> AuthenticateAsync(ISessionContext context, string user, string password, CancellationToken cancellationToken)
	{
		logger.LogInformation("Authenticating user {User} with password {Password}", user, password);
		return Task.FromResult(true);
	}

	public IUserAuthenticator CreateInstance(ISessionContext context)
	{
		return serviceProvider.GetRequiredService<IUserAuthenticator>();
	}
}