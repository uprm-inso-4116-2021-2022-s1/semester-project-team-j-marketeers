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
using Marketeers.Services;
using Newtonsoft.Json;

namespace Marketeers.Controllers
{
    [ApiController]
    public class MarketController : Controller
    {
        [Route("api/[controller]/showall")]
        [HttpGet]
        public ActionResult Market()
        {
            List<MarketModel> market = new List<MarketModel>();
            market = JsonConvert.DeserializeObject<List<MarketModel>>(GetAllMarkets().ToString());
            TempData["market"] = market;
            return View("ShowMarket");
        }


        private readonly IConfiguration _configuration;
        public MarketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/[controller]/all")]
        [HttpGet]
        public DataTable GetAllMarkets()
        {
            string query = @"select marketid, marketname from markets";

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
            return table;
        }

        [Route("api/[controller]/{marketid}")]
        [HttpGet]
        public JsonResult GetMarketByID(int marketid)
        {
            string query = @"select marketid, marketname from markets where marketid = @marketid";

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

            return new JsonResult(table);
        }

        [Route("api/[controller]/{marketid}/products")]
        [HttpGet]
        public JsonResult GetProductsFromMarket(int marketid)
        {
            string query = @"
                select *
                from products
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

            return new JsonResult(table);
        }
    }
}
