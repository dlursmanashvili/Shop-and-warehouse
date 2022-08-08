using System;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class PermissionService : ServiceRepositoryBase<Permission, PermissionRepository>
    {
        public PermissionService(IConfiguration configuration) : base(configuration)
        {
        }
    }
}