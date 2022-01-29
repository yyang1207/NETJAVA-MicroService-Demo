using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConfig
{
    public abstract class BaseDBContext<T> : DbContext where T:DbContext
    {
        private readonly StoreChangeService _changeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">组件配置信息</param>
        /// <param name="service"></param>
        public BaseDBContext(DbContextOptions<T> options, StoreChangeService service) : base(options)
        {
            _changeService = service;
        }

        /// <summary>
        /// 切换db
        /// </summary>
        /// <param name="storeId">门店编号</param>
        /// <returns>返回db上下文</returns>
        public T ChangeDB(string storeId)
        {
            T context = GetDbContext();
            _changeService.ChangeDB(storeId, context.Database.GetDbConnection());
            return context;
        }

        protected abstract T GetDbContext();
    }
}
