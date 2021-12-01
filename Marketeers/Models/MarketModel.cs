using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class MarketModel
    {
        [JsonProperty("marketid")]
        public int Id { get; set; }
        [JsonProperty("marketname")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
