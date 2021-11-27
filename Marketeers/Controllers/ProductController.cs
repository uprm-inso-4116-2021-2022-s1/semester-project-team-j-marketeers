using Marketeers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Controllers
{
    public class ProductController : Controller
    {
        // Customer POV
        [Route("/[controller]/show")]
        [HttpGet]
        public ActionResult ProductAll()
        {
            string json = GetAllProducts();
            List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(json);
            TempData["product"] = products;
            return View("ShowProduct");
        }

        // GET: ProductController/1
        [Route("/[controller]/{marketid}")]
        [HttpGet]
        public ActionResult Product(int marketid)
        {
            string json = GetProductsFromMarket(marketid);
            List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(json);
            TempData["product"] = products;
            return View("ShowProduct");
        }

        //Market Method
        [Route("/[controller]/{marketid}/sell")]
        [HttpGet]
        public ActionResult ProductFromMarket(int marketid)
        {
            string json = GetProductsFromMarket(marketid) ;
            List<ProductModel> productsfrommarket = JsonConvert.DeserializeObject<List<ProductModel>>(json);
            TempData["productfrommarket"] = productsfrommarket;
            return View("ShowProductFromMarket");
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Back-End Method API
        [Route("/[controller]/all")]
        [HttpGet]
        public string GetAllProducts()
        {
            string query = @"SELECT productid, itemname, price, quantity FROM products";

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

        [Route("api/[controller]/{productid}")]
        [HttpGet]
        public string GetProductId(int productid)
        {
            string query = @"SELECT productid, itemname FROM products WHERE productid = @productid";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@productid", productid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return JsonConvert.SerializeObject(table);
        }

        [Route("api/[controller]/{marketid}/Product")]
        [HttpGet]
        public string GetProductsFromMarket(int marketid)
        {
            string query = @"SELECT * FROM products WHERE marketid = @marketid";

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
