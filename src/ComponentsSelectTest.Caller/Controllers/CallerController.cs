using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceA
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallerController : ControllerBase
    {
        private readonly Nacos.V2.INacosNamingService _svc;

        public CallerController(Nacos.V2.INacosNamingService svc)
        {
            _svc = svc;
        }

        [HttpGet("test")]
        public async Task<string> Test()
        {
            string sva = await GetHealthInfo("ServiceA", "DEFAULT_GROUP", "api/HealthCheck");
            string svb = await GetHealthInfo("ServiceB", "DEFAULT_GROUP", "api/HealthCheck");

            return $"{sva},{svb}";
        }

        private async Task<string> GetHealthInfo(string serviceName,string groupName,string path)
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
