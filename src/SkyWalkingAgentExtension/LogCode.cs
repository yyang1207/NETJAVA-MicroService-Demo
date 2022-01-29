using System;
using System.Collections.Generic;
using System.Text;

namespace SkyWalkingAgentExtension
{
    /// <summary>
    /// 日志分级编号
    /// 100~149为跟踪信息
    /// 150~199为调试信息
    /// 2xx为普通信息
    /// 3xx为警告信息，程序可以正常运行，比如某些参数的缺失
    /// 4xx为错误信息，一般为依赖的远程组件调用错误（例如：超时、依赖外部输入的授权失败、偶尔出现的数据语法错误等），且不影响应用程序正常运行，伴随第三方组件或者网络修复后程序可以正常
    /// 5xx为严重错误，影响程序正常运行（例如：数据库连接字符串配置错误等），需要修改项目中代码/配置文件重新发布版本的情况
    /// </summary>
    public enum LogCode
    {
        /// <summary>
        /// 跟踪信息
        /// </summary>
        Trace = 100,

        /// <summary>
        /// 调试信息
        /// </summary>
        Debug = 150,

        /// <summary>
        /// 普通信息
        /// </summary>
        Information = 200,

        /// <summary>
        /// 警告信息
        /// </summary>
        Warning = 300,

        /// <summary>
        /// 一般性错误
        /// </summary>
        Error = 400,

        /// <summary>
        /// 授权失败
        /// </summary>
        AuthorizeFail = 401,

        /// <summary>
        /// 连接被拒绝
        /// </summary>
        ConnectionRefused = 402,

        /// <summary>
        /// 请求超时
        /// </summary>
        TimeOut = 403,

        /// <summary>
        /// 访问资源不存在
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 语法错误，包括sql，redis等
        /// </summary>
        Syntax = 405,

        /// <summary>
        /// json转换失败
        /// </summary>
        JsonConvert = 406,

        /// <summary>
        /// 数据库类错误
        /// </summary>
        DataBase = 410,

        /// <summary>
        /// RabbitMq类错误
        /// </summary>
        RabbitMQ = 420,

        /// <summary>
        /// Redis类错误
        /// </summary>
        Redis = 430,

        /// <summary>
        /// Nacos类错误
        /// </summary>
        Nacos = 440,

        /// <summary>
        /// Kafka类错误
        /// </summary>
        Kafka = 450,

        /// <summary>
        /// ClickHouse类操作
        /// </summary>
        ClickHouse = 460,

        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 500,

        /// <summary>
        /// 第三方组件连接字符串null或者空
        /// </summary>
        ConnectionStringNullOrEmpty = 501,

        /// <summary>
        /// 连接字符串/配置文件中的授权信息无效，包括数据库、redis、mq、nacos等
        /// </summary>
        ConnectionStringAuthorizeFail = 502
    }
}
