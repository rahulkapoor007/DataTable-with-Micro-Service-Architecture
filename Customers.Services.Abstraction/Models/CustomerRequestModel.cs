using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Services.Abstraction.Models
{
    public class CustomerRequestModel
    {
        [JsonProperty("searchFields")]
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();

        [JsonProperty("generalSearchField")]
        public string GeneralSearchField { get; set; }

        [JsonProperty("sortingField")]
        public SortingField SortingField { get; set; }

        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; } = 1;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
        public class SortingField
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }
    }
}
