using System;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class CategoryService : ServiceRepositoryBase<Category, CategoryRepository>
    {
        public CategoryService(IConfiguration configuration) : base(configuration)
        {

        }
    }
}