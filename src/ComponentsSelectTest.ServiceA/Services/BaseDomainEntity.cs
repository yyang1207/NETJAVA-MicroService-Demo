using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentsSelectTest.ServiceA.Services
{
    public class BaseDomainEntity
    {
        public bool Confirmed { get; set; }
        public bool Cancelled { get; set; }

        public void Confirm()
        {
            Confirmed = true;
            Cancelled = false;
        }

        public void Cancel()
        {
            Confirmed = false;
            Cancelled = true;
        }
    }
}
