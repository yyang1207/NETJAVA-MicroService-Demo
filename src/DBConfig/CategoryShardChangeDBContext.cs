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
    public abstract class CategoryShardChangeDBContext<T> : DbContext where T : DbContext
    {
        private readonly CategoryShardConfigContext _configDB;
        private readonly ShardKind _shardKind;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">组件配置信息</param>
        /// <param name="config">配置库db上下文</param>
        public CategoryShardChangeDBContext(DbContextOptions<T> options, CategoryShardConfigContext config,ShardKind shardKind) : base(options)
        {
            _configDB = config;
            _shardKind = shardKind;
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

        private void ChangeDB(string identityfier, IDbConnection dbConnection)
        {
            if (string.IsNullOrEmpty(identityfier)) throw new Exception($"标识符:{identityfier}不能为空");

            CategoryShardConfig config = _configDB.CategoryShardConfigs.Where(x => x.CategoryId==(int)_shardKind && x.Identityfier == identityfier).FirstOrDefault();
            if (config == null || string.IsNullOrEmpty(config.DBConnectionString)) throw new Exception($"标识符:{identityfier}对应数据库连接不能为空");

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
