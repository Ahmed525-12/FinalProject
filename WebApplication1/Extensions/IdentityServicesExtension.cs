using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ThriftinessCore.Entites.Identity;
using ThriftinessRepository.AppIdentity;
using WebApplication1.Helper;
using WebApplication1.ViewModel;

namespace Talabat.PL.Extensions
{
	public static class IdentityServicesExtension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration _configuration)
		{
			Services.AddIdentity<AppUser, IdentityRole>()
							.AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
			Services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));
			Services.AddTransient<IEmailSettings, EmailSettings>();
			return Services;
		}
	}
}