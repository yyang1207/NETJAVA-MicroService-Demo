using ComponentsSelectTest.ServiceA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAController : ControllerBase
    {
        [HttpGet("b1")]
        public string BookSuccess()
        {
            OrderService1 svc = new OrderService1();
            svc.Order(new OrderInfo() 
            { 
                Id=1, 
                UserName="test", 
                Amount=100
            });

            return "Ok";
        }

        [HttpGet("b2")]
        public string BookFail()
        {
            OrderService2 svc = new OrderService2();
            svc.Order(new OrderInfo() 
            {
                Id = 2,
                UserName = "test",
                Amount = 101
            });
            return "Ok";
        }
    }
}
