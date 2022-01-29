using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceA.Services
{
    public class OrderInfo: BaseDomainEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public decimal Amount { get; set; }
    }
}
