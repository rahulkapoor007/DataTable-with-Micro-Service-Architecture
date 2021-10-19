using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Services.Abstraction.Models
{
    public class CustomerResponseModel
    {
        [JsonProperty("oacyList")]
        public IEnumerable<CustomerSendingModel> customerList { get; set; } //= new IEnumerable<CustomerSendingModel>();

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 1;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;

        [JsonProperty("resultCount")]
        public int ResultCount { get; set; } = 0;

    }
}
