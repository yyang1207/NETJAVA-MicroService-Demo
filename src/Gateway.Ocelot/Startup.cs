using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Nacos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Ocelot
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
            var identityServerOptions = new IdentityServerOptions();
            Configuration.Bind("IdentityServerOptions", identityServerOptions);
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(identityServerOptions.IdentityScheme, options =>
            {
                options.Authority = identityServerOptions.AuthorityAddress; // 1、授权中心地址

                ////ApiName对应Audience，对应token接收人
                //options.ApiName = identityServerOptions.ResourceName; // 2、api名称(项目具体名称)
                options.RequireHttpsMetadata = false; // 3、https元数据，不需要
                //options.SupportedTokens = SupportedTokens.Both;
                //options.ApiSecret = "secret";
            });

            IdentityModelEventSource.ShowPII = true;

            services.AddOcelot().AddNacosDiscovery();

            Console.WriteLine(identityServerOptions.AuthorityAddress);
            Console.WriteLine(identityServerOptions.ResourceName);
            Console.WriteLine(identityServerOptions.IdentityScheme);

            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseOcelot().Wait();
        }
    }
}
