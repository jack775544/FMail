using System.Buffers;
using FMail.Data;
using Microsoft.EntityFrameworkCore;
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;

namespace FMail.Services;

public class EfMessageStore : MessageStore, IMessageStoreChanged
{
	private readonly IDbContextFactory<SmtpDbContext> _dbContextFactory;
	private readonly ILogger<EfMessageStore> _logger;

	public event IMessageStoreChanged.MessageChangedDelegate? OnMessageChanged;

	public EfMessageStore(IDbContextFactory<SmtpDbContext> dbContextFactory, ILogger<EfMessageStore> logger)
	{
		_dbContextFactory = dbContextFactory;
		_logger = logger;
	}

	public override async Task<SmtpResponse> SaveAsync(
		ISessionContext context,
		IMessageTransaction transaction,
		ReadOnlySequence<byte> buffer,
		CancellationToken cancellationToken)
	{
		try
		{
			await using var stream = new MemoryStream();

			var position = buffer.GetPosition(0);
			while (buffer.TryGet(ref position, out var memory))
			{
				stream.Write(memory.Span);
			}

			stream.Position = 0;

			var message = await MimeKit.MimeMessage.LoadAsync(stream, cancellationToken);
			var smtpMessage = new SmtpMessage(message);

			await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
			dbContext.Messages.Add(smtpMessage);
			await dbContext.SaveChangesAsync(cancellationToken);

			OnMessageChanged?.Invoke();

			return SmtpResponse.Ok;
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Failed to save mail message");
			return SmtpResponse.TransactionFailed;
		}
	}
}