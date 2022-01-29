using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkyWalkingAgentExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace swmsweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private static long count = 0;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            Interlocked.Increment(ref count);
            _logger.LogInformation("swms healthcheck", LogCode.Information);
            return $"swms-OK:{DateTime.Now},访问次数:{count}";
        }
    }
}
