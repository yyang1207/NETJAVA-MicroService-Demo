using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConfig
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddShard(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ShardConfigContext>(options => options.UseMySQL(configuration.GetConnectionString("ConfigDB")), ServiceLifetime.Singleton);
            return services;
        }

        public static IServiceCollection AddCategoryShard(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CategoryShardConfigContext>(options => options.UseMySQL(configuration.GetConnectionString("ConfigDB")), ServiceLifetime.Singleton);
            return services;
        }
    }
}
