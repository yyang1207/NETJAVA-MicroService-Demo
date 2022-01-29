using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkyWalkingAgentExtension;

namespace ComponentsSelectTest.ServiceF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("ServiceF Called Logs",LogCode.Information);
            return "ServiceF-OK";
        }
    }
}
