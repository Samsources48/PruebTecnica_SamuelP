using Application.DTOs;

namespace Application.Features.Dashboard.Interfaces
{
    public interface IDashboardOperation
    {
        Task<DashboardStatsDto> GetStats();
    }
}
