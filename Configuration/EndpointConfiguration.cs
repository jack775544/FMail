namespace FMail.Configuration;

public class EndpointConfiguration
{
	public int Port { get; set; }
	public bool Secure { get; set; }
	public bool EnableAuthentication { get; set; }
}