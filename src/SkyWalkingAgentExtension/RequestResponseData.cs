using System;
using System.Collections.Generic;
using System.Text;

namespace SkyWalkingAgentExtension
{
    /// <summary>
    /// 请求响应数据
    /// </summary>
    public class RequestResponseData
    {
        /// <summary>
        /// 全局追踪Id
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 请求报文
        /// </summary>
        public string RequestInfo { get; set; }

        /// <summary>
        /// 响应报文
        /// </summary>
        public string ResponseInfo { get; set; }

        /// <summary>
        /// 响应码
        /// </summary>
        public int ResponseCode { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public long StartTicks { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public long StopTicks { get; set; }

        /// <summary>
        /// 消耗时间,毫秒
        /// </summary>
        public long TakeTicks { get; set; }
    }
}
