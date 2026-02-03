using Application.DTOs;
using Application.Features.Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DashboardController(ILogger<DashboardController> logger, IDashboardOperation dashboardOperation) : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger = logger;
        private readonly IDashboardOperation _dashboardOperation = dashboardOperation;

        [HttpGet("stats")]
        [ProducesResponseType(typeof(DashboardStatsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var stats = await _dashboardOperation.GetStats();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard stats");
                return BadRequest("Error al obtener estadísticas del dashboard");
            }
        }
    }
}
