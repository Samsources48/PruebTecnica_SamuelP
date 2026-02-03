using Domain.Interfaces.Seguridad;
using Domain.Repository.Seguridad;

namespace Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            return services;
        }
    }
}
