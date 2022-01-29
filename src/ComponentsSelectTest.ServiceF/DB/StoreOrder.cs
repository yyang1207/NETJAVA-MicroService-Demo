using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentsSelectTest.ServiceF.DB
{
    [Table("tb_order")]
    public class StoreOrder
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        public string OrderNo { get; set; }

        /// <summary>
        /// 门店标号
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
