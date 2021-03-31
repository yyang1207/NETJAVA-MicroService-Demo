using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.Caller
{
    public class NacosDiscoveryConfig
    {
        //
        // 摘要:
        //     nacos server addresses.
        public List<string> ServerAddresses { get; set; }
        //
        // 摘要:
        //     EndPoint
        public string EndPoint { get; set; }
        public string ContextPath { get; set; }

        //
        // 摘要:
        //     default timeout, unit is Milliseconds.
        public int DefaultTimeOut { get; set; }
        //
        // 摘要:
        //     default namespace
        public string Namespace { get; set; }
        //
        // 摘要:
        //     accessKey
        public string AccessKey { get; set; }
        //
        // 摘要:
        //     secretKey
        public string SecretKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //
        // 摘要:
        //     listen interval, unit is millisecond.
        public int ListenInterval { get; set; }
        public bool ConfigUseRpc { get; set; }
        public bool NamingUseRpc { get; set; }
        public string NamingLoadCacheAtStart { get; set; }
    }
}
