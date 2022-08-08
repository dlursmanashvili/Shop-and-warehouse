using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class UserService : ServiceRepositoryBase<User, UserRepository>
    {
        public UserService(IConfiguration configuration) : base(configuration)
        {

        }

        public Role GetUserRole(object userId) => _repository.GetUserRole(userId);

        public List<short> LoadUserPermissions(object id) => _repository.LoadUserPermissions(id);

        public void AssignUserToRole(object userId, object roleId)
            => _repository.AssignUserToRole(userId, roleId);

        public void UnassignUserToRole(object userId, object roleId)
            => _repository.UnassignUserToRole(userId, roleId);

        public bool Login(string username, string password)
        {
            SqlParameter userId = new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.Output };

            _repository.ExecuteNonQuerySP(
                Constants.ProcedureNames.Login,
                new SqlParameter("@Username", username) { SqlDbType = SqlDbType.VarChar },
                new SqlParameter("@Password", password) { SqlDbType = SqlDbType.VarChar },
                userId
            );

            CurrentUser.UserId = (int)userId.Value;
            CurrentUser.Permissions = LoadUserPermissions(CurrentUser.UserId);
            return CurrentUser.UserId > 0;
        }

        public void ChangeUserPassword(object userId, string newPassword, string oldPassword) => _repository.ChangeUserPassword(userId, newPassword, oldPassword);


    }
}