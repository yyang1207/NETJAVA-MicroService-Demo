using DBConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceF.DB
{
    public class TestContext3 : CategoryShardChangeDBContext<TestContext3>
    {
        public TestContext3(DbContextOptions<TestContext3> options, CategoryShardConfigContext config) : base(options, config, ShardKind.Store)
        {
        }

        protected override TestContext3 GetDbContext()
        {
            return this;
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        public DbSet<StoreOrder> Orders { get; set; }
    }
}
