using System;
using System.ComponentModel;
using System.IO;

namespace swmsweb
{
    /// <summary>
    /// 日志
    /// </summary>
    /// <summary>
    /// 日志类型的枚举值
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 普通
        /// </summary> 
        [Description("普通")]
        Default = 0,
        /// <summary>
        /// 盘点
        /// </summary> 
        [Description("盘点")]
        Check = 10,

        /// <summary>
        /// 入库
        /// </summary> 
        [Description("入库")]
        InStorage = 20,
        /// <summary>
        /// 请求
        /// </summary> 
        [Description("请求")]
        Request = 30,
        /// <summary>
        /// 错误
        /// </summary> 
        [Description("错误")]
        Error = 40,
        /// <summary>
        /// 任务
        /// </summary> 
        [Description("任务")]
        Job = 50,
    }


    public class Log
    {
        private static string rootPath = MySqlHelper.LogPath;

        private static object logThreadObject = new object();
        /// <summary>
        ///  记录性能日志到文件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="log"></param>
        public static void WriteLogNew(LogType type, string log, string title = "", string refurl = "", string url = "", string ip = "", string opName = "")
        {

            lock (logThreadObject)
            {
                try
                {
                    var logType = "";
                    var datepath = rootPath + @"\" + DateTime.Today.ToString("yyyyMMdd") + @"\";
                    string path = "";
                    switch (type)
                    {
                        case LogType.Check:
                            logType = "pd";//盘点
                            datepath += logType;
                            path = datepath + @"\" + logType + ".txt";
                            break;
                        case LogType.InStorage:
                            logType = "rk"; //入库
                            datepath += logType;
                            path = datepath + @"\" + logType + ".txt";
                            break;
                        case LogType.Default:
                            logType = "log";
                            datepath += logType;
                            path = datepath + @"\" + logType + ".txt";
                            break;
                        case LogType.Request:
                            logType = "request";
                            datepath += logType;
                            path = datepath + @"\" + DateTime.Now.ToString("HH") + "." + logType + ".txt";
                            break;
                        case LogType.Job:
                            logType = "scheduler";
                            datepath += logType;
                            path = datepath + @"\" + logType + ".txt";
                            break;
                        case LogType.Error:
                            logType = "error";
                            datepath += logType;
                            path = datepath + @"\" + logType + ".txt";
                            break;
                    }
                    if (!System.IO.Directory.Exists(datepath))
                    {
                        System.IO.Directory.CreateDirectory(datepath);
                    }
                    if (!File.Exists(path))
                    {
                        File.Create(path).Close();
                    }
                    using (StreamWriter w = File.AppendText(path))
                    {
                        w.WriteLine("----------------------------------------------------------------------------------------------------");
                        if (!string.IsNullOrEmpty(ip))
                        {
                            w.WriteLine("【IP/MAC地址】-->{0}", ip);
                        }
                        if (!string.IsNullOrEmpty(opName))
                        {
                            w.WriteLine("【帐号(姓名)】-->{0}", opName);
                        }
                        w.WriteLine("【时      间】-->{0}-->{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), title);
                        if (!string.IsNullOrEmpty(refurl))
                        {
                            w.WriteLine("【前一页地址】-->{0}", refurl);
                        }
                        if (!string.IsNullOrEmpty(url))
                        {
                            w.WriteLine("【接口地址】-->{0}", url);
                        }
                        w.WriteLine();
                        w.WriteLine(log);
                        w.Flush();
                        w.Close();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        public static void WriteLogNew(LogType type, int log)
        {
            WriteLogNew(type, Convert.ToString(log));
        }
        public static void WriteLogNew(LogType type, Exception ex)
        {
            WriteLogNew(type, "", ex);
        }
        public static void WriteLogNew(LogType type, string log, Exception ex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            BuildExceptionMessage(sb, ex);
            WriteLogNew(type, log + sb.ToString());
        }
        /// <summary>
        /// 生成异常消息
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="ex"></param>
        private static void BuildExceptionMessage(System.Text.StringBuilder sb, Exception ex)
        {
            sb.Append(ex.Message)
             .AppendLine()
             .Append(ex.StackTrace).AppendLine();
            if (ex.InnerException != null)
            {
                BuildExceptionMessage(sb, ex.InnerException);
            }
        }
    }
}