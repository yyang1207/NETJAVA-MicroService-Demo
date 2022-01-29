using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBConfig
{
    /// <summary>
    /// 分库db上下文
    /// </summary>
    /// <typeparam name="T">子类db上下文</typeparam>
    public abstract class ShardChangeDBContext<T> : DbContext where T : DbContext
    {
        private readonly ShardConfigContext _configDB;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">组件配置信息</param>
        /// <param name="config">配置库db上下文</param>
        public ShardChangeDBContext(DbContextOptions<T> options, ShardConfigContext config) : base(options)
        {
            _configDB = config;
        }

        /// <summary>
        /// 切换db
        /// </summary>
        /// <param name="identityfier">db分库表示符</param>
        /// <returns>返回db上下文</returns>
        public T ChangeDB(string identityfier)
        {
            T context = GetDbContext();
            ChangeDB(identityfier, context.Database.GetDbConnection());
            return context;
        }

        private void ChangeDB(string storeId, IDbConnection dbConnection)
        {
            if (string.IsNullOrEmpty(storeId)) return;

            ShardConfig config = _configDB.StoreConfigs.Where(x => x.StoreId == storeId).FirstOrDefault();
            if (config == null || string.IsNullOrEmpty(config.DBConnectionString)) throw new Exception($"门店编号{storeId}对应数据库连接不能为空");

            if (dbConnection.State.HasFlag(ConnectionState.Open))
            {
                //连接未关闭的时候的切换方式
                dbConnection.ChangeDatabase(config.DBConnectionString);
            }
            else
            {
                dbConnection.ConnectionString = config.DBConnectionString;
            }
        }

        /// <summary>
        /// 获取当前db上下文
        /// </summary>
        /// <returns>返回子类db上下文对象</returns>
        protected abstract T GetDbContext();
    }
}
