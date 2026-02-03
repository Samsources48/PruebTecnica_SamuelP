using Application.Features.Seguridad.Interfaces;
using Application.Features.Seguridad.Operations;
using Domain.Extensions;
using System.Runtime.CompilerServices;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensionsOperations
    {
        public static IServiceCollection AddServicesLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthOperation, AuthOperation>();
            services.AddScoped<IUsuariosOperation, UsuariosOperation>();
            services.AddScoped<IRolesOperation, RolesOperation>();

            services.AddDataAccessServices();
            return services;
        }
    }
}
