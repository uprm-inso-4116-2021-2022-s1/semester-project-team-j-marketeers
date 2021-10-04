using Marketeers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Diagnostics;

namespace Marketeers.Services
{
    public class UserDAO
    {
        string connectionString = @"Server=ec2-34-234-12-149.compute-1.amazonaws.com;Database=dcotbsj3q6c5t4;Port=5432;sslmode=Require;Trust Server Certificate=true;User Id=misqawyzokbawh;Password=d40b0e9a9ee57c1ff241f9d69b354a39b68cd6c79bfbb9752cf9ec9bddcd0968";
        
        public bool FindUserandPass(UserModel user)
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
                    
                    //table.Load(myReader);
                    try
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        //DataTable table = reader.GetSchemaTable();

                        if (reader.HasRows)
                        {
                            //user.Id = (int)reader.GetValue(reader.GetOrdinal("customerid"));
                            successful = true;
                            //Trace.WriteLine(reader.GetValue(0).ToString());
                        }
                        reader.Close();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //Close
                    connection.Close();

                }
            }
            return successful;

        }
    }

    
}
