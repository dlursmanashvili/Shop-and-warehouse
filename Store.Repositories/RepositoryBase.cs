using System;
using DatabaseHelper;
using System.Collections.Generic;
using System.Reflection;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Store.Repositories
{
    public abstract class RepositoryBase<T> where T : new()
    {
        protected readonly string _connectionString;

        public RepositoryBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Transaction

        public virtual void BeginTransaction()
        {
            using SqlDatabase database = new(_connectionString, true);
            database.BeginTransaction();
        }

        public virtual void CommitTransaction()
        {
            using SqlDatabase database = new(_connectionString, true);
            database.BeginTransaction();
        }

        public virtual void RollbackTransaction()
        {
            using SqlDatabase database = new(_connectionString, true);
            database.RollbackTransaction();
        }

        #endregion

        public virtual T Get(object id)
        {
            using (SqlDatabase database = new(_connectionString))
            {
                SqlDataReader reader = database.ExecuteReader(
                    ProcedurePatterns<T>.GetProcedure,
                    CommandType.StoredProcedure,
                    new SqlParameter { ParameterName = "@ID", Value = id }
                );

                if (reader.Read())
                {
                    T item = new();
                    foreach (var property in GetProperties())
                    {
                        if (property.Name == "Password") continue;

                        if (reader[$"{property.Name}"] == DBNull.Value)
                            property.SetValue(item, null);
                        else
                            property.SetValue(item, reader[$"{property.Name}"]);
                    }
                    return item;
                }
                else
                {
                    return default;
                }
            }
        }

        public virtual IEnumerable<T> Load()
        {
            using (SqlDatabase database = new(_connectionString))
            {
                SqlDataReader reader = database.ExecuteReader(
                    ProcedurePatterns<T>.SelectProcedure,
                    CommandType.StoredProcedure
                );

                while (reader.Read())
                {
                    T item = new();
                    foreach (var property in GetProperties())
                    {
                        if (property.Name == "Password") continue;

                        if (reader[$"{property.Name}"] == DBNull.Value)
                            property.SetValue(item, null);
                        else
                            property.SetValue(item, reader[$"{property.Name}"]);
                    }
                    yield return item;
                }
            }
        }

        public virtual object Insert(T item)
        {
            using SqlDatabase database = new(_connectionString);
            object id = database.ExecuteScalar<object>(
                ProcedurePatterns<T>.InsertProcedure,
                CommandType.StoredProcedure,
                GetParameters(item, ProcedurePatterns<T>.InsertProcedure).ToArray()
            );

            return id;
        }

        public virtual void Update(T item)
        {
            using SqlDatabase database = new(_connectionString);
            database.ExecuteNonQuery(
                ProcedurePatterns<T>.UpdateProcedure,
                CommandType.StoredProcedure,
                GetParameters(item, ProcedurePatterns<T>.UpdateProcedure).ToArray()
            );
        }

        public virtual void Delete(object id)
        {
            using SqlDatabase database = new(_connectionString);
            database.ExecuteNonQuery(
                ProcedurePatterns<T>.DeleteProcedure,
                CommandType.StoredProcedure,
                new SqlParameter { ParameterName = "@ID", Value = id }
            );
        }

        public virtual void ExecuteNonQuerySP(string procedureName, params SqlParameter[] parameters)
        {
            using SqlDatabase database = new(_connectionString);
            database.ExecuteNonQuery(
                procedureName,
                CommandType.StoredProcedure,
                parameters
            );
        }

        public virtual string ExecuteScalarSP(string procedureName, params SqlParameter[] parameters)
        {
            using SqlDatabase database = new(_connectionString);
            return (string)database.ExecuteScalar<object>(
                procedureName,
                CommandType.StoredProcedure,
                parameters
            );
        }

        public virtual IEnumerable<string> ExecuteReaderSP(string procedureName, params SqlParameter[] parameters)
        {
            using SqlDatabase database = new(_connectionString);
            var reader = database.ExecuteReader(procedureName, CommandType.StoredProcedure, parameters);

            while (reader.Read())
            {
                yield return reader[0].ToString();
            }
        }

        protected IEnumerable<SqlParameter> GetParameters(T item, string procedureName)
        {
            foreach (var property in GetProperties())
            {
                if (GetProcedureParameters(procedureName).Contains($"@{property.Name}"))
                {
                    SqlParameter parameter = new();
                    parameter.ParameterName = $"@{property.Name}";
                    parameter.Value = property.GetValue(item);
                    yield return parameter;
                }
            }
        }

        protected IEnumerable<PropertyInfo> GetProperties() => typeof(T).GetProperties();

        protected string[] GetProcedureParameters(string procedureName)
        {
            DataTable procedureParameters = new();

            using (SqlDatabase database = new(_connectionString))
            {
                procedureParameters = database.GetDataTable(
                    "select * from GetProcedureParameters_TF(@procedureName)",
                    new SqlParameter { ParameterName = "@procedureName", Value = procedureName }
                );
            }

            return procedureParameters.Rows.OfType<DataRow>().Select(row => row[0].ToString()).ToArray();
        }
    }
}