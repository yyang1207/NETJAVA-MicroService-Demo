using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceToken
{
    /// <summary>
    /// 客户端凭证请求对象
    /// </summary>
    public struct ClientCredentialsRequest
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string Secret { get; set; }


        /// <summary>
        /// 申请的scopes范围
        /// </summary>
        public string Scopes { get; set; }
    }
}
