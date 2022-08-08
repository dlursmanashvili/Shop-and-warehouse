using System;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class ProductService : ServiceRepositoryBase<Product, ProductRepository>
    {
        public ProductService(IConfiguration configuration) : base(configuration)
        {
            
        }

        public Category GetProductCategory(object productId) => _repository.GetProductCategory(productId);
    }
}