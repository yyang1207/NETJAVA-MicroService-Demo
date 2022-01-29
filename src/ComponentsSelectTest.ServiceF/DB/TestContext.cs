using DBConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceF.DB
{
    public class TestContext : BaseDBContext<TestContext>
    {
        public TestContext(DbContextOptions<TestContext> options,StoreChangeService service) : base(options,service)
        { }

        protected override TestContext GetDbContext()
        {
            return this;
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        public DbSet<StoreOrder> Orders { get; set; }
    }
}
