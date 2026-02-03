using Application.Features.Products.Interfaces;
using Application.Features.Products.Operations;
using Application.Features.Clientes.Interfaces;
using Application.Features.Clientes.Operations;
using Application.Features.Pedidos.Interfaces;
using Application.Features.Pedidos.Operations;
using Application.Features.Dashboard.Interfaces;
using Application.Features.Dashboard.Operations;
using Domain.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensionsOperations
    {
        public static IServiceCollection AddServicesLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductsOperation, ProductsOperation>();
            services.AddScoped<IClientesOperation, ClientesOperation>();
            services.AddScoped<IPedidosOperation, PedidosOperation>();
            services.AddScoped<IDashboardOperation, DashboardOperation>();

            services.AddDataAccessServices();
            return services;
        }
    }
}
