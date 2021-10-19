using Customers.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Repositories.Abstraction.Models
{
    public class FilterModel
    {
        public string WhereFilters { get; set; }
        public string FilterColumn { get; set; }
        public string SpName { get; set; }
    }
}
