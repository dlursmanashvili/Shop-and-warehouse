using System;
using Store.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DatabaseHelper;
using System.Data.SqlClient;
using System.Data;

namespace Store.Repositories
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<Permission> LoadRolePermissions(object roleId)
        {
            using (SqlDatabase database = new(_connectionString))
            {
                SqlDataReader reader = database.ExecuteReader(
                    Constants.ProcedureNames.RolePermissions,
                    CommandType.StoredProcedure,
                    new SqlParameter("@RoleID", roleId) { SqlDbType = SqlDbType.Int }
                );

                while (reader.Read())
                {
                    Permission p = new();
                    p.ID = reader["ID"].ToString();
                    p.PermissionName = reader["PermissionName"].ToString();
                    p.PermissionCode = Convert.ToInt16(reader["PermissionCode"]);
                    p.Description = reader["Description"].ToString();
                    yield return p;
                }
            }
        }

        public void AssignPermissionToRole(object userId, object roleId)
        {
            ExecuteNonQuerySP(
                Constants.ProcedureNames.AssignPermissionToRole,
                new SqlParameter("@RoleID", userId) { SqlDbType = SqlDbType.Int },
                new SqlParameter("@PermissionID", roleId) { SqlDbType = SqlDbType.Int }
            );
        }

        public void UnassignPermissionToRole(object userId, object roleId)
        {
            ExecuteNonQuerySP(
                Constants.ProcedureNames.UnassignPermissionToRole,
                new SqlParameter("@RoleID", userId) { SqlDbType = SqlDbType.Int },
                new SqlParameter("@PermissionID", roleId) { SqlDbType = SqlDbType.Int }
            );
        }
    }
}