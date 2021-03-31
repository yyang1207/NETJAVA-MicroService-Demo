using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.Caller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var c = builder.Build();

                    // read configuration from config files
                    // it will use default json parser to parse the configuration store in nacos server.
                    builder.AddNacosV2Configuration(c.GetSection("NacosConfig"));
                    // you also can specify ini or yaml parser as well.
                    // builder.AddNacosConfiguration(c.GetSection("NacosConfig"), Nacos.IniParser.IniConfigurationStringParser.Instance);
                    // builder.AddNacosConfiguration(c.GetSection("NacosConfig"), Nacos.YamlParser.YamlConfigurationStringParser.Instance);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
