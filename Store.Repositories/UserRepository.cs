using System;
using Store.Models;
using Microsoft.Extensions.Configuration;
using DatabaseHelper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Role GetUserRole(object userId)
        {
            using (SqlDatabase database = new(_connectionString))
            {
                SqlDataReader reader = database.ExecuteReader(
                    Constants.ProcedureNames.UserRole,
                    CommandType.StoredProcedure,
                    new SqlParameter("@UserID", userId) { SqlDbType = SqlDbType.Int }
                );

                if (reader.Read())
                {
                    return new()
                    {
                        ID = reader["ID"].ToString(),
                        RoleName = reader["RoleName"].ToString(),
                        Description = reader["Description"].ToString()
                    };
                }
                else return default;
            }
        }

        public List<short> LoadUserPermissions(object id)
        {
            return ExecuteReaderSP(
                    Constants.ProcedureNames.UserPermissionCodes,
                    new SqlParameter("@UserID", id) { SqlDbType = SqlDbType.Int }
                    ).Select(p => short.Parse(p)).ToList();
        }

        public void AssignUserToRole(object roleId, object permissionId)
        {
            ExecuteNonQuerySP(
                Constants.ProcedureNames.AssignUserToRole,
                new SqlParameter("@UserID", roleId) { SqlDbType = SqlDbType.Int },
                new SqlParameter("@RoleID", permissionId) { SqlDbType = SqlDbType.Int }
            );
        }

        public void UnassignUserToRole(object roleId, object permissionId)
        {
            ExecuteNonQuerySP(
                Constants.ProcedureNames.UnassignUserToRole,
                new SqlParameter("@UserID", roleId) { SqlDbType = SqlDbType.Int },
                new SqlParameter("@RoleID", permissionId) { SqlDbType = SqlDbType.Int }
            );
        }

        public void ChangeUserPassword(object userId, string newPassword, string oldPassword)
        {
            ExecuteNonQuerySP(
                Constants.ProcedureNames.ChangeUserPassword,
                new SqlParameter("@ID", userId) { SqlDbType = SqlDbType.Int },
                new SqlParameter("@NewPassword", newPassword) { SqlDbType = SqlDbType.VarChar, Size = 20 },
                new SqlParameter("@OldPassword", oldPassword) { SqlDbType = SqlDbType.VarChar, Size = 20 }
            );
        }
    }
}