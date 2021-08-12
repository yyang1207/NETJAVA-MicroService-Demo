using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceToken
{
    /// <summary>
    /// token对象
    /// </summary>
    public struct AccessTokenResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string AccessToken { get; set; }
    }
}
