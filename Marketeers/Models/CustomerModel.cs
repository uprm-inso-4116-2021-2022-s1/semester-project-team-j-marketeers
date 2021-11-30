using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class CustomerModel
    {
        [JsonProperty("customerid")]
        public int Customerid { get; set; }
        [JsonProperty("customername")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
