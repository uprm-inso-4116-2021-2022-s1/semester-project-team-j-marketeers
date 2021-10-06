using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int Customerid { get; set; }
        public int Driverid { get; set; }
        public int Marketid { get; set; }
    }
}
