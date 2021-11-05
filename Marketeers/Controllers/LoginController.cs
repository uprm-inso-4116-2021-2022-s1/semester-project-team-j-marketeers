using Marketeers.Models;
using Marketeers.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Marketeers.Controllers
{
    public class LoginController : Controller
    {
        //CustomerIndex
        [Route("/[controller]/CustomerIndex")]
        [HttpGet]
        public IActionResult CustomerIndex()
        {
            return View("CustomerLogin");
        }

        //DriverIndex
        [Route("/[controller]/DriverIndex")]
        [HttpGet]
        public IActionResult DriverIndex()
        {
            return View("DriverLogin");
        }

        //MarketIndex
        [Route("/[controller]/MarketIndex")]
        [HttpGet]
        public IActionResult MarketIndex()
        {
            return View("MarketLogin");
        }

        //Customer
        [Route("/[controller]/CustomerLogin")]
        [HttpPost]
        public IActionResult CustomerLogin(CustomerModel user)
        {
            SecurityService security = new SecurityService();
            if (security.IsValid(user))
            {
                //HttpContext.Session.SetString("username", user.ToString());
                return View("SuccessfulForCustomer", user);
            }
            else
            {
                return View("Failed", user);
            }
        }

        //Driver
        [Route("/[controller]/DriverLogin")]
        [HttpPost]
        public IActionResult DriverLogin(DriverModel user)
        {
            SecurityService security = new SecurityService();
            if (security.IsValid(user))
            {
                return View("SuccessfulForDriver", user);
            }
            else
            {
                return View("Failed", user);
            }
        }

        //Market
        [Route("/[controller]/MarketLogin")]
        [HttpPost]
        public IActionResult MarketLogin(MarketModel user)
        {
            SecurityService security = new SecurityService();
            if (security.IsValid(user))
            {
                return View("SuccessfulForMarket", user);
            }
            else
            {
                return View("Failed", user);
            }
        }

        //Logout
        [Route("[controller]/Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }


        string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

        [Route("{customerid}")]
        [HttpGet]
        public string GetCustomerId(int customerid)
        {
            NpgsqlDataReader reader;
            DataTable table = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(@"SELECT customerid, customername FROM customers WHERE customerid = @customerid", connection))
                {
                    command.Parameters.AddWithValue("@customerid", customerid);
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
                //Close
                connection.Close();
            }

            return JsonConvert.SerializeObject(table);
        }

    }
}
