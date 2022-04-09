using System.Text.Json;
using FMail.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FMail.Services;

public class SmtpDbContext : DbContext
{
#nullable disable // Disable nullable since DbSets are created by the base class automatically
	public DbSet<SmtpMessage> Messages { get; set; }
	public DbSet<SmtpAttachment> Attachments { get; set; }
#nullable enable

	public SmtpDbContext(DbContextOptions<SmtpDbContext> options) : base(options)
	{
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		configurationBuilder.Properties<List<string>>()
			.HaveConversion<ListToStringConverter<string>, ListComparer<string>>();
		configurationBuilder.Properties<List<SmtpAddress>>()
			.HaveConversion<ListToStringConverter<SmtpAddress>, ListComparer<SmtpAddress>>();
	}
}

public class ListToStringConverter<T> : ValueConverter<List<T>, string>
{
	private static readonly JsonSerializerOptions _jsonOptions = new();

	public ListToStringConverter() : base(
		v => JsonSerializer.Serialize(v, _jsonOptions),
		v => JsonSerializer.Deserialize<List<T>>(v, _jsonOptions)!)
	{
	}
}


public class ListComparer<T> : ValueComparer<List<T>>
{
	public ListComparer() : base(
		(left, right) => left != null && right != null && left.SequenceEqual(right),
		c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
		c => c.ToList())
	{
	}
}
