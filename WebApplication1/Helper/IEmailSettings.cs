using WebApplication1.ViewModel;

namespace WebApplication1.Helper
{
	public interface IEmailSettings
	{
		public void SendEmail(Email email);
	}
}