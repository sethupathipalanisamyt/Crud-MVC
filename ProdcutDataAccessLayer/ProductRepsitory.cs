using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Crud_MVC.Models;

namespace ProdcutDataAccessLayer
{
    public class ProductRepsitory
    {
        SqlConnection DAL;
        IConfiguration _configuration;
        public ProductRepsitory(IConfiguration configuration)
        {
            _configuration = configuration;
            var a = _configuration.GetConnectionString("DbConnection");
            DAL= new SqlConnection(a);
        }
        public void Insert(ProductModel product)
        {
            try
            {
                var gst = product.Price * 10 / 100;
                product.Gst = gst;

                var Insertsql = ($"exec InsertProduct'{product.Name}',{product.Price},{product.Gst},{product.Weight},'{product.Description}'");

                DAL.Open();
                DAL.Execute(Insertsql);
                DAL.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not insert {ex.Message}");
            }


        }
    }
}
