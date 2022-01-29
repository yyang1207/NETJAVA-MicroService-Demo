using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentsSelectTest.ServiceA.DB
{
    [Table("tb_order")]
    public class Order
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 订单所有者
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
