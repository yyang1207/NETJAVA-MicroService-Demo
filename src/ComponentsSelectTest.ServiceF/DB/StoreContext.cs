using DBConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceF.DB
{
    /// <summary>
    /// 门店仓库db
    /// </summary>
    public class StoreContext:DbContext
    {
        private readonly StoreChangeService _changeService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">组件配置信息</param>
        /// <param name="service"></param>
        public StoreContext(DbContextOptions<StoreContext> options, StoreChangeService service) : base(options)
        {
            _changeService = service;
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        public DbSet<StoreOrder> Orders { get; set; }

        /// <summary>
        /// 切换db
        /// </summary>
        /// <param name="storeId">门店编号</param>
        /// <returns>返回db上下文</returns>
        public StoreContext ChangeDB(string storeId)
        {
            _changeService.ChangeDB(storeId, this.Database.GetDbConnection());
            return this;
        }
    }
}
