using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class OrderModel
    {
        [JsonProperty ("orderid")]
        public int Id { get; set; }
        [JsonProperty("customerid")]
        public int Customerid { get; set; }
        [JsonProperty("driverid")]
        public int Driverid { get; set; }
        [JsonProperty("marketid")]
        public int Marketid { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
