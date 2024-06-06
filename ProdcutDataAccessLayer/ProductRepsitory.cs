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

        public IEnumerable<ProductModel> Showall()
        {
            IEnumerable<ProductModel> result = Enumerable.Empty<ProductModel>();

            try
            {
                var showAllQuery = ($"exec List");
                DAL.Open();
                result = DAL.Query<ProductModel>(showAllQuery);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DAL.Close();
            }

            return result;
        }
        public void Put(Decimal Price, string Name)
        {
            try
            {

                if (Price != 0 && Name.Length != 0)
                {
                    var Gst = Price * 10 / 100;

                    var Update = ($"exec Put '{Name}',{Price},{Gst}");
                    DAL.Open();
                    DAL.Execute(Update);
                    DAL.Close();
                }
                else
                {

                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void Delete(string Name)
        {
            try
            {
                if (Name != null && Name.Length != 0)
                {
                    var Delete = ($"exec Spremove '{Name}'");
                    DAL.Open();
                    DAL.Execute(Delete);
                    DAL.Close();
                }

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public IEnumerable<ProductModel> GetbyId(int Id)
        {
            IEnumerable<ProductModel> result = Enumerable.Empty<ProductModel>();

            try
            {
                var showAllQuery = ($"exec Getmyid {Id}");
                DAL.Open();
                result = DAL.Query<ProductModel>(showAllQuery);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DAL.Close();
            }



            return result;

        }
    }
}
