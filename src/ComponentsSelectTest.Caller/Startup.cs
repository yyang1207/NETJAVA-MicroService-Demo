using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.AspNetCore.V2;
using Nacos.V2.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.Caller
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddOmegaCore(option =>
            //{
            //    // your alpha-server address
            //    option.GrpcServerAddress = "10.0.20.56:8080";
            //    // your app identification
            //    option.InstanceId = "Caller";
            //    // your app name
            //    option.ServiceName = "Caller";
            //});


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //注册服务时使用
            //services.AddNacosAspNet(Configuration);

            //只使用服务发现时使用
            services.AddNacosV2Naming(x =>
            {
                Configuration.GetSection("nacos").Bind(x);

                x.EndPoint = "";
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
