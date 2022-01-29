using ComponentsSelectTest.ServiceB.DB;
using Servicecomb.Saga.Omega.Abstractions.Transaction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceB.Services
{
    public class StockService1
    {
        private readonly ConcurrentDictionary<int, StockInfo> _bookings = new ConcurrentDictionary<int, StockInfo>();
        private readonly StockDAL dal = new StockDAL();

        [Compensable(nameof(CancelOrder))]
        public void Order(StockInfo domain)
        {
            domain.Confirm();
            _bookings.TryAdd(domain.ProductId, domain);

            dal.Add(new DB.StockTable() { ProductId = domain.ProductId, Stock = domain.Stock, ReverseStock = domain.ReverseStock });
        }

        void CancelOrder(StockInfo domain)
        {
            _bookings.TryGetValue(domain.ProductId, out var carBooking);
            carBooking?.Cancel();
            dal.PhysicalDelete(new DB.StockTable() { ProductId = domain.ProductId, Stock = domain.Stock, ReverseStock = domain.ReverseStock });
        }
    }
}
