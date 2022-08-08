using System;
using Store.Models;
using Microsoft.Extensions.Configuration;
using DatabaseHelper;
using System.Data.SqlClient;
using System.Data;

namespace Store.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Category GetProductCategory(object productId)
        {
            using (SqlDatabase database = new(_connectionString))
            {
                SqlDataReader reader = database.ExecuteReader(
                    Constants.ProcedureNames.ProductCategory,
                    CommandType.StoredProcedure,
                    new SqlParameter("@ProductID", productId) { SqlDbType = SqlDbType.Int }
                );

                if (reader.Read())
                {
                    return new()
                    {
                        ID = reader["ID"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString(),
                        ParentID = reader["ParentID"].ToString()
                    };
                }
                else return default;
            }
        }
    }
}