using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTableDemo.Models
{
    public class CustomerFilterRequestModel
    {
        [JsonProperty("filterColumn")]
        public string FilterColumn { get; set; }

        [JsonProperty("spName")]
        public string SpName { get; set; }

        [JsonProperty("searchFields")]
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();
    }
}
