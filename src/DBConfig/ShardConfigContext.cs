using Microsoft.EntityFrameworkCore;
using System;

namespace DBConfig
{
    /// <summary>
    /// 门店仓库对应数据库配置
    /// </summary>
    public class ShardConfigContext:DbContext
    {
        public ShardConfigContext(){ }

        public ShardConfigContext(DbContextOptions<ShardConfigContext> options) : base(options)
        { 
            
        }


        /// <summary>
        /// 门店配置信息
        /// </summary>
        public DbSet<ShardConfig> StoreConfigs { get; set; }
    }
}
