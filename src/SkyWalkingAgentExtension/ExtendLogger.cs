using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyWalkingAgentExtension
{
    /// <summary>
    /// 扩展日志
    /// </summary>
    /// <typeparam name="T">日志类</typeparam>
    public class ExtendLogger<T>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志组件</param>
        public ExtendLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// trace
        /// </summary>
        /// <param name="traceData">输入输出内容及执行时间</param>
        /// <param name="logger">日志组件</param>
        public void LogTraceRequest(RequestResponseData traceData)
        {
            _logger.LogInformation("{Host}-{Path}-{Method}-{TraceId}-{RequestInfo}-{ResponseInfo}-{StartTicks}-{StopTicks}-{Code}",
                traceData.Host,
                traceData.Path,
                traceData.Method,
                traceData.TraceId,
                traceData.RequestInfo.Replace("{", "{{").Replace("}", "}}"),
                traceData.ResponseInfo.Replace("{", "{{").Replace("}", "}}"),
                traceData.StartTicks,
                traceData.StopTicks,                
                (int)LogCode.Information);
        }

        /// <summary>
        /// trace
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        public void LogTrace(string msg, LogCode code = LogCode.Trace)
        {
            int num = (int)code;
            if (num < 100 || num > 149) throw new ArgumentException("Trace级别的code范围是100~149。");

            _logger.LogTrace(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// debug
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        public void LogDebug(string msg, LogCode code = LogCode.Debug)
        {
            int num = (int)code;
            if (num < 150 || num > 199) throw new ArgumentException("Debug级别的code范围是150~199。");

            _logger.LogDebug(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// info
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        public void LogInformation(string msg, LogCode code = LogCode.Information)
        {
            int num = (int)code;
            if (num < 200 || num > 299) throw new ArgumentException("Information级别的code范围是200~299。");

            _logger.LogInformation(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        public void LogWarning(string msg, LogCode code = LogCode.Warning)
        {
            int num = (int)code;
            if (num < 300 || num > 399) throw new ArgumentException("Warning级别的code范围是100~149。");

            _logger.LogWarning(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// error
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        public void LogError(string msg, LogCode code = LogCode.Error)
        {
            int num = (int)code;
            if (num < 400 || num > 499) throw new ArgumentException("Error级别的code范围是400~499。");

            _logger.LogError(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        public void LogFatal(string msg, LogCode code = LogCode.Fatal)
        {
            int num = (int)code;
            if (num < 500 || num > 599) throw new ArgumentException("Fatal级别的code范围是500~599。");

            _logger.LogCritical(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }
    }


    /// <summary>
    /// ILogger扩展
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// trace
        /// </summary>
        /// <param name="traceData">输入输出内容及执行时间</param>
        /// <param name="logger">日志组件</param>
        internal static void LogRequest(this ILogger logger, RequestResponseData traceData)
        {
            logger.LogInformation("{Host}-{Path}-{Method}-{TraceId}-{RequestInfo}-{ResponseInfo}-{StartTicks}-{StopTicks}-{Code}-{ResponseCode}-{TakeTicks}",
                traceData.Host,
                traceData.Path,
                traceData.Method,
                traceData.TraceId,
                traceData.RequestInfo.Replace("{", "{{").Replace("}", "}}"),
                traceData.ResponseInfo.Replace("{", "{{").Replace("}", "}}"),
                traceData.StartTicks,
                traceData.StopTicks,
                (int)LogCode.Information,
                traceData.ResponseCode,
                traceData.TakeTicks);
        }

        /// <summary>
        /// trace
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        /// <param name="logger">日志组件</param>
        public static void LogTrace(this ILogger logger, string msg, LogCode code = LogCode.Trace)
        {
            int num = (int)code;
            if (num < 100 || num > 149) throw new ArgumentException("Trace级别的code范围是100~149。");

            logger.LogTrace(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// debug
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        /// <param name="logger">日志组件</param>
        public static void LogDebug(this ILogger logger, string msg, LogCode code = LogCode.Debug)
        {
            int num = (int)code;
            if (num < 150 || num > 199) throw new ArgumentException("Debug级别的code范围是150~199。");

            logger.LogDebug(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// info
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        /// <param name="logger">日志组件</param>
        public static void LogInformation(this ILogger logger, string msg, LogCode code = LogCode.Information)
        {
            int num = (int)code;
            if (num < 200 || num > 299) throw new ArgumentException("Information级别的code范围是200~299。");

            logger.LogInformation(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        /// <param name="logger">日志组件</param>
        public static void LogWarning(this ILogger logger, string msg, LogCode code = LogCode.Warning)
        {
            int num = (int)code;
            if (num < 300 || num > 399) throw new ArgumentException("Warning级别的code范围是100~149。");

            logger.LogWarning(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// error
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        /// <param name="logger">日志组件</param>
        public static void LogError(this ILogger logger, string msg, LogCode code = LogCode.Error)
        {
            int num = (int)code;
            if (num < 400 || num > 499) throw new ArgumentException("Error级别的code范围是400~499。");

            logger.LogError(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="code">日志code等级</param>
        /// <param name="logger">日志组件</param>
        public static void LogFatal(this ILogger logger, string msg, LogCode code = LogCode.Fatal)
        {
            int num = (int)code;
            if (num < 500 || num > 599) throw new ArgumentException("Fatal级别的code范围是500~599。");

            logger.LogCritical(msg.Replace("{", "{{").Replace("}", "}}") + "-{Code}", (int)code);
        }
    }
}
