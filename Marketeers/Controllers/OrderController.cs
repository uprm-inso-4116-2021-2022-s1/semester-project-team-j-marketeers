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

namespace Marketeers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select orderid,
                       customerid
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

            return new JsonResult(table);
        }


        //[HttpPost]
        //public JsonResult Post(OrderModel order, UserModel user, MarketModel market)
        //{
        //    string query = @"
        //        insert into orders(customerid, marketid)
        //        values (@customerid, @marketid)
        //    ";

        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //    NpgsqlDataReader myReader;
        //    using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
        //        {
        //            myCommand.Parameters.AddWithValue("@customerid", user.Id);
        //            myCommand.Parameters.AddWithValue("@marketid", market.Id);
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader);

        //            myReader.Close();
        //            myCon.Close();

        //        }
        //    }

        //    return new JsonResult("Added Successfully");
        //}
    }
}
