using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class MarketList
    {
        public List<MarketModel> marketlist{ get; set; }
    }
    public class MarketModel
    {
        public MarketModel(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
        [JsonProperty("marketid")]
        public int Id { get; set; }
        [JsonProperty("marketname")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
