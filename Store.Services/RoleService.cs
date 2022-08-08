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
    public class RoleService : ServiceRepositoryBase<Role, RoleRepository>
    {
        public RoleService(IConfiguration configuration) : base(configuration)
        {
        }
        public IEnumerable<Permission> LoadRolePermissions(object roleId) => _repository.LoadRolePermissions(roleId);

        public void AssignPermissionToRole(object roleId, object permissionId) 
            => _repository.AssignPermissionToRole(roleId, permissionId);

        public void UnassignPermissionToRole(object roleId, object permissionId) 
            => _repository.UnassignPermissionToRole(roleId, permissionId);
    }
}