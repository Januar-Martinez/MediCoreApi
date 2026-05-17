using MediCoreApi.Common;
using MediCoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCoreApi.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatistics()
        {
            var statistics = await _statisticsService.GetStatisticsAsync();
            return Ok(ApiResponse<object>.Ok(statistics, "Statistics retrieved successfully"));
        }
    }
}
