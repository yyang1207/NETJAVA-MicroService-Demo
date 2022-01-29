namespace swmsweb.Controllers.v2
{
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

    [ApiVersion("2.0")]

    [Route("api/v{api-version:apiversion}/[controller]")]
    [ApiController]
    public class TestOrderController : ControllerBase
    {

        private readonly ILogger<TestOrderController> _logger;


        public TestOrderController(ILogger<TestOrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("UpdateTestOrder")]
        public async Task<HttpResult> UpdateTestOrder([FromBody] TestOrder testOrder)
        {

            _logger.LogInformation("V2.0 TestOrderController Logs", LogCode.Information);

            try
            {
                string orderId = testOrder.OrderID;
                string storeId = testOrder.StoreID;

                var service=new TestOrderService();

                var orderInfo=service.GetOrderInfo(orderId, storeId);

                if (orderInfo != null)
                {
                    if (orderInfo.Status == 1)
                    {
                        int rs = service.UpdateTestOrder(orderId, storeId, "v2.0");
                        if (rs > 0)
                        {
                            _logger.LogInformation("V2.0 TestOrderController Logs 数据更新成功", LogCode.Information);
                            return HttpResult.successResult("成功！", new { count = 1, message = "数据更新成功" });
                        }
                        else
                        {
                            _logger.LogInformation("V2.0 TestOrderController Logs 数据更新失败", LogCode.Information);
                            return HttpResult.failResult("失败！", new { count = -1, message = "更新出错，请重试" });
                        }
                    }
                    else
                    {
                        _logger.LogInformation("V2.0 TestOrderController Logs 该数据状态禁止重复更新", LogCode.Information);
                        return HttpResult.successResult("成功！", new { count = 0, message = "该数据状态禁止重复更新" });
                    }
                }
                else
                {
                    _logger.LogInformation("V2.0 TestOrderController Logs 数据更新失败,未找到数据", LogCode.Information);
                    return HttpResult.failResult("失败！", new { count = 0, message = "未找到数据" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("V2.0 TestOrderController Logs 数据更新失败", LogCode.DataBase);
                return HttpResult.failResult("失败", new { count = -1, message = ex.Message });
            }
            
        }
    }
}
