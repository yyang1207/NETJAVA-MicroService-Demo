using DBConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceF.DB
{
    public class TestContext2 : ShardChangeDBContext<TestContext2>
    {
        public TestContext2(DbContextOptions<TestContext2> options,ShardConfigContext config) : base(options,config)
        { }

        protected override TestContext2 GetDbContext()
        {
            return this;
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        public DbSet<StoreOrder> Orders { get; set; }
    }
}
