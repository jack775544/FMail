namespace FMail.Data;

public class SmtpAttachment
{
	public Guid Id { get; set; }
	public byte[]? Contents { get; set; }
	public string? ContentType { get; set; }
}