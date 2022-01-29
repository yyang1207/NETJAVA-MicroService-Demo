using ComponentsSelectTest.ServiceF.DB;
using DBConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace ComponentsSelectTest.ServiceF
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
            //services.AddDbContext<ConfigContext>(options => options.UseMySQL("Server=10.0.20.55;Port=3306;Database=dbconfig;User ID=root;Password=hotwind;Charset=utf8;SslMode=None;"),ServiceLifetime.Singleton);
            //services.AddDbContext<StoreContext>(options => options.UseMySQL("Server=10.0.20.55;Port=3306;Database=dborder1;User ID=root;Password=hotwind;Charset=utf8;SslMode=None;"));
            //services.AddSingleton<StoreChangeService>();
            //services.AddDbContext<TestContext>(options => options.UseMySQL("Server=10.0.20.55;Port=3306;Database=dborder1;User ID=root;Password=hotwind;Charset=utf8;SslMode=None;"));

            services.AddShard(Configuration);
            services.AddDbContext<TestContext2>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultDB")));

            services.AddCategoryShard(Configuration);
            services.AddDbContext<TestContext3>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultDB")));

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

            app.UseRequestResponseLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
