using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Services.Abstraction.Models
{
    public class CustomerSendingModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
