using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SkyWalkingAgentExtension
{
    /// <summary>
    /// http请求响应日志中间件
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">下一个中间件</param>
        /// <param name="logger">日志组件</param>
        public RequestResponseLoggingMiddleware(RequestDelegate next,ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="context">http上下文对象</param>
        /// <returns>返回操作结果</returns>
        public async Task Invoke(HttpContext context)
        {
            long startTick = DateTimeOffset.Now.Ticks;

            HttpRequest request = context.Request;
            string reqBody =await GetRequestBody(request);

            //读取response
            string respBody = "";
            int respCode = 0;

            try
            {
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await _next(context);
                    respBody = await GetResponse(context.Response);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 509;
                respBody = ex.Message;
            }

            respCode = context.Response.StatusCode;

            RequestResponseData data = new RequestResponseData();
            data.Host = request.Host.Value;
            data.Path = request.Path.Value;
            data.Method = request.Method;
            data.TraceId = request.Headers["TraceId"];
            data.RequestInfo = reqBody;
            data.ResponseInfo = respBody;
            data.ResponseCode = respCode;

            long stopTick = DateTimeOffset.Now.Ticks;

            data.StartTicks = startTick;
            data.StopTicks = stopTick;
            data.TakeTicks = (stopTick - startTick) / 10000;

            Console.WriteLine($"TraceId:{data.TraceId}");

            //写入日志
            _logger.LogRequest(data);
        }


        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            var strBody = "";
            if (request.Method.ToLower().Equals("post"))
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                strBody = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);
            }

            return strBody;
        }

        private async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
