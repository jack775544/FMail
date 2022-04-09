namespace FMail.Services;

public interface IMessageStoreChanged
{
	public delegate void MessageChangedDelegate();

	public event MessageChangedDelegate OnMessageChanged;
}