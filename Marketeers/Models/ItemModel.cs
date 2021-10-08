using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class OrderedItemModel
    {
        [JsonProperty("itemid")]
        public int Id { get; set; }
        [JsonProperty("orderid")]
        public int Orderid { get; set; }
        [JsonProperty("productid")]
        public int Productid { get; set; }
    }
}
