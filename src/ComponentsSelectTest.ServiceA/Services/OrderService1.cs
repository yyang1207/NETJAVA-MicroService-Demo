using ComponentsSelectTest.ServiceA.DB;
using Servicecomb.Saga.Omega.Abstractions.Transaction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceA.Services
{
    public class OrderService1
    {
        private readonly ConcurrentDictionary<int, OrderInfo> _bookings = new ConcurrentDictionary<int, OrderInfo>();
        private readonly OrderDAL dal = new OrderDAL();

        [Compensable(nameof(CancelCar))]
        public void Order(OrderInfo domain)
        {
            domain.Confirm();
            _bookings.TryAdd(domain.Id, domain);

            dal.Add(new DB.Order() { Id = domain.Id, UserName = domain.UserName, Amount = domain.Amount });
        }

        void CancelCar(OrderInfo domain)
        {
            _bookings.TryGetValue(domain.Id, out var carBooking);
            carBooking?.Cancel();
            dal.PhysicalDelete(new DB.Order() { Id = domain.Id, UserName = domain.UserName, Amount = domain.Amount });
        }
    }
}
