using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.AspNetCore.V2;
using SkyWalkingAgentExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swmsweb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //var builder = new ConfigurationBuilder()
            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //.AddEnvironmentVariables();
            //Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //DataBaseConfig.MysqlConnection1 = Configuration.GetConnectionString("SwmsMySqlServer1");

            //string ser_conn = Configuration.GetConnectionString("SwmsMySqlServer1");

            //string oldPassword = ser_conn.Split(';')[2].Replace("password=", "");
            //string newPassword = DESProvider.Decrypt(oldPassword, "hotwind.");
            //MySqlHelper.dbConnection= ser_conn.Replace(oldPassword, newPassword);


            MySqlHelper.LogPath = Configuration.GetSection("LogFilePath").Value;
            MySqlHelper.dbConnection= Configuration.GetConnectionString("SwmsMySqlServer1");

            //°æ±¾¿ØÖÆ
            services.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddApiVersioning(option =>
            {
                // allow a client to call you without specifying an api version
                // since we haven't configured it otherwise, the assumed api version will be 1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            });

            //services.AddControllers().AddNewtonsoftJson();
            services.AddNacosAspNet(Configuration);
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

         

            //app.UseAuthorization();
            
            app.UseRequestResponseLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
