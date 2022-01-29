using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceB.DB
{
    [Table("tb_stock")]
    public class StockTable
    {
        [Key]
        public int ProductId { get; set; }

        public int Stock { get; set; }

        public int ReverseStock { get; set; }
    }
}
