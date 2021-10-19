using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DataTableDemo.Enums
{
    public enum Operator
    {
        [Description("=")]
        NotSpecified = 0,
        [Description("=")]
        EQUAL = 1,
        [Description("IN")]
        IN = 2,
        [Description("LIKE")]
        LIKE = 3,
        [Description(">")]
        GT = 4,
        [Description("<")]
        LT = 5,
        [Description(">=")]
        GTE = 6,
        [Description("<=")]
        LTE = 7
    }
}
