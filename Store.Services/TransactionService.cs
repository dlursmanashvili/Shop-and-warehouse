using System;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class TransactionService : ServiceRepositoryBase<Transaction, TransactionRepository>
    {
        public TransactionService(IConfiguration configuration) : base(configuration)
        {

        }
    }
}