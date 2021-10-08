using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class AvailableProductModel
    {
        [JsonProperty("productid")]
        public int Id { get; set; }
        [JsonProperty("productname")]
        public string Itemname { get; set; }
        [JsonProperty("price")]
        public float price { get; set; }
        [JsonProperty("quantity")]
        public int quantity { get; set; }
        [JsonProperty("marketid")]
        public int Marketid { get; set; }
    }
}
