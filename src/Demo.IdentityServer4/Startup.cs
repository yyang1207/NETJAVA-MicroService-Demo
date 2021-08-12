using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.IdentityServer4.MVC.DBContext;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.IdentityServer4.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddIdentityServer()
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = builder =>
                //    {
                //        builder.UseSqlServer(connectionString, options =>
                //             options.MigrationsAssembly(migrationsAssembly));
                //    };
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = builder =>
                //    {
                //        builder.UseSqlServer(connectionString, options =>options.MigrationsAssembly(migrationsAssembly));
                //    };
                //})
                //.AddDeveloperSigningCredential();

                .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don’t have a certificate to use.
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.GetClients());


            // 2、用户相关的配置
            services.AddDbContext<IdentityServerUserDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // 1.1 添加用户
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                // 1.2 密码复杂度配置
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<IdentityServerUserDbContext>()
            .AddDefaultTokenProviders();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ////1、初始化数据
            //InitializeDatabase(app);

            //// 2、初始化用户数据
            //InitializeUserDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // 1、使用IdentityServe4
            app.UseIdentityServer();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // 1、将config中数据存储起来
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }

        // 2、将用户中数据存储起来
        private void InitializeUserDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IdentityServerUserDbContext>();
                context.Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var idnetityUser = userManager.FindByNameAsync("tony").Result;
                if (idnetityUser == null)
                {
                    idnetityUser = new IdentityUser
                    {
                        UserName = "tony",
                        Email = "tony@email.com"
                    };
                    var result = userManager.CreateAsync(idnetityUser, "123456").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    result = userManager.AddClaimsAsync(idnetityUser, new Claim[] {
                        new Claim(JwtClaimTypes.Name, "tony"),
                        new Claim(JwtClaimTypes.GivenName, "tony"),
                        new Claim(JwtClaimTypes.FamilyName, "tony"),
                        new Claim(JwtClaimTypes.Email, "tony@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://tony.com")
                    }).Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        }
    }
}
