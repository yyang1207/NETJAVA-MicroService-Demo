using Microsoft.EntityFrameworkCore;
using System;

namespace DBConfig
{
    /// <summary>
    /// 门店仓库对应数据库配置
    /// </summary>
    public class CategoryShardConfigContext : DbContext
    {
        public CategoryShardConfigContext(){ }

        public CategoryShardConfigContext(DbContextOptions<CategoryShardConfigContext> options) : base(options)
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryShardConfig>().HasKey(x => new { x.CategoryId,x.Identityfier });
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 门店配置信息
        /// </summary>
        public DbSet<CategoryShardConfig> CategoryShardConfigs { get; set; }
    }
}
