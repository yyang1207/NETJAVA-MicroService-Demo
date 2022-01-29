using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyWalkingAgentExtension
{
    /// <summary>
    /// 中间件扩展
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// 扩展函数
        /// </summary>
        /// <param name="app">应用构造器</param>
        /// <returns>返回构造器</returns>
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TraceIdMiddleware>()
                .UseMiddleware<RequestResponseLoggingMiddleware>();
        }

        /// <summary>
        /// 扩展函数
        /// </summary>
        /// <param name="app">构造器</param>
        /// <returns>返回构造器</returns>
        public static IApplicationBuilder UseTraceId(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TraceIdMiddleware>();
        }
    }
}
