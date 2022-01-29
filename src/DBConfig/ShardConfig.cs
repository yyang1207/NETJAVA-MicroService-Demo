using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DBConfig
{
   [Table("tb_config")]
    /// <summary>
    /// 门店编号对应数据库连接
    /// </summary>
    public class ShardConfig
    {
        [Key]
        /// <summary>
        /// 门店编号
        /// </summary>
        public string StoreId { get; set; }


        /// <summary>
        /// 数据库连接
        /// </summary>
        public string DBConnectionString { get; set; }
    }
}
