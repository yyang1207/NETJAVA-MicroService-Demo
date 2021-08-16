using System;

namespace ComponentsSelect.ServiceToken
{
    /// <summary>
    /// 天气预报响应对象
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 摄氏度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 华氏度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 总结
        /// </summary>
        public string Summary { get; set; }
    }
}
