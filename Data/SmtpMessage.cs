using MimeKit;

namespace FMail.Data;

public class SmtpMessage
{
	public Guid Id { get; set; }
	public List<SmtpAddress>? From { get; set; }
	public List<SmtpAddress>? To { get; set; }
	public List<SmtpAddress>? Cc { get; set; }
	public List<SmtpAddress>? Bcc { get; set; }
	public DateTime Date { get; set; }
	public string? Subject { get; set; }
	public string? Body { get; set; }
	public byte[]? RawContent { get; set; }
	public List<SmtpAttachment>? Attachments { get; set; }


	public SmtpMessage()
	{
	}

	public SmtpMessage(MimeMessage message)
	{
		LoadMimeMessage(message);
		using var ms = new MemoryStream();
		message.WriteTo(ms);
		RawContent = ms.ToArray();
	}

	public SmtpMessage(MemoryStream mimeStream)
	{
		var message = MimeMessage.Load(mimeStream);
		LoadMimeMessage(message);
		RawContent = mimeStream.ToArray();
	}

	private void LoadMimeMessage(MimeMessage message)
	{
		From = message.From.Select(x => new SmtpAddress(x)).ToList();
		To = message.To.Select(x => new SmtpAddress(x)).ToList();
		Cc = message.Cc.Select(x => new SmtpAddress(x)).ToList();
		Bcc = message.Bcc.Select(x => new SmtpAddress(x)).ToList();
		Date = message.Date.UtcDateTime;
		Subject = message.Subject;
		Body = message.HtmlBody ?? message.TextBody;
		Attachments = message.Attachments.Select(x =>
		{
			using var content = new MemoryStream();
			x.WriteTo(content);
			return new SmtpAttachment
			{
				Contents = content.ToArray(),
				ContentType = x.ContentType.ToString()
			};
		}).ToList();
	}
}
