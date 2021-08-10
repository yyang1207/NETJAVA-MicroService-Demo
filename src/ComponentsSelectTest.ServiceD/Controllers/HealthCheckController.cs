using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly Nacos.V2.INacosNamingService _svc;

        public HealthCheckController(ILogger<HealthCheckController> logger, Nacos.V2.INacosNamingService svc)
        {
            _logger = logger;
            _svc = svc;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            //_logger.LogError("ServiceD Called Logs");

            string svf = await GetHealthInfo("ServiceF", "DEFAULT_GROUP", "api/HealthCheck");

            return $"ServiceD-OK,{svf}";
        }


        private async Task<string> GetHealthInfo(string serviceName, string groupName, string path)
        {
            // need to know the service name.
            var instance = await _svc.SelectOneHealthyInstance(serviceName, groupName);
            var host = $"{instance.Ip}:{instance.Port}";

            var baseUrl = instance.Metadata.TryGetValue("secure", out _)
                ? $"https://{host}"
                : $"http://{host}";

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return "";
            }

            var url = $"{baseUrl}/{path}";

            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
