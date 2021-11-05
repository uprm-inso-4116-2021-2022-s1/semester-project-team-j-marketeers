using Marketeers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marketeers.Services
{
    public class UserDAO
    {
        string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";

        //Customer Login
        public bool FindUserandPass(CustomerModel user)
        {
            bool successful = false;

            string sqlStatement = "SELECT * FROM customers WHERE customername = @username AND password = @password";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(sqlStatement, connection))
                {

                    command.Parameters.Add("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                    command.Parameters.Add("@password", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Password;

                    try
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        DataTable table = new DataTable();
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                            successful = true;
                            user.Id = Convert.ToInt32(table.Rows[0][0].ToString());
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //Close
                    connection.Close();

                }
            }
            return successful;
        }

        //Customer Register
        public bool CheckUsername(CustomerModel user)
        {
            bool successful = false;   
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                bool exists = false;

                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM customers WHERE customername = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                    exists = (int)(long)command.ExecuteScalar() > 0;
                }

                if (exists)
                {
                    //Already Exist The User
                    successful = false;
                }
                else
                {
                    //The Username does NOT Exist, Add Data
                    successful = true;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO customers (customername, password) VALUES (@username, @password)", connection))
                    {
                        cmd.Parameters.AddWithValue("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                        cmd.Parameters.AddWithValue("@password", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Password;
                        successful = true;

                        cmd.ExecuteNonQuery();
                    }

                }
                //Close
                connection.Close();
            }
            return successful;
        }

        //Driver
        public bool FindUserandPass(DriverModel user)
        {
            bool successful = false;

            string sqlStatement = "SELECT * FROM drivers WHERE drivername = @username AND password = @password";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(sqlStatement, connection))
                {

                    command.Parameters.Add("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                    command.Parameters.Add("@password", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Password;

                    try
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        DataTable table = new DataTable();
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                            successful = true;
                            user.Id = Convert.ToInt32(table.Rows[0][0].ToString());
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //Close
                    connection.Close();

                }
            }
            return successful;
        }

        //Driver
        public bool CheckUsername(DriverModel user)
        {
            bool successful = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                bool exists = false;

                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM drivers WHERE drivername = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                    exists = (int)(long)command.ExecuteScalar() > 0;
                }

                if (exists)
                {
                    //Already Exist The User
                    successful = false;
                }
                else
                {
                    //The Username does NOT Exist, Add Data
                    successful = true;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO drivers (drivername, password) VALUES (@username, @password)", connection))
                    {
                        cmd.Parameters.AddWithValue("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                        cmd.Parameters.AddWithValue("@password", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Password;
                        successful = true;

                        cmd.ExecuteNonQuery();
                    }

                }
                //Close
                connection.Close();
            }
            return successful;
        }

        //Market 
        public bool FindUserandPass(MarketModel user)
        {
            bool successful = false;

            string sqlStatement = "SELECT * FROM markets WHERE marketname = @username AND password = @password";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(sqlStatement, connection))
                {

                    command.Parameters.Add("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                    command.Parameters.Add("@password", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Password;

                    try
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        DataTable table = new DataTable();
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                            successful = true;
                            user.Id = Convert.ToInt32(table.Rows[0][0].ToString());
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //Close
                    connection.Close();

                }
            }
            return successful;
        }

        //Market 
        public bool CheckUsername(MarketModel user)
        {
            bool successful = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                bool exists = false;

                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM markets WHERE marketname = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                    exists = (int)(long)command.ExecuteScalar() > 0;
                }

                if (exists)
                {
                    //Already Exist The User
                    successful = false;
                }
                else
                {
                    //The Username does NOT Exist, Add Data
                    successful = true;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO markets (marketname, password) VALUES (@username, @password)", connection))
                    {
                        cmd.Parameters.AddWithValue("@username", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Username;
                        cmd.Parameters.AddWithValue("@password", (NpgsqlTypes.NpgsqlDbType)SqlDbType.VarChar, 40).Value = user.Password;
                        successful = true;

                        cmd.ExecuteNonQuery();
                    }

                }
                //Close
                connection.Close();
            }
            return successful;
        }
    }
}
