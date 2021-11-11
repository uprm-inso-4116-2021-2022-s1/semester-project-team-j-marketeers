using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Marketeers.Models;
using Newtonsoft.Json;

namespace Marketeers.Controllers
{
    
    [ApiController]
    public class OrderController : Controller
    {

        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("/[controller]/showavailableorder")]
        [HttpGet]
        public ActionResult Order()
        {
            string json = GetAllOrdersFreeOrders();
            List<OrderModel> freeorder = JsonConvert.DeserializeObject<List<OrderModel>>(json);
            TempData["orders"] = freeorder;
            return View("ShowFreeOrders");
        }

        [Route("/[controller]/orderfromdriver")]
        [HttpGet]
        public ActionResult OrderFromDriver()
        {
            string json = GetAllOrdersFromDriver(4);
            List<OrderModel> orderfromdriver = JsonConvert.DeserializeObject<List<OrderModel>>(json);
            TempData["orderfromdriver"] = orderfromdriver;
            return View("OrderFromDriver");
        }

        [Route("/[controller]/{orderid}/take")]
        [HttpGet]
        public ActionResult HelperTakeOrder(int orderid)
        {
            TakeOrder(orderid, 4);
            return RedirectToAction("OrderFromDriver","Order");
        }

        [Route("/[controller]/{orderid}/complete")]
        [HttpGet]
        public ActionResult HelperCompleteOrder(int orderid)
        {
            CompleteOrderStatus(orderid);
            return RedirectToAction("OrderFromDriver", "Order");
        }


        [Route("api/[controller]/OrderSubmissionIndex")]
        [HttpGet]
        public IActionResult GetOrderSubmission()
        {
            return View("OrderSubmission");
        }

        [Route("/[controller]/OrderConfirmationIndex")]
        [HttpGet]
        public IActionResult GetOrderConfirmation()
        {
            return View("OrderConfirmation");
        }

        [Route("api/[controller]/all")]
        [HttpGet]
        public string GetAllOrders()
        {
            string query = @"
                select orderid,
                       customerid,
                       marketid
                from orders
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }

            return JsonConvert.SerializeObject(table);
        }

        [Route("api/[controller]/market/{marketid}")]
        [HttpGet]
        public string GetAllOrdersFromMarket(int marketid)
        {
            string query = @"
                select orderid,
                       customerid,
                       marketid
                from orders
                where marketid = @marketid
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@marketid", marketid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return JsonConvert.SerializeObject(table);
        }

        [Route("api/[controller]/customer/{customerid}")]
        [HttpGet]
        public string GetAllOrdersFromCustomer(int customerid)
        {
            string query = @"
                select *
                from orders
                where customerid = @customerid
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@customerid", customerid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return JsonConvert.SerializeObject(table);
        }

        [Route("api/[controller]/customer/{driverid}")]
        [HttpGet]
        public string GetAllOrdersFromDriver(int driverid)
        {
            string query = @"
                select *
                from orders
                where driverid = @driverid and completed = false
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@driverid", driverid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return JsonConvert.SerializeObject(table);
        }

        [Route("api/[controller]/free")]
        [HttpGet]
        public string GetAllOrdersFreeOrders()
        {
            string query = @"
                select orderid,
                       customerid,
                       marketid,
                       location
                from orders
                where completed = false and driverid is null
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return JsonConvert.SerializeObject(table);
        }

        [Route("api/[controller]/add")]
        [HttpPost]
        public string AddOrder(OrderModel order)
        {
            string query = @"
                insert into orders(customerid, marketid, location)
                values(@customerid, @marketid, @location)
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@customerid", order.Customerid);
                    myCommand.Parameters.AddWithValue("@marketid", order.Marketid);
                    myCommand.Parameters.AddWithValue("@location", order.Location);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return JsonConvert.SerializeObject("Order is added");
        }

        [Route("api/[controller]/add")]
        [HttpPost]
        public string AddItemToOrder(OrderedItemModel item)
        {
            string query = @"
                insert into items(productid, orderid)
                values(@productid, @orderid)
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@productid", item.Productid);
                    myCommand.Parameters.AddWithValue("@orderid", item.Orderid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return JsonConvert.SerializeObject("Order is added");
        }

        [Route("api/[controller]/take")]
        [HttpPut]
        public string TakeOrder(int orderid, int driverid)
        {
            string query = @"update orders set driverid = @driverid where orderid = @orderid";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@driverid", driverid);
                    myCommand.Parameters.AddWithValue("@orderid", orderid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return JsonConvert.SerializeObject("Order is complete");
        }

        [Route("api/[controller]/complete/{orderid}")]
        [HttpPut]
        public string CompleteOrderStatus(int orderid)
        {
            string query = @"
                update orders
                set completed = true
                where orderid = @orderid
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@orderid", orderid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return JsonConvert.SerializeObject("Order is complete");
        }
        
        [Route("api/[controller]/{orderid}/products")]
        [HttpGet]
        public string GetItemsFromOrder(int orderid)
        {
            string query = @"
                select *
                from items natural inner join products
                where orderid = @orderid
            ";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@orderid", orderid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return JsonConvert.SerializeObject(table);
        }
    }
}
