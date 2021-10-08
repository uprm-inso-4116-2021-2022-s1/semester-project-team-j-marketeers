using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marketeers.Models
{
    public class DriverModel
    {
        [JsonProperty("driverid")]
        public int Id { get; set; }
        [JsonProperty("drivername")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
