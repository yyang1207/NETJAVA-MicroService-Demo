using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComponentsSelectTest.Caller.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Nacos.V2.INacosNamingService _svc;
        public BooksController(Nacos.V2.INacosNamingService service)
        {
            _svc = service;
        }

        /// <summary>
        /// 正常操作
        /// </summary>
        /// <returns></returns>
        [HttpGet("ok")]
        public async Task<string> BookSuccess()
        {
            string resp_A = await GetResponseAsync("ServiceA", "DEFAULT_GROUP", "api/BookA");
            string resp_B = await GetResponseAsync("ServiceA", "DEFAULT_GROUP", "api/BookB");
            return $"Ok-{resp_A}-{resp_B}";
        }

        private async Task<string> GetResponseAsync(string serviceName, string groupName, string path)
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
