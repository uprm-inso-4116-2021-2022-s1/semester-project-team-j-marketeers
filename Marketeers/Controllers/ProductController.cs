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
        //Future: shopping cart maybe
        [Route("/[controller]/showall")]
        [HttpGet]
        public ActionResult ProductAll()
        {
            string json = GetAllProducts();
            List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(json);
            TempData["product"] = products;
            return View("ShowProduct");
        }

        // Customer POV
        [Route("/[controller]/showproduct")]
        [HttpGet]
        public ActionResult ProductFromCustomer(int marketid, int customerid)
        {
            string json = GetProductsFromMarket(marketid);
            List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(json);
            TempData["product"] = products;
            return View("ShowProduct");
        }

        // Market POV
        [Route("/[controller]/stock")]
        [HttpGet]
        public ActionResult ProductFromMarket(int marketid)
        {
            string json = GetProductsFromMarket(marketid) ;
            List<ProductModel> productsfrommarket = JsonConvert.DeserializeObject<List<ProductModel>>(json);
            TempData["productfrommarket"] = productsfrommarket;
            return View("ShowProductFromMarket");
        }

        [Route("/[controller]/SupermarketProducts")]
        [HttpGet]
        public IActionResult SupermarketProducts(int marketid)
        {
            return View("AddProduct");
        }

        // Modify Product
        [Route("/[controller]/restock")]
        [HttpGet]
        public IActionResult RestockProducts(ProductModel product)
        {
            AddProduct(product);
            return RedirectToAction("ProductFromMarket", "Product", new {marketid = product.Marketid});
        }

        [Route("/[controller]/delete")]
        [HttpGet]
        public ActionResult Delete(int productid, int marketid)
        {
            RemoveProduct(productid);
            return RedirectToAction("ProductFromMarket", "Product", new { marketid = marketid });
        }

        [Route("/[controller]/edit")]
        [HttpGet]
        public ActionResult Edit(int productid)
        {
            return View();
        }


        //Back-End Method API
        [Route("api/[controller]/all")]
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

        [Route("api/[controller]/add")]
        [HttpPost]
        public string AddProduct(ProductModel product)
        {
            string query = @"insert into products(marketid, itemname, price, quantity) values(@marketid, @itemname, @price, @quantity)";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@marketid", product.Marketid);
                    myCommand.Parameters.AddWithValue("@itemname", product.Itemname);
                    myCommand.Parameters.AddWithValue("@price", product.price);
                    myCommand.Parameters.AddWithValue("@quantity", product.quantity);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return JsonConvert.SerializeObject("Product is added");
        }

        [Route("api/[controller]/remove")]
        [HttpPost]
        public string RemoveProduct(int productid)
        {
            string query = @"delete from products where productid = @productid";

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
            return JsonConvert.SerializeObject("Product is removed");
        }

        [Route("api/[controller]/add")]
        [HttpPost]
        public string EditQuantity(int change, int productid)
        {
            string query = @"update products set quantity = quantity + @change where productid = @productid";

            DataTable table = new DataTable();
            string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(connectionString))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@productid", productid);
                    myCommand.Parameters.AddWithValue("@change", change);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return JsonConvert.SerializeObject("Quantity updated");
        }
    }
}
