using DataTableDemo.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTableDemo.Models
{
    public class CustomerDataRequestModel
    {
        [JsonProperty("searchFields")]
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();

        [JsonProperty("generalSearchField")]
        public string GeneralSearchField { get; set; }

        [JsonProperty("sortingField")]
        public SortingField SortingField { get; set; } = new SortingField();

        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; } = 1;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
    public class SearchFields
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("fieldValue")]
        public List<string> FieldValue { get; set; }

        [JsonProperty("operatorType")]
        public Operator OperatorType { get; set; }
    }

    public class SortingField
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }
    }
    
}
