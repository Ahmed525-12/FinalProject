using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ThriftinessCore.Entites.Identity;
using ThriftinessRepository.AppIdentity;

namespace Talabat.PL.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            return Services;
        }
    }
}