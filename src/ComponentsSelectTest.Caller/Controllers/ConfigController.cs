using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.Caller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _settings;
        private readonly AppSettings _snapShotSettings;
        private readonly AppSettings _monitorSettings;

        public ConfigController(
            ILogger<ConfigController> logger,
            IConfiguration configuration,
            IOptions<AppSettings> options,
            IOptionsSnapshot<AppSettings> sOptions,
            IOptionsMonitor<AppSettings> _mOptions
            )
        {
            _logger = logger;
            _configuration = configuration;
            _settings = options.Value;
            _snapShotSettings = sOptions.Value;
            _monitorSettings = _mOptions.CurrentValue;
        }

        [HttpGet]
        public string Get()
        {
            string id = Guid.NewGuid().ToString("N");

            _logger.LogInformation($"==========={_configuration["all"]}======");

            _logger.LogInformation($"============== begin {id} =====================");

            var conn = _configuration.GetConnectionString("Default");
            _logger.LogInformation($"{id} conn = {conn}");

            var version = _configuration["version"];
            _logger.LogInformation($"{id} version = {version}");

            var str1 = Newtonsoft.Json.JsonConvert.SerializeObject(_settings);
            _logger.LogInformation($"{id} IOptions = {str1}");

            var str2 = Newtonsoft.Json.JsonConvert.SerializeObject(_snapShotSettings);
            _logger.LogInformation($"{id} IOptionsSnapshot = {str2}");

            var str3 = Newtonsoft.Json.JsonConvert.SerializeObject(_monitorSettings);
            _logger.LogInformation($"{id} IOptionsMonitor = {str3}");

            _logger.LogInformation($"===============================================");

            return "ok";
        }

        [HttpGet("log")]
        public string Test()
        {
            string info = $"{"日志测试"},{DateTime.UtcNow.Ticks}";
            _logger.LogError(info);
            return info;
        }
    }
}
