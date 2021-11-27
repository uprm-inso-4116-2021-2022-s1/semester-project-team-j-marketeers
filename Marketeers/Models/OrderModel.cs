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

        [JsonProperty("productid")]// Added
        public int Productid { get; set; }
        [JsonProperty("itemid")]// Added
        public int Itemid { get; set; }
        [JsonProperty("itemname")]// Added
        public string Itemname { get; set; }
        [JsonProperty("price")]// Added
        public float Price { get; set; }
        [JsonProperty("quantity")]// Added
        public int Quantity { get; set; }
        [JsonProperty("complete")]// Added
        public bool Complete { get; set; }
        [JsonProperty("ready")]// Added
        public bool Ready { get; set; }
    }
}
