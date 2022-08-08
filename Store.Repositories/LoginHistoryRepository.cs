using System;
using Store.Models;
using Microsoft.Extensions.Configuration;

namespace Store.Repositories
{
    public class LoginHistoryRepository : RepositoryBase<LoginHistory>
    {
        public LoginHistoryRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}