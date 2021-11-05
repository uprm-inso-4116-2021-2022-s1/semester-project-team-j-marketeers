using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class ProductList
    {
        public List<ProductModel> productlist { get; set; }
    }
    public class ProductModel
    {
        [JsonProperty("productid")]
        public int Id { get; set; }
        [JsonProperty("itemname")]
        public string Itemname { get; set; }
        [JsonProperty("price")]
        public float price { get; set; }
        [JsonProperty("quantity")]
        public int quantity { get; set; }
        [JsonProperty("marketid")]
        public int Marketid { get; set; }
    }
}
