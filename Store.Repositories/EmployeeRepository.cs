using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using DatabaseHelper;
using Store.Models;

namespace Store.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<Employee> LoadEmptyEmployees()
        {
            using (SqlDatabase database = new(_connectionString))
            {
                SqlDataReader reader = database.ExecuteReader(
                    Constants.ProcedureNames.EmptyEmployees,
                    CommandType.StoredProcedure
                );

                while (reader.Read())
                {
                    yield return new()
                    {
                        ID = reader["ID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    };
                }
                
            }
        }
    }
}
