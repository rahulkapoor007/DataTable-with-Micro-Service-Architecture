using Customers.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Services.Abstraction.Models
{
    public class FilterRequestModel
    {
        [JsonProperty("filterColumn")]
        public string FilterColumn { get; set; }

        [JsonProperty("spName")]
        public string SpName { get; set; }

        [JsonProperty("searchFields")]
        public List<SearchFields> SearchFields { get; set; } = new List<SearchFields>();
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
}
