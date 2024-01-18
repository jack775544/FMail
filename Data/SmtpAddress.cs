using MimeKit;

namespace FMail.Data;

public class SmtpAddress
{
	public string? Name { get; set; }
	public string? Address { get; set; }

	public SmtpAddress()
	{
	}

	public SmtpAddress(InternetAddress address)
	{
		Name = address.Name;

		if (address is MailboxAddress mailboxAddress)
		{
			Address = mailboxAddress.Address;
		}
	}

	public static string FormatAddresses(IEnumerable<SmtpAddress>? input)
	{
		if (input == null)
		{
			return "No From Address";
		}

		var addresses = new List<string>();
		foreach (var address in input)
		{
			if (!string.IsNullOrWhiteSpace(address.Address) && !string.IsNullOrWhiteSpace(address.Name))
			{
				addresses.Add($"{address.Name} ({address.Address})");
			}
			else if (!string.IsNullOrWhiteSpace(address.Address))
			{
				addresses.Add(address.Address);
			}
			else
			{
				addresses.Add("Invalid Address");
			}
		}

		return string.Join(", ", addresses);
	}
}