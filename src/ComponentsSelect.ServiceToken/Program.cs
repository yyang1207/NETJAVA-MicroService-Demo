using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelect.ServiceToken
{
    /// <summary>
    /// 项目入口类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        /// <summary>
        /// 创建宿主构造器
        /// </summary>
        /// <param name="args">启动时的输入参数</param>
        /// <returns>返回构造器</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
