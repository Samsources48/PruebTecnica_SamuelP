using Application.DTOs;
using Application.Features.Dashboard.Interfaces;
using Domain.Interfaces.Catalogos;

namespace Application.Features.Dashboard.Operations
{
    public class DashboardOperation(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository) : IDashboardOperation
    {
        public async Task<DashboardStatsDto> GetStats()
        {
            var pedidos = await pedidoRepository.GetAllAsync();
            var clientes = await clienteRepository.GetAllAsync();

            var completedOrders = pedidos.Count(p => p.Estado.Equals("COMPLETED", StringComparison.OrdinalIgnoreCase));
            var pendingOrders = pedidos.Count(p => p.Estado.Equals("PENDING", StringComparison.OrdinalIgnoreCase));
            var activeClients = clientes.Count(c => c.Activo);

            var activityByDate = pedidos
                .GroupBy(p => p.FechaPedido.Date)
                .Select(g => new ActivityByDateDto
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Count()
                })
                .OrderBy(a => a.Date)
                .ToList();

            return new DashboardStatsDto
            {
                TotalOrders = pedidos.Count,
                CompletedOrders = completedOrders,
                PendingOrders = pendingOrders,
                ActiveClients = activeClients,
                ActivityByDate = activityByDate
            };
        }
    }
}
