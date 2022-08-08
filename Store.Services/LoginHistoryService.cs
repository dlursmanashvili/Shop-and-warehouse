using System;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class LoginHistoryService : ServiceRepositoryBase<LoginHistory, LoginHistoryRepository>
    {
        public LoginHistoryService(IConfiguration configuration) : base(configuration)
        {

        }
    }
}