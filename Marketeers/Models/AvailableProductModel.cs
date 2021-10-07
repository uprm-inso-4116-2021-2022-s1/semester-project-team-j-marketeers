using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Models
{
    public class AvailableProductModel
    {
        public int Id { get; set; }
        public string Itemname { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public int Marketid { get; set; }
    }
}
