using System;
using Store.Models;
using Microsoft.Extensions.Configuration;

namespace Store.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}