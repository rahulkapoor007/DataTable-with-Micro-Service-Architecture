using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Customers.Repositories.Context.Entities
{
    public class CustomersModel
    {
        //[Key]
        //[JsonProperty("id")]
     //   public Int64 RowNum { get; set; }
        public int Id { get; set; }

       // [JsonProperty("name")]
        public string Name { get; set; }

       // [JsonProperty("address")]
        public string Address { get; set; }

      //  [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

       // [JsonProperty("emailId")]
        public string Email { get; set; }

       // [JsonProperty("count")]
        public int count { get; set; }
    }
}
