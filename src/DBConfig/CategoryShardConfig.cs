using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DBConfig
{
    [Table("tb_shardconfig")]
    public class CategoryShardConfig
    {
        public int CategoryId { get; set; }

        public string Identityfier { get; set; }

        public string DBConnectionString { get; set; }
    }
}
