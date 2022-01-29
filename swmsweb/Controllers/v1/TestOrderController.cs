using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SkyWalkingAgentExtension;
using swmsweb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace swmsweb.Controllers.v1
{
    [ApiVersion("1.0")]

    [Route("api/v{api-version:apiversion}/[controller]")]
    [ApiController]
    public class TestOrderController : ControllerBase
    {
        private static long count = 0;
        private readonly ILogger<TestOrderController> _logger;


        public TestOrderController(ILogger<TestOrderController> logger) {
            _logger = logger;
        }
        [HttpPost,Route("UpdateTestOrder")]
        public async Task<HttpResult> UpdateTestOrder([FromBody]TestOrder testOrder) {

            _logger.LogInformation("V1.0 TestOrderController Logs", LogCode.Information);
            try
            {
                string orderId = testOrder.OrderID;
                string storeId = testOrder.StoreID;

                _logger.LogInformation("请求json"+JsonConvert.SerializeObject(testOrder), LogCode.Information);
                var service = new TestOrderService();

                var orderInfo = service.GetOrderInfo(orderId, storeId);

                if (orderInfo != null)
                {
                    if (orderInfo.Status == 1)
                    {
                        int rs = service.UpdateTestOrder(orderId, storeId, "v1.0");
                        if (rs > 0)
                        {
                            _logger.LogInformation("V1.0 TestOrderController Logs 数据更新成功", LogCode.Information);
                            return   HttpResult.successResult("成功！", new { count = 1, message = "数据更新成功" });

                        }
                        else
                        {
                            _logger.LogInformation("V1.0 TestOrderController Logs 数据更新失败", LogCode.Information);
                            return  HttpResult.failResult("失败！", new { count = -1, message = "更新出错，请重试" });
                        }
                    }
                    else
                    {
                        _logger.LogInformation("V1.0 TestOrderController Logs 该数据状态禁止重复更新", LogCode.Information);
                        return  HttpResult.successResult("成功！", new { count = 0, message = "该数据状态禁止重复更新" });
                    }
                }
                else
                {
                    _logger.LogInformation("V1.0 TestOrderController Logs 未找到数据", LogCode.Information);
                    return  HttpResult.failResult("失败！", new { count = 0, message = "未找到数据" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("V1.0 TestOrderController Logs 数据更新失败", LogCode.DataBase);
                return  HttpResult.failResult("失败", new { count = -1, message = ex.Message });
            }
        }

        [HttpPost, Route("GetTestOrderStatus")]
        public async Task<HttpResult> GetTestOrderStatus([FromBody] TestOrder testOrder) {
            _logger.LogInformation("V1.0 TestOrderController Logs", LogCode.Information);

            try
            {
                string orderId = testOrder.OrderID;
                string storeId = testOrder.StoreID;

                var orderInfo = new TestOrderService().GetOrderInfo(orderId, storeId);
                if (orderInfo != null)
                {
                    _logger.LogInformation("V1.0 TestOrderController Logs GetTestOrderStatus数据查询成功", LogCode.Information);
                    return HttpResult.successResult("成功！", new { count = 1, status = orderInfo.Status });
                }
                else {
                    _logger.LogInformation("V1.0 TestOrderController Logs GetTestOrderStatus数据查询成功", LogCode.Information);
                    return HttpResult.successResult("查询成功,，没有数据！", new { count = 0, status = 0 });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("V1.0 TestOrderController Logs GetTestOrderStatus数据查询失败", LogCode.DataBase);
                return HttpResult.failResult("失败", new { count = -1, message = ex.Message });
            }
        }

        [HttpPost, Route("GetTestOrderStatus1")]
        public async Task<HttpResult> GetTestOrderStatus1([FromBody] TestOrder testOrder)
        {
            Interlocked.Increment(ref count);
            long rep = count % 100000;
            if (rep >= 10001 && rep <= 10020) Thread.Sleep(4000);//线程沉睡6s


            _logger.LogInformation("V1.0 TestOrderController Logs", LogCode.Information);


            string orderId = testOrder.OrderID;
            string storeId = testOrder.StoreID;

            var orderInfo = new TestOrderService().GetOrderInfo(orderId, storeId);
            if (orderInfo != null)
            {
                _logger.LogInformation("V1.0 TestOrderController Logs GetTestOrderStatus数据查询成功", LogCode.Information);
                return HttpResult.successResult("成功！", new { count = 1, status = orderInfo.Status });
            }
            else
            {
                _logger.LogInformation("V1.0 TestOrderController Logs GetTestOrderStatus数据查询成功", LogCode.Information);
                return HttpResult.successResult("查询成功,，没有数据！", new { count = 0, status = 0 });
            }



        }
    }
}
