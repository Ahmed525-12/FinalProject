using Microsoft.AspNetCore.Mvc;
using Talabat.PL.Helper;
using ThriftinessCore.Repos;
using ThriftinessRepository.Repos;

namespace TODO.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddAutoMapper(typeof(MappingProfiles));
            return Services;
        }
    }
}