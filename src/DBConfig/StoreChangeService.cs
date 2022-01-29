using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBConfig
{
    /// <summary>
    /// 门店基类
    /// </summary>
    public class StoreChangeService
    {
        private readonly ShardConfigContext _configDB;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">配置库上下文</param>
        public StoreChangeService(ShardConfigContext config)
        {
            _configDB = config;
        }

        public void ChangeDB(string storeId,IDbConnection dbConnection)
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
    }
}
