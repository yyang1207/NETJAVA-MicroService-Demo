using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.Caller
{
    public class AppSettings
    {
        public string version { get; set; } = "1.0";
        public string Str { get; set; }
        public int num { get; set; }

        public List<int> arr { get; set; }

        public SubObj subobj { get; set; }
    }

    public class SubObj
    {
        public string a { get; set; }
    }
}
