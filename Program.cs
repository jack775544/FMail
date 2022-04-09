using FMail.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmtpServer;
using SmtpServer.Authentication;
using SmtpServer.Storage;
using FMail.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddDbContextFactory<SmtpDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Main"));
});

builder.Services.AddHostedService<SmtpHostedService>();
builder.Services.TryAddSingleton<IMessageStore, EfMessageStore>();
builder.Services.TryAddSingleton<IMessageStoreChanged, EfMessageStore>();
builder.Services.TryAddSingleton<IUserAuthenticatorFactory, AllowAllUserAuthenticator>();
builder.Services.TryAddSingleton<IUserAuthenticator, AllowAllUserAuthenticator>();
builder.Services.TryAddSingleton(serviceProvider =>
{
	var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
	var options = new SmtpServerOptionsBuilder().ServerName("localhost");

	foreach (var endpoint in appSettings.Value.Endpoints)
	{
		options = options.Endpoint(e =>
		{
			e = e.Port(endpoint.Port, endpoint.Secure);

			if (!endpoint.Secure)
			{
				e = e.AllowUnsecureAuthentication();
			}

			if (endpoint.EnableAuthentication)
			{
				e = e.AllowUnsecureAuthentication();
			}

			e.Build();
		});
	}

	return new SmtpServer.SmtpServer(options.Build(), serviceProvider);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// ---------------------- Configure Services Ended ----------------------

var app = builder.Build();

await using (var dbContext = await app.Services.GetRequiredService<IDbContextFactory<SmtpDbContext>>().CreateDbContextAsync())
{
	await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();