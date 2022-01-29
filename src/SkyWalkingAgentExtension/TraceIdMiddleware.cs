using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkyWalkingAgentExtension
{
    /// <summary>
    /// TraceId中间件
    /// </summary>
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">下一个中间件</param>
        /// <param name="configuration">配置信息</param>
        public TraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 中间件执行函数
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns>返回操作结果</returns>
        public async Task Invoke(HttpContext context)
        {
            string traceid = "";

            //获取traceid
            HttpRequest request = context.Request;
            string skywalkingVersion = request.Headers["sw8"];
            if (!string.IsNullOrEmpty(skywalkingVersion))
            {
                traceid = GetTraceId(skywalkingVersion);
            }

            //traceid加入header
            request.Headers["TraceId"] = traceid;

            await _next(context);
        }


        private string GetTraceId(string content)
        {
            if (string.IsNullOrEmpty(content)) return "";

            var parts = content.Split('-');
            if (parts.Length < 8) return "";

            if (!int.TryParse(parts[0], out var sampled)) return "";

            string traceId = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1]));

            return traceId;
        }
    }
}
