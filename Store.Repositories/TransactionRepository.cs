using System;
using Store.Models;
using Microsoft.Extensions.Configuration;

namespace Store.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>
    {
        public TransactionRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}