using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkyWalkingAgentExtension;
using System;

namespace ComponentsSelectTest.ServiceA
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
            _logger.LogInformation("ServiceA Called Logs", LogCode.Information);
            return $"ServiceA-OK:{DateTime.Now}";
        }
    }
}
