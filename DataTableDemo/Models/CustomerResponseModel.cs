using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTableDemo.Models
{
    public class CustomerResponseModel
    {
        [JsonProperty("oacyList")]
        public List<CustomersModel> DataList { get; set; } = new List<CustomersModel>();

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; } = 0;

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 1;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;

        [JsonProperty("resultCount")]
        public int ResultCount { get; set; } = 0;
    }

}
