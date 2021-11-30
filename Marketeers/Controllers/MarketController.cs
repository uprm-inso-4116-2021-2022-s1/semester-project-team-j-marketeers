using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
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
using System.Text.Json.Serialization;
using Newtonsoft.Json.Serialization;

namespace Marketeers.Controllers
{
    [ApiController]
    public class MarketController : Controller
    {
        //Market POV
        [Route("/[controller]/SupermarketInformation")]
        [HttpGet]
        public IActionResult SupermarketInfo()
        {
            return View("SupermarketInfo");
        }

        //Customer POV
        [Route("/[controller]/showmarket")]
        [HttpGet]
        public ActionResult ShowMarket()
        {
            string json = GetAllMarkets();
            List<MarketModel> markets = JsonConvert.DeserializeObject<List<MarketModel>>(json);
            TempData["market"] = markets;
            return View("ShowMarket");
        }

        //Do Not Remember For What This Is For :C
        [Route("/[controller]/{marketid}")]
        [HttpGet]
        public ActionResult MarketToProduct(int marketid)
        {
            return RedirectToAction(marketid.ToString(), "Product");
        }

        //Back-End Method API
        [Route("api/[controller]/all")]
        [HttpGet]
        public string GetAllMarkets()
        {
            string query = @"SELECT marketid, marketname FROM markets";

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

        [Route("api/[controller]/{marketid}")]
        [HttpGet]
        public string GetMarketId(int marketid)
        {
            string query = @"SELECT marketid, marketname FROM markets WHERE marketid = @marketid";

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
    }
}
