using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avicom.CRM.Data.Enums
{
    public enum ContractStatus
    {
        [Description("test1")]
        NotYetConcluded,
        [Description("test2")]
        Concluded,
        [Description("test3")]
        Cancelled
    }
}
