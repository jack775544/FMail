using FMail.Data;
using Microsoft.EntityFrameworkCore;

namespace FMail.Services;

public class SmtpDbContext(DbContextOptions<SmtpDbContext> options) : DbContext(options)
{
	public DbSet<SmtpMessage> Messages => Set<SmtpMessage>();
	public DbSet<SmtpAttachment> Attachments => Set<SmtpAttachment>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SmtpMessage>()
			.OwnsMany(x => x.From, d => { d.ToJson(); })
			.OwnsMany(x => x.To, d => { d.ToJson(); })
			.OwnsMany(x => x.Cc, d => { d.ToJson(); })
			.OwnsMany(x => x.Bcc, d => { d.ToJson(); });
	}
}