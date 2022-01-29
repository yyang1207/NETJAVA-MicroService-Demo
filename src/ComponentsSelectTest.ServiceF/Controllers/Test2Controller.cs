using ComponentsSelectTest.ServiceF.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test2Controller : ControllerBase
    {
        private readonly TestContext2 _dbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">db上下文</param>
        public Test2Controller(TestContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 返回指定门店的订单列表
        /// </summary>
        /// <param name="sid">门店编号</param>
        /// <returns>返回数据集合</returns>
        [HttpGet("{sid}")]
        public IEnumerable<StoreOrder> Get(string sid)
        {
            return _dbContext.ChangeDB(sid).Orders.Where(x => x.StoreId == sid).ToList();
        }
    }
}
