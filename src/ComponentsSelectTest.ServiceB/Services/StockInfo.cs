using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceB.Services
{
    public class StockInfo:BaseDomainEntity
    {
        public int ProductId { get; set; }

        public int Stock { get; set; }

        public int ReverseStock { get; set; }
    }
}
