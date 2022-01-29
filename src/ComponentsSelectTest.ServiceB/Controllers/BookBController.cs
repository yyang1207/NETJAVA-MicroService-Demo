using ComponentsSelectTest.ServiceB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookBController : ControllerBase
    {
        [HttpGet("b1")]
        public string BookSuccess()
        {
            StockService1 svc = new StockService1();
            svc.Order(new StockInfo() { ProductId=1, Stock=100,ReverseStock=200});
            return "";
        }
    }
}
