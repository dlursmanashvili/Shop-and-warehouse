using System;
using Store.Models;
using Microsoft.Extensions.Configuration;

namespace Store.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>
    {
        public PermissionRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}