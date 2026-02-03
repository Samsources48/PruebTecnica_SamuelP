using Domain.Interfaces.Catalogos;
using Domain.Repository.Catalogo;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IProductosRepository, ProductosRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();

            return services;
        }
    }
}
